using Microsoft.AspNetCore.Mvc;
using Prods.Data;
using Prods.Models;
using Prods.Repository.IRepository;

namespace Prods.Controllers
{

    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _customer;
        public CustomerController(IUnitOfWork customer)
        {
            _customer = customer;
        }

        public IActionResult Index()
        {
            List<Customer> customer = _customer.Customer.GetAll().ToList();
            return View(customer);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customer.Customer.Add(customer);
                _customer.Save();
                return RedirectToAction("Index");
            }
            return View();
           
        }
        public IActionResult Edit(int id)
        { 
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            Customer customer = _customer.Customer.Get(u => u.Id == id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customer.Customer.Update(customer);
                _customer.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            Customer customer = _customer.Customer.Get(u => u.Id == id);
            return View(customer); 
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Customer customer = _customer.Customer.Get(u => u.Id == id);
            if(customer == null)
            {
                return NotFound();
            }
            _customer.Customer.Remove(customer);
            _customer.Save();
            return RedirectToAction("Index");

        }
    }
}
