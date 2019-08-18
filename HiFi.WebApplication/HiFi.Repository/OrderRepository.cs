using HiFi.Data.Data;
using HiFi.Data.DomainObjects;
using HiFi.Data.Models;
using HiFi.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        //private readonly IShoppingCartService _shoppingCartService;

        public OrderRepository(
            ApplicationDBContext context)
        {
            _context = context;
            //_shoppingCartService = shoppingCartService;
        }

        public async Task CreateOrderAsync(OrderHeader order, IEnumerable<ShoppingCartItem> shoppingCartItems)
        {
            order.OrderPlacedTime = DateTime.Now;
            if (order.Status == null)
                order.Status = "Pending";
            await _context.Orders.AddAsync(order);

            await _context.OrderDetails.AddRangeAsync(shoppingCartItems.Select(e => new OrderDetails
            {
                Quantity = e.Qunatity,
                ProductName = e.Product.ProductName,
                OrderId = order.Id,
                Price = Convert.ToDouble(e.Product.Price),
                Description = e.Product.Description,
                ProductId = e.ProductId
            }));

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(e => e.OrderDetails)
                .Select(e => new MyOrderViewModel
                {
                    OrderPlacedTime = e.OrderPlacedTime,
                    OrderTotal = e.OrderTotal,
                    OrderHeader = new OrderDto
                    {
                        AddressLine1 = e.AddressLine1,
                        AddressLine2 = e.AddressLine2,
                        City = e.City,
                        Country = e.Country,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PhoneNumber = e.PhoneNumber,
                        State = e.State,
                        ZipCode = e.ZipCode
                    },
                    ProductOrderInfos = e.OrderDetails.Select(o => new MyProductOrderInfo
                    {
                        Name = o.ProductName,
                        Price = o.Price,
                        Quantity = o.Quantity
                    })
                })
                .ToListAsync();

        }
        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync(string userId)
        {
            //throw new Exception("Not imple");
            return await _context.Orders
                .Where(e => e.CreatedByUserId == userId)
                .Include(e => e.OrderDetails)
                .Select(e => new MyOrderViewModel
                {
                    OrderPlacedTime = e.OrderPlacedTime,
                    OrderTotal = e.OrderTotal,
                    OrderHeader = new OrderDto
                    {
                        AddressLine1 = e.AddressLine1,
                        AddressLine2 = e.AddressLine2,
                        City = e.City,
                        Country = e.Country,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PhoneNumber = e.PhoneNumber,
                        State = e.State,
                        ZipCode = e.ZipCode
                    },
                    ProductOrderInfos = e.OrderDetails.Select(o => new MyProductOrderInfo
                    {
                        Name = o.ProductName,
                        Price = o.Price,
                        Quantity = o.Quantity
                    })
                })
                .ToListAsync();
        }
    }
}

