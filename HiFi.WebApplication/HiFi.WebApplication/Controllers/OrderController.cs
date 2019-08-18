using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HiFi.Data.DomainObjects;
using HiFi.Data.Models;
using HiFi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiFi.WebApplication.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        //private readonly IOrderRepository _orderRepository;

        public OrderController(
            IShoppingCartService shoppingCartService,
            IMapper mapper,
            IOrderService orderService)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync();
            if (cartItems?.Count() <= 0)
            {
                return Redirect("/shoppingcart");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromForm]OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return View(orderDto);
            }

            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync();

            if (cartItems?.Count() <= 0)
            {
                ModelState.AddModelError("", "Your Cart is empty. Please add some cakes before checkout");
                return View(orderDto);
            }

            var order = _mapper.Map<OrderDto, OrderHeader>(orderDto);
            order.CreatedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderService.CreateOrderAsync(order);

            await _shoppingCartService.ClearCartAsync();


            return View("CheckoutComplete");
        }


        public async Task<IActionResult> MyOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetAllOrdersAsync(userId);
            return View(orders);
        }
    }
}