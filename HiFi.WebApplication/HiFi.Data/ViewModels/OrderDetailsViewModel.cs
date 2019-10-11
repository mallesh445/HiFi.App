using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderHeader OrderHeader { get; set; }
        public List<OrderDetails> OrderDetail { get; set; }
    }
}
