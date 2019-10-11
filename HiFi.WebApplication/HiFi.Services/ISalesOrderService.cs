using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Services
{
    public interface ISalesOrderService
    {
        Task<IEnumerable<OrderHeader>> GetAllSalesOrders();

        Task<int> TotalOrdersCount();
    }
}
