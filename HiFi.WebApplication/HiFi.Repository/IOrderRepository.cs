using HiFi.Data.Models;
using HiFi.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Repository
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(OrderHeader orde, IEnumerable<ShoppingCartItem> shoppingCartItemsr);
        Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync();
        Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync(string userId);
    }
}
