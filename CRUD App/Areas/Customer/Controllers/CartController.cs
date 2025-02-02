using System.Security.Claims;
using CRUDApp.Data;
using CRUDApp.Data.Repository;
using CRUDApp.Data.Repository.IRepository;
using CRUDApp.Models;
using CRUDApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_App.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            string userId = claim.Value;
            ShoppingCartVM shoppingCartVM = new()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(
                    u => u.ApplicationUserId == userId,
                    "Product"
                    ),
                OrderHeader = new ()
            };
            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = CalculateTotalPrice(cart.Count, cart.Product.Price, cart.Product.ListPrice, cart.Product.Price50, cart.Product.Price100);
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(shoppingCartVM);
        }
        public async Task<IActionResult> Summary()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            string userId = claim.Value;
            ShoppingCartVM shoppingCartVM = new()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(
                    u => u.ApplicationUserId == userId,
                    "Product"
                    ),
                OrderHeader = new()
            };
            var users = _unitOfWork.ApplicationUser.GetAll();
            ApplicationUser user = await _unitOfWork.ApplicationUser.GetFirstOrDefaultAsync(u => u.Id == userId);
            shoppingCartVM.OrderHeader.ApplicationUser = user;
            shoppingCartVM.OrderHeader.Name = user.Name;
            shoppingCartVM.OrderHeader.Address = user.StreetAddress;
            shoppingCartVM.OrderHeader.PhoneNumber = user.PhoneNumber;
            shoppingCartVM.OrderHeader.City = user.City;
            shoppingCartVM.OrderHeader.State = user.State;
            foreach (var cart in shoppingCartVM.ListCart)
            {
                cart.Price = CalculateTotalPrice(cart.Count, cart.Product.Price, cart.Product.ListPrice, cart.Product.Price50, cart.Product.Price100);
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(shoppingCartVM);
        }
        public IActionResult AddItem(int? cartId)
        {
            if (cartId == null)
            {
                return NotFound();
            }
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult SubtractItem(int? cartId)
        {
            if (cartId == null)
            {
                return NotFound();
            }
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        #region API Calls
        [HttpDelete]
        public IActionResult Delete(int? cartId)
        {
            if (cartId == null)
            {
                return NotFound();
            }
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
            if (cart == null)
            {
                return NotFound();
            }
            _unitOfWork.ShoppingCart.Delete(cart);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Successfully Deleted Cart" });
        }
        #endregion API Calls

        private double CalculateTotalPrice(int count, double price, double listPrice, double price50, double price100)
        {
            if (count > 100)
            {
                return price100;
            }
            else if (count > 50)
            {
                return price50;
            }
            else
            {
                return price;
            }
        }
    }
}
