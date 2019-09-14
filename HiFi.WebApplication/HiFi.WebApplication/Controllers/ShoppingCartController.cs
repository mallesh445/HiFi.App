using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HiFi.Data.Models;
using HiFi.Data.ViewModels;
using HiFi.Services;
using HiFi.WebApplication.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCart;
        public ShoppingCartController(IProductService productService, IShoppingCartService shoppingCart)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult AddToCart(int? productId = 0, decimal? price = 0, int? quantity = 0)
        {
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var shoppingCartItems = await _shoppingCart.GetShoppingCartItemsAsync();
            var shoppingCartCountTotal = await _shoppingCart.GetCartCountAndTotalAmountAsync();
            HttpContext.Session.SetInt32("CartCount", shoppingCartCountTotal.ItemCount);
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = shoppingCartItems,
                ShoppingCartItemsTotal = shoppingCartCountTotal.ItemCount,
                ShoppingCartTotal = shoppingCartCountTotal.TotalAmount,
            };

            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(int productId,int productQty = 1)
        {
            var selectedProduct = await _productService.GetProductById(productId);
            if (selectedProduct == null)
            {
                return NotFound();
            }
            await _shoppingCart.AddToCartAsync(selectedProduct);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = await _productService.GetProductById(productId);
            if (selectedProduct == null)
            {
                return NotFound();
            }

            await _shoppingCart.RemoveFromCartAsync(selectedProduct);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAllCart()
        {
            await _shoppingCart.ClearCartAsync();
            return RedirectToAction("Index");
        }

        //public IActionResult ShowCart()
        //{
        //    return View();
        //}

        //[BindProperty]
        //public OrderDetailsCartViewModel detailCart { get; set; }


        #region Tangy code
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Index")]
        //public IActionResult IndexPost()
        //{
        //    var claimsIdentity = (ClaimsIdentity)this.User.Identity;
        //    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        //    detailCart.listCart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value).ToList();

        //    detailCart.OrderHeader.OrderDate = DateTime.Now;
        //    detailCart.OrderHeader.UserId = claim.Value;
        //    detailCart.OrderHeader.Status = SD.StatusSubmitted;
        //    OrderHeader orderHeader = detailCart.OrderHeader;
        //    _db.OrderHeader.Add(orderHeader);
        //    _db.SaveChanges();

        //    foreach (var item in detailCart.listCart)
        //    {
        //        item.MenuItem = _db.MenuItem.FirstOrDefault(m => m.Id == item.MenuItemId);
        //        OrderDetails orderDetails = new OrderDetails
        //        {
        //            MenuItemId = item.MenuItemId,
        //            OrderId = orderHeader.Id,
        //            Description = item.MenuItem.Description,
        //            Name = item.MenuItem.Name,
        //            Price = item.MenuItem.Price,
        //            Count = item.Count
        //        };
        //        _db.OrderDetails.Add(orderDetails);
        //    }

        //    _db.ShoppingCart.RemoveRange(detailCart.listCart);
        //    _db.SaveChanges();
        //    HttpContext.Session.SetInt32("CartCount", 0);

        //    return RedirectToAction("Confirm", "Order", new { id = orderHeader.Id });
        //}


        //public IActionResult Plus(int cartId)
        //{
        //    var cart = _db.ShoppingCart.Where(c => c.Id == cartId).FirstOrDefault();
        //    cart.Count += 1;
        //    _db.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult Minus(int cartId)
        //{
        //    var cart = _db.ShoppingCart.Where(c => c.Id == cartId).FirstOrDefault();
        //    if (cart.Count == 1)
        //    {
        //        _db.ShoppingCart.Remove(cart);
        //        _db.SaveChanges();

        //        var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
        //        HttpContext.Session.SetInt32("CartCount", cnt);
        //    }
        //    else
        //    {
        //        cart.Count -= 1;
        //        _db.SaveChanges();
        //    }
        //    return RedirectToAction(nameof(Index));
        //} 
        #endregion
    }
}