using CRUDApp.Data;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Models;
using CRUDAppWeb.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }
        public IActionResult Index()
        {
            return View(unitOfWork.Category.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("SameNameError", "Category Name shouldn't be same as Display Order");
            }
            if (ModelState.IsValid)
            {

                unitOfWork.Category.Add(category);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var dbFromCategory = unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (dbFromCategory == null)
            {
                return NotFound();
            }
            return View(dbFromCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel model)
        {
            if (model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("SameNameError", "Display Order and Name cannot be same");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(model);
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
            var categoryFromDb = unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var categoryFromDb = unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            unitOfWork.Category.Delete(categoryFromDb);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

    }
}
