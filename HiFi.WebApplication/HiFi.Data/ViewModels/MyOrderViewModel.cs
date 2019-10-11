using HiFi.Data.DomainObjects;
using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
{
    public class MyOrderViewModel
    {
        public OrderDto OrderHeader { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlacedTime { get; set; }
        public IEnumerable<MyProductOrderInfo> ProductOrderInfos { get; set; }

    }

    public class MyProductOrderInfo
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
    }
}
