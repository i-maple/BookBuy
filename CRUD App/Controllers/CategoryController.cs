using CRUD_App.Data;
using CRUD_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_App.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {   
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CategoryModel> categoriesList =  _db.Categories;
            return View(categoriesList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("SameNameError", "Category Name shouldn't be same as Display Order");
            }
            if (ModelState.IsValid)
            {

            _db.Categories.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var dbFromCategory = _db.Categories.Find(id);
            if(dbFromCategory == null)
            {
                return NotFound();
            }
            return View(dbFromCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel model)
        {
            if(model.Name == model.DisplayOrder.ToString())
            {
                ModelState.AddModelError("SameNameError", "Display Order and Name cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(model);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            var categoryFromDb = _db.Categories.Find(id);
            if(id == null || id == 0)
            {
                return NotFound();
            }
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
