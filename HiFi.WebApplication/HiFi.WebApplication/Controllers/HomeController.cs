using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HiFi.WebApplication.Models;
using HiFi.Common;
using HiFi.Data.ViewModels;
using HiFi.Services;
using HiFi.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace HiFi.WebApplication.Controllers
{
    //[Route("Home")] 
    public class HomeController : Controller
    {
        IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCart;
        //private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IProductService productService, IMapper mapper, IShoppingCartService shoppingCart)
        {
            _productService = productService;
            _mapper = mapper;
            _shoppingCart = shoppingCart;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                string userData = User.Identity.Name;
                string authenticationType = HttpContext.User.Identity.AuthenticationType;
                string roleName = User.Claims.
                    Where(t => t.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").
                    Select(a => a.Value).FirstOrDefault();

                if (userData != null && userData != "")
                {
                    if (string.Equals(roleName.ToLower(), SD.AdminEndUser.ToLower(), StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard1", "Dashboards", new { Area = "Admin" });
                    }
                    else
                    {
                        LatestAndFeatureProductsViewModel latestAndFeatureVM = BindLatestAndFeatureProducts();
                        var products = _productService.GetAllProductsFromBySubCategory();
                        latestAndFeatureVM = PrepareLatestAndFeatureProductsByAutoMapper(products);

                        //var products = _productService.GetAllProducts();
                        //latestAndFeatureVM = PrepareLatestAndFeatureProducts(products);
                        return View(latestAndFeatureVM);
                    }
                }
                else
                {
                    LatestAndFeatureProductsViewModel latestAndFeatureVM = BindLatestAndFeatureProducts();
                    var products = _productService.GetAllProductsFromBySubCategory();
                    latestAndFeatureVM = PrepareLatestAndFeatureProductsByAutoMapper(products);
                    return View(latestAndFeatureVM);
                }
            }
            else
            {
                var shoppingCartCount = await _shoppingCart.GetCartCountAndTotalAmountAsync();
                HttpContext.Session.SetInt32("CartCount", shoppingCartCount.ItemCount);
                LatestAndFeatureProductsViewModel latestAndFeatureVM = null;
                var products = _productService.GetAllProductsFromBySubCategory();
                latestAndFeatureVM = PrepareLatestAndFeatureProductsByAutoMapper(products);
                return View(latestAndFeatureVM);
            }
        }

        private LatestAndFeatureProductsViewModel PrepareLatestAndFeatureProductsByAutoMapper(IEnumerable<Product> products)
        {
            LatestAndFeatureProductsViewModel lpViewModel = BindLatestAndFeatureProducts();
            IEnumerable<FeatureProductsViewModel> featureProductsList = _mapper.Map<IEnumerable<FeatureProductsViewModel>>(products);
            IEnumerable<LatestProductsViewModel> latestProductsList = _mapper.Map<IEnumerable<LatestProductsViewModel>>(products);

            lpViewModel.FeatureProductsVM = new List<FeatureProductsViewModel>();
            lpViewModel.FeatureProductsVM = featureProductsList.OrderBy(d => d.DisplayOrder);
            lpViewModel.LatestProductsVM = new List<LatestProductsViewModel>();
            lpViewModel.LatestProductsVM = latestProductsList.OrderByDescending(l => l.CreatedDate);
            return lpViewModel;
        }

        /// <summary>
        /// Prepare Latest And Feature Products
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        private LatestAndFeatureProductsViewModel PrepareLatestAndFeatureProducts(IEnumerable<Product> products)
        {
            try
            {
                LatestAndFeatureProductsViewModel lpViewModel = new LatestAndFeatureProductsViewModel();
                var latestProds = products.OrderBy(p => p.DisplayOrder).ToList();
                var featureProductsList = new List<FeatureProductsViewModel>();
                var latestProductsList = new List<LatestProductsViewModel>();
                for (int i = 0; i < latestProds.Count; i++)
                {
                    if (i < 20)
                    {
                        FeatureProductsViewModel featureProductsViewModel = new FeatureProductsViewModel
                        {
                            ProductName = latestProds[i].ProductName,
                            Description = latestProds[i].Description,
                            ShortDescription = latestProds[i].ShortDescription,
                            DisplayOrder = latestProds[i].DisplayOrder,
                            CreatedDate = latestProds[i].CreatedDate,
                            UpdatedDate = latestProds[i].UpdatedDate,
                            IsActive = latestProds[i].IsActive,
                            Price = latestProds[i].Price,
                            Quantity = latestProds[i].Quantity,
                            SubCategoryOneId = latestProds[i].SubCategoryOneId
                        };
                        featureProductsList.Add(featureProductsViewModel);
                        LatestProductsViewModel latestProductsViewModel = new LatestProductsViewModel
                        {
                            ProductName = latestProds[i].ProductName,
                            Description = latestProds[i].Description,
                            ShortDescription = latestProds[i].ShortDescription,
                            DisplayOrder = latestProds[i].DisplayOrder,
                            CreatedDate = latestProds[i].CreatedDate,
                            UpdatedDate = latestProds[i].UpdatedDate,
                            IsActive = latestProds[i].IsActive,
                            Price = latestProds[i].Price,
                            Quantity = latestProds[i].Quantity,
                            SubCategoryOneId = latestProds[i].SubCategoryOneId
                        };
                        latestProductsList.Add(latestProductsViewModel);
                    }
                    else
                    {
                        break;
                    }
                }

                lpViewModel.FeatureProductsVM = new List<FeatureProductsViewModel>();
                lpViewModel.FeatureProductsVM = featureProductsList.OrderBy(d => d.DisplayOrder);
                lpViewModel.LatestProductsVM = new List<LatestProductsViewModel>();
                lpViewModel.LatestProductsVM = latestProductsList.OrderByDescending(l => l.CreatedDate);
                return lpViewModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private LatestAndFeatureProductsViewModel BindLatestAndFeatureProducts()
        {
            try
            {
                LatestAndFeatureProductsViewModel latestAndFeatureProductsVM = new LatestAndFeatureProductsViewModel();

                latestAndFeatureProductsVM.FeatureProductsVM = new List<FeatureProductsViewModel>();

                return latestAndFeatureProductsVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        //public IActionResult Dashboard1()
        //{
        //    return View();
        //}
    }
}
