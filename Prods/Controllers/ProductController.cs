using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prods.Models;
using Prods.Repository.IRepository;

namespace Prods.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _image;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment image)
        {
            _unitOfWork = unitOfWork;
            _image = image;
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
        public IActionResult AddProduct(Product product,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string img = _image.WebRootPath;

                if(img != null)
                {
                    string imgname = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string prodpath = Path.Combine(img, @"images\Products");

                    using(var productpath = new FileStream(Path.Combine(prodpath, imgname), FileMode.Create))
                    {
                        file.CopyTo(productpath);
                    }
                    product.ImageUrl = @"\images\Products\" + imgname;
                }
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
        public IActionResult EditProduct(Product product, IFormFile? file)
        {
            if (ModelState.IsValid) 
            {
                string image = _image.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string Prodpath = Path.Combine(image, @"images\Products");

                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        var oldpath = Path.Combine(image, product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldpath))
                        {
                            System.IO.File.Delete(oldpath);
                        }
                    }

                    using(var filestream = new FileStream(Path.Combine(Prodpath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    product.ImageUrl = @"\images\Products\" + filename;
                }
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
