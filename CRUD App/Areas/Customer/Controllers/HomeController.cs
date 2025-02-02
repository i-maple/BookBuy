using System.Diagnostics;
using System.Security.Claims;
using CRUDApp.Data.Repository;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Models;
using CRUDApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CRUD_App.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        this.unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> products = unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
        return View(products);
    }

    public IActionResult Details(int? productId)
    {
        if (productId == null)
        {
            return NotFound();
        }
        Product product = unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType");
        if (product == null)
        {
            return NotFound();
        }
        ShoppingCartVM shoppingCart = new() { Count = 1, Product = product };
        return View(shoppingCart);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Details(ShoppingCart shoppingCart)
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) { return NotFound(); }
        shoppingCart.ApplicationUserId = claim.Value;

        var shoppingCartFromDb = unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ApplicationUserId == shoppingCart.ApplicationUserId && u.ProductId == shoppingCart.ProductId);

        if (shoppingCartFromDb == null)
        {
            unitOfWork.ShoppingCart.Add(shoppingCart);
        }
        else
        {
            unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, shoppingCart.Count);
        }
        unitOfWork.Save();

        return RedirectToAction("Index", "Cart"); 
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
