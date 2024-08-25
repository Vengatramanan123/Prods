using Microsoft.AspNetCore.Mvc;
using Prods.Models;
using Prods.Repository.IRepository;

namespace Prods.Controllers
{

    public class JournalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public JournalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Journal> Journal = _unitOfWork.Journal.GetAll().ToList();
            return View(Journal);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Journal journal)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Journal.Add(journal);
                _unitOfWork.Save();
                return RedirectToAction("Index");

            }
            return View();

        }
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
                Journal? journal = _unitOfWork.Journal.Get(u => u.JournalId == id);
                return View(journal);
        }
        [HttpPost]
        public IActionResult Edit(Journal journal)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Journal.Update(journal);
                _unitOfWork.Save();
                return RedirectToAction("Index");

            }
            return View();

        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
                Journal? journal = _unitOfWork.Journal.Get(u => u.JournalId == id);
                return View(journal);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Journal journal = _unitOfWork.Journal.Get(u => u.JournalId == id);
            if(journal == null)
            {
                return NotFound();
            }
            _unitOfWork.Journal.Remove(journal);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
