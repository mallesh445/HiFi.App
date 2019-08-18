using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
{
    public class OrderDetailsCartViewModel
    {
        public List<ShoppingCartItem> listCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
