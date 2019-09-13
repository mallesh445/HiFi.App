using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Services
{
    public interface ISalesOrderService
    {
        IEnumerable<OrderHeader> GetAllSalesOrders();

        int TotalOrdersCount();
    }
}
