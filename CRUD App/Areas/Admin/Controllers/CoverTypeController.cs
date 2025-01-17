using CRUDApp.Data.Repository;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_App.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        public IUnitOfWork unitOfWork;
        public CoverTypeController(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = unitOfWork.CoverType.GetAll();
            return View(coverTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Add(coverType);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CoverType cover = unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            return View(cover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Update(coverType);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CoverType cover = unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            return View(cover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePOST(int? id)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Delete(unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id));
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
