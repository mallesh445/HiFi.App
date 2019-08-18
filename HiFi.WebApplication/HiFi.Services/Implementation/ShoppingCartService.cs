using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HiFi.Data.Models;
using HiFi.Repository;

namespace HiFi.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        public ShoppingCartService(IShoppingCartRepository _shoppingCartRepository)
        {
            shoppingCartRepository = _shoppingCartRepository;
        }
        public string Id { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get ; set ; }

        public async Task<int> AddToCartAsync(Product product, int qty = 1)
        {
            return await shoppingCartRepository.AddToCartAsync(product, qty);
        }

        public Task ClearCartAsync()
        {
            return shoppingCartRepository.ClearCartAsync();
        }

        public async Task<(int ItemCount, decimal TotalAmount)> GetCartCountAndTotalAmountAsync()
        {
            return await shoppingCartRepository.GetCartCountAndTotalAmmountAsync();
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            return await shoppingCartRepository.GetShoppingCartItemsAsync();
        }

        public async Task<int> RemoveFromCartAsync(Product product)
        {
            return await shoppingCartRepository.RemoveFromCartAsync(product);
        }
    }
}
