using CRUDApp.Data;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Models;
using CRUDAppWeb.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {

        private IUnitOfWork unitOfWork;
        public CompanyController(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }
        public IActionResult Index()
        {
            return View(unitOfWork.Company.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company Company)
        {
            if (ModelState.IsValid)
            {

                unitOfWork.Company.Add(Company);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(Company);
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var dbFromCompany = unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (dbFromCompany == null)
            {
                return NotFound();
            }
            return View(dbFromCompany);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company model)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Company.Update(model);
                unitOfWork.Save();
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
            var CompanyFromDb = unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (CompanyFromDb == null)
            {
                return NotFound();
            }
            return View(CompanyFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var CompanyFromDb = unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (CompanyFromDb == null)
            {
                return NotFound();
            }
            unitOfWork.Company.Delete(CompanyFromDb);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}
