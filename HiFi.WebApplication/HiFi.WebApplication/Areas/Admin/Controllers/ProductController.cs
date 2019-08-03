using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HiFi.Common;
using HiFi.Data.Data;
using HiFi.Data.Models;
using HiFi.Services;
using HiFi.Services.Catalog;
using HiFi.WebApplication.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HiFi.WebApplication.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;

        public ProductController(IProductService productService, IHostingEnvironment hostingEnvironment, 
            ICategoryService categoryService,ISubCategoryService subCategoryService)
        {
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
        }

        // GET: Admin/SubCategoryOne
        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            var listProductViewModel = new List<ProductViewModel>();
            foreach (var item in products)
            {
                ProductViewModel productViewModel = new ProductViewModel
                {
                    Description = item.Description,
                    DisplayOrder = item.DisplayOrder,
                    Price = item.Price,
                    ProductId = item.PKProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    ShortDescription = item.ShortDescription,
                    SubCategoryId = item.SubCategoryOne?.SubCategoryName
                };
                var prodImages = _productService.GetAllProductImagesById(item.PKProductId);
                foreach (var img in prodImages)
                {
                    if (img.IsMainImage)
                    {
                        ProductImageViewModel productImageViewModel = new ProductImageViewModel
                        {
                            ImageName = img.ImageName,
                            ImagePath = img.ImagePath,
                            IsMainImage = img.IsMainImage,
                            CreatedDate = img.CreatedDate,
                            PKImageId = img.PKImageId,
                            UpdatedDate = img.UpdatedDate,
                            FKProductId = img.Product.PKProductId
                            
                        };
                        productViewModel.ProductImageModel.Add(productImageViewModel);
                    }
                }
                listProductViewModel.Add(productViewModel);
            }
            return View(listProductViewModel);
        }

        // GET: Admin/SubCategoryOne/Create
        public IActionResult Create()
        {
            //ViewBag.FKCategoryId = new SelectList(objCategoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
            ViewBag.SubCategoryId = new SelectList(_subCategoryService.GetAllSubCategories(), "SubCategoryOneId", "SubCategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = new Product
                    {
                        ProductName = productModel.ProductName,
                        Description = productModel.Description,
                        ShortDescription = productModel.ShortDescription,
                        DisplayOrder = productModel.DisplayOrder,
                        Price = productModel.Price,
                        Quantity = productModel.Quantity,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        SubCategoryOne = new SubCategoryOne
                        { SubCategoryOneId= Convert.ToInt32(productModel.SubCategoryId) }
                    };

                    var resultProduct = _productService.InsertProduct(product);

                    if (resultProduct != null)
                    {
                        //Image Being Saved
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var files = HttpContext.Request.Form.Files;
                        var productImage = new ProductImage();
                        if (files[0] != null && files[0].Length > 0)
                        {
                            //when user uploads an image
                            var uploads = Path.Combine(webRootPath, "Images");
                            var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));
                            using (var filestream = new FileStream(Path.Combine(uploads, resultProduct.PKProductId + extension), FileMode.Create))
                            {
                                files[0].CopyTo(filestream);
                            }
                            productImage.ImagePath = @"\Images\" + resultProduct.PKProductId + extension;
                            productImage.ImageName = resultProduct.ProductName;
                        }
                        else
                        {
                            //when user does not upload image
                            var uploads = Path.Combine(webRootPath, @"Images\" + SD.DefaultFoodImage);
                            System.IO.File.Copy(uploads, webRootPath + @"\Images\" + resultProduct.PKProductId + ".png");
                            productImage.ImagePath = @"\Images\" + resultProduct.PKProductId + ".png";
                            productImage.ImageName = resultProduct.ProductName;
                        }
                        productImage.CreatedDate = DateTime.Now;
                        productImage.UpdatedDate = DateTime.Now;
                        productImage.Product = resultProduct;
                        var result = _productService.InsertProductImage(productImage);
                    }
                    return RedirectToAction(nameof(Index));
                }
                //return View(category);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

    }
}