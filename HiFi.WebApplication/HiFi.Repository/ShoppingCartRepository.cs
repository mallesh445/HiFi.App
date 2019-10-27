using HiFi.Data.Data;
using HiFi.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Repository
{
    public class ShoppingCartRepository: IShoppingCartRepository
    {
        private readonly ApplicationDBContext _context;

        public string Id { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCartRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            var httpContext = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            var context = services.GetRequiredService<ApplicationDBContext>();

            var request = httpContext.Request;
            var response = httpContext.Response;

            //var cardId = request.Cookies["CartId-cookie"] ?? Guid.NewGuid().ToString();
            //response.Cookies.Append("CartId-cookie", cardId, new CookieOptions
            //{
            //    Expires = DateTime.Now.AddMonths(1)
            //});
            string cardId = request.Cookies["CartId-cookie"];
            if (string.IsNullOrEmpty(cardId))
            {
                cardId = Guid.NewGuid().ToString();
                response.Cookies.Append("CartId-cookie", cardId, new CookieOptions
                {
                    Expires = DateTime.Now.AddMonths(1)
                });
            }

            return new ShoppingCartRepository(context)
            {
                Id = cardId
            };
        }

        public async Task<int> AddToCartAsync(Product product, int qty = 1)
        {
            return await AddOrRemoveCart(product, qty);
        }

        public async Task<int> RemoveFromCartAsync(Product product)
        {
            return await AddOrRemoveCart(product, -1);
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            try
            {
                ShoppingCartItems = ShoppingCartItems ?? await _context.ShoppingCartItems
                        .Where(e => e.ShoppingCartId == Id)
                        .Include(e => e.Product)
                        .ToListAsync();

                return ShoppingCartItems;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ClearCartAsync()
        {
            var shoppingCartItems = _context
                .ShoppingCartItems
                .Where(s => s.ShoppingCartId == Id);

            _context.ShoppingCartItems.RemoveRange(shoppingCartItems);

            ShoppingCartItems = null; //reset
            await _context.SaveChangesAsync();
        }

        public async Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync()
        {
            var subTotal = ShoppingCartItems?
                .Select(c => c.Product.Price * c.Qunatity) ??
                await _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == Id)
                .Select(c => c.Product.Price * c.Qunatity)
                .ToListAsync();

            return (subTotal.Count(), subTotal.Sum());
        }

        private async Task<int> AddOrRemoveCart(Product product, int qty)
        {
            var shoppingCartItem = await _context.ShoppingCartItems
                            .SingleOrDefaultAsync(s => s.ProductId == product.PKProductId && s.ShoppingCartId == Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = Id,
                    Product = product,
                    Qunatity = 0
                };

                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }

            shoppingCartItem.Qunatity += qty;

            if (shoppingCartItem.Qunatity <= 0)
            {
                shoppingCartItem.Qunatity = 0;
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            await _context.SaveChangesAsync();

            ShoppingCartItems = null; // Reset

            return await Task.FromResult(shoppingCartItem.Qunatity);
        }

    }
}
