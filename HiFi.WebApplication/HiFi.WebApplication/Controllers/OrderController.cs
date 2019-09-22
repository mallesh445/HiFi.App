using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HiFi.Data.DomainObjects;
using HiFi.Data.Models;
using HiFi.Services;
using HiFi.WebApplication.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HiFi.WebApplication.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;//= NLog.LogManager.GetCurrentClassLogger();
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IEmailSender _emailSender;
        //private readonly IOrderRepository _orderRepository;

        public OrderController(IShoppingCartService shoppingCartService, IMapper mapper, IOrderService orderService, IEmailSender emailSender
            ,ILogger<OrderController> logger)
        {
            _shoppingCartService = shoppingCartService;
            _mapper = mapper;
            _orderService = orderService;
            _emailSender = emailSender;
            _logger = logger;
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
            try
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

                string bodyMessage = GetBodyMessage(orderDto);
                await _emailSender.SendEmailAsync(orderDto.Email, "Thank you for Placing order with us.", bodyMessage);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return View("CheckoutComplete");
        }

        public async Task<IActionResult> MyOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetAllOrdersAsync(userId);
            return View(orders);
        }

        public string GetBodyMessage(OrderDto orderDto)
        {
            string MailBodyText = string.Empty;
            try
            {
                string FilePath = Directory.GetCurrentDirectory()+"\\EmailTemplates\\Order.html";
                StreamReader str = new StreamReader(FilePath);
                MailBodyText = str.ReadToEnd();
                str.Close();
                //Repalce [username] = signup user name   
                MailBodyText = MailBodyText.Replace("[username]", orderDto.FirstName + orderDto.LastName);
                MailBodyText = MailBodyText.Replace("[Status]", orderDto.Status);
                MailBodyText = MailBodyText.Replace("[AddressLine1]", orderDto.AddressLine1);
                MailBodyText = MailBodyText.Replace("[City]", orderDto.City);
                MailBodyText = MailBodyText.Replace("[State]", orderDto.State);
                MailBodyText = MailBodyText.Replace("[PhoneNumber]", orderDto.PhoneNumber);
                MailBodyText = MailBodyText.Replace("[ZipCode]", orderDto.ZipCode);
                return MailBodyText;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
            return MailBodyText;
        }
    }
}