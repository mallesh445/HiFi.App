using HiFi.Data.Models;
using HiFi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        //public IShoppingCartService ShoppingCart { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public int ShoppingCartItemsTotal { get; set; }

        public string Comments { get; set; }
    }
}
