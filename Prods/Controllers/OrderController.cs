using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;
using Prods.Data;
using Prods.Models;
using Prods.Models.ViewModel;
using Prods.Repository;
using Prods.Repository.IRepository;
using Prods.Utilites;
using Stripe;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace Prods.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _db;
        private readonly ApplicationDbContext _context;
        public OrderController(IUnitOfWork db, ApplicationDbContext context)
        {
            _db = db;
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            List<Order> order;
            var userRole = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("Id");

            if (userRole == "Admin" || userRole == "SuperAdmin")
            {
                order = _db.Order.GetAll().ToList();
            }
            else
            {
                if (userId == null)
                {
                    return Unauthorized();
                }
                order = _db.Order.GetAll().Where(u => u.UserId == userId).ToList();
            }
            return View(order);
        }

        public IActionResult Order()
        {
            var journals = _context.Journals
                .Select(j => new { id = j.JournalId, name = j.Name })
                .ToList();

            ViewBag.Journals = journals;
            return View();
        }

        [HttpPost]
        public IActionResult Order(Order order)
        {
            if (ModelState.IsValid)
            {
                var userid = HttpContext.Session.GetInt32("Id");
                if (userid == null)
                {
                    return Unauthorized();
                }
                order.UserId = userid.Value;
                order.OrderStatus = OrderStatus.orderplacedpaymentpending;

                _db.Order.Add(order);
                _db.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Cancel(int? id)
        {
            var userRole = HttpContext.Session.GetString("Username");
            var userId = HttpContext.Session.GetInt32("Id");
            if (userRole == "Admin" || userRole == "SuperAdmin")
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }

                var order = _db.Order.Get(u => u.Id == id && u.UserId == userId);

                if (order == null)
                {
                    return NotFound();
                }

                order.OrderStatus = OrderStatus.ordercancelled ;
                _db.Order.Update(order);
            }
            else
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }

                var order = _db.Order.Get(u => u.Id == id && u.UserId == userId);


                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    // Refund Options
                    if (order.OrderStatus == OrderStatus.paymentreceived.ToString())
                    {
                        var refund = new RefundCreateOptions
                        {
                            Amount = order.Price * 100
                        };
                        var refundService = new RefundService();
                        Refund refundpayment = null;

                        try
                        {
                            refundpayment = refundService.Create(refund);
                        }
                        catch (StripeException ex)
                        {
                            return StatusCode(500, "Failed To Refund");
                        }
                    }

                    var Order = _db.Order.Get(u => u.Id == id);
                    order.OrderStatus = OrderStatus.ordercancelled + "." + OrderStatus.paymentreceived;
                    _db.Order.Update(Order);
                }
            }
            _db.Save();

            return RedirectToAction("Index");
        }

        public IActionResult PayNow(int? Id)
        {
            var userId = HttpContext.Session.GetInt32("Id");
            var paynow = _db.Order.Get(o => o.Id == Id && o.UserId == userId);
            return View(paynow);
        }

        [HttpGet]
        public IActionResult GetProducts(int journalId)
        {
            var products = _context.Products
                .Where(p => p.JournalId == journalId)
                .Select(p => new { id = p.Id, name = p.ProductName })
                .ToList();

            return Json(products);
        }

        [HttpGet]
        public IActionResult GetPrice(int productId)
        {
            var product = _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new { price = p.Price })
                .FirstOrDefault();

            return Json(product);
        }

        // Stripe Integration
        [HttpPost]
        public IActionResult ConfirmPayment(PaymentVM model)
        {
            var stripeToken = Request.Form["StripeToken"].ToString(); 

            if (string.IsNullOrEmpty(stripeToken))
            {
                return View("Error");
            }

            var options = new ChargeCreateOptions
            {
                Amount = (long)(model.Amount * 100), 
                Currency = model.Currency,
                Description = $"Payment for Order #{model.OrderId}",
                Source = stripeToken
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.Status == "succeeded")
            {
                // Save payment details to the database
                var payment = new Payment
                {
                    OrderId = model.OrderId,
                    Amount = (long)(model.Amount * 100),
                    Currency = model.Currency,
                    Description = options.Description,
                    StripeChargeId = charge.Id,
                    Status = charge.Status,
                    PaymentDate = DateTime.Now
                };

                _context.Payments.Add(payment);
                var userId = HttpContext.Session.GetInt32("Id");
                
                var order = _db.Order.Get(o => o.Id == model.OrderId && o.UserId == userId);
                if (order != null)
                {
                    order.OrderStatus = OrderStatus.paymentreceived;
                    _db.Order.Update(order);
                    _db.Save();

                    Email(order);
                }

                return View("PaymentSuccess");
            }

            return View("Error");
        }

        public async Task<IActionResult> Email(Order order)
        {
            var Apikey = "5abeac05910ed3ba60e77134b466e735";
            var ApiSecret = "2ba9eb92d99530ed784157d45e9640ec";
            var client = new MailjetClient(Apikey, ApiSecret);

            var request = new MailjetRequest
            {
                Resource = Send.Resource
            }
            .Property(Send.FromEmail, "thalavengat256@gmail.com")
            .Property(Send.FromName, "Prods")
            .Property(Send.Subject, $"Order Confirmation - Order #{order.Id}")
            .Property(Send.HtmlPart, $"<strong>Hi,</strong> your payment for Order #{order.Id} has been successfully processed.")
            .Property(Send.TextPart, $"Hi, your payment for Order #{order.Id} has been successfully processed.")
            .Property(Send.Recipients, new JArray
            {
                new JObject
                {
                    {"Email", "vengatramanan81@gmail.com"},
                    {"Name", "Vengat"}
                }
            });

            var response = await client.PostAsync(request);

            if (response.StatusCode != 200)
            {
                Console.WriteLine($"Failed to send email: {response.GetErrorMessage()}");
                // Handle the error as needed
            }
            else
            {
                Console.WriteLine("Email sent successfully!");
            }

            return View("Index");
        }

    }
}
