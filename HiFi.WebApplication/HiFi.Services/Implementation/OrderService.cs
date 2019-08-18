using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HiFi.Data.Models;
using HiFi.Data.ViewModels;
using HiFi.Repository;

namespace HiFi.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderService(IOrderRepository orderRepository, IShoppingCartService shoppingCartService)
        {
            _orderRepository = orderRepository;
            _shoppingCartService = shoppingCartService;
        }
        public async Task CreateOrderAsync(OrderHeader order)
        {
            var shoppingCartItems = await _shoppingCartService.GetShoppingCartItemsAsync();
            order.OrderTotal = (await _shoppingCartService.GetCartCountAndTotalAmountAsync()).TotalAmount;
            await _orderRepository.CreateOrderAsync(order, shoppingCartItems);
        }

        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync(string userId)
        {
            return await _orderRepository.GetAllOrdersAsync(userId);
        }
    }
}
