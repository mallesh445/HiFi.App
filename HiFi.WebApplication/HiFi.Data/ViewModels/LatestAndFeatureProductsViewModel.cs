using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Data.ViewModels
{
    public class LatestAndFeatureProductsViewModel
    {
        public IEnumerable<FeatureProductsViewModel> FeatureProductsVM { get; set; }
        public IEnumerable<LatestProductsViewModel> LatestProductsVM { get; set; }
    }
}
