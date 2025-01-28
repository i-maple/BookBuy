using CRUDApp.Data.Repository;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Models;
using CRUDApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork unitOfWork;
        private IWebHostEnvironment webHostEnvironment;
        public ProductController(IUnitOfWork unit, IWebHostEnvironment webHostEnvironment)
        {
            unitOfWork = unit;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new()
            {
                Id = id == null ? 0 : (int)id,
                CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverTypeList = unitOfWork.CoverType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
            };
            if (id != null)
            {
                productViewModel.Product = unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            }
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productVm, IFormFile? formFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = webHostEnvironment.WebRootPath;
                if (formFile != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string newPath = Path.Combine(wwwRootPath, @"images\products\");
                    var extension = Path.GetExtension(formFile.FileName);

                    if (productVm.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(newPath, fileName + extension), FileMode.Create))
                    {
                        formFile.CopyTo(fileStreams);
                    }

                    productVm.Product.ImageUrl = @"images\products\" + fileName + extension;
                }
                if (productVm.Product.Id == 0)
                {
                    unitOfWork.Product.Add(productVm.Product);
                }
                else
                {
                    unitOfWork.Product.Update(productVm.Product);
                }
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(productVm);
        }

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        #region Api Calls
        [HttpGet]

        public IActionResult GetAll()
        {
            var productList = unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { Data = productList });
        }

        [HttpDelete]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "Error While Deleting Product" });
            }
            var product = unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            string wwwRootPath = webHostEnvironment.WebRootPath;
            if (product.ImageUrl != null)
            {
                var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            unitOfWork.Product.Delete(product);
            unitOfWork.Save();
            return Json(new { success = true, message = "Product Deleted Successfully" });
        }

        #endregion Api Calls
    }
}
