using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prods.Models;
using Prods.Repository.IRepository;

namespace Prods.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> product = _unitOfWork.Product.GetAll().ToList();
            return View(product);
        }
        public IActionResult AddProduct()
        {
            ViewBag.JournalList = new SelectList(_unitOfWork.Journal.GetAll(), "JournalId", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.JournalList = new SelectList(_unitOfWork.Journal.GetAll(), "JournalId", "Name", product.JournalId);
            return View(product);
        }

        public IActionResult EditProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            ViewBag.JournalList = new SelectList(_unitOfWork.Journal.GetAll(), "JournalId", "Name", product.JournalId);
            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid) 
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            ViewBag.JournalList = new SelectList(_unitOfWork.Journal.GetAll(), "JournalId", "Name", product.JournalId);
            return View();
        }
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            return View(product);
            
        }
        [HttpPost, ActionName("DeleteProduct")]
        public IActionResult DeleteProductPost(int? id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
