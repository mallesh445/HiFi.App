using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Areas.Admin.ViewModels
{
    public class DashboardViewModel
    {
        public int administrators_count { get; set; }
        public int employees_count { get; set; }
        public int customers_count { get; set; }
        public int categories_count { get; set; }
        public int products_count { get; set; }
        public int subcategories_count { get; set; }
        public int orders_count { get; set; }
    }
}
