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
            ICategoryService categoryService, ISubCategoryService subCategoryService)
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
                    SubCategoryId = item.SubCategoryOneId.ToString()
                };
                var prodImages = _productService.GetAllProductImagesById(item.PKProductId);
                if (prodImages.Count() > 0)
                {
                    productViewModel.ProductImageModel.Add(AssignProdudctImageToProduct(prodImages));
                }
                #region Implemented in Separate method
                //foreach (var img in prodImages)
                //{
                //    if (img.IsMainImage)
                //    {
                //        ProductImageViewModel productImageViewModel = new ProductImageViewModel
                //        {
                //            ImageName = img.ImageName,
                //            ImagePath = img.ImagePath,
                //            IsMainImage = img.IsMainImage,
                //            CreatedDate = img.CreatedDate,
                //            PKImageId = img.PKImageId,
                //            UpdatedDate = img.UpdatedDate,
                //            FKProductId = img.Product.PKProductId

                //        };
                //        productViewModel.ProductImageModel.Add(productImageViewModel);
                //    }
                //} 
                #endregion
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
                    Product product =PrepareProductEntityFromProductViewModel(productModel,true);

                    var resultProduct = _productService.InsertProduct(product);

                    if (resultProduct != null)
                    {
                        //Image Being Saved
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var files = HttpContext.Request.Form.Files;
                        string uploadedImageName = files[0].FileName.Substring(0, files[0].FileName.LastIndexOf("."));
                        var productImage = new ProductImage();
                        if (files[0] != null && files[0].Length > 0)
                        {
                            //when user uploads an image
                            var uploads = Path.Combine(webRootPath, "Images");
                            var extension = files[0].FileName.Substring(files[0].FileName.LastIndexOf("."), files[0].FileName.Length - files[0].FileName.LastIndexOf("."));
                            using (var filestream = new FileStream(Path.Combine(uploads, uploadedImageName + resultProduct.PKProductId + extension), FileMode.Create))
                            {
                                files[0].CopyTo(filestream);
                            }
                            productImage.ImagePath = @"\Images\" + uploadedImageName + resultProduct.PKProductId + extension;
                            productImage.IsMainImage = true; //if it is 1 image then
                            productImage.ImageName = uploadedImageName;
                            //productImage.ImageName = resultProduct.ProductName;
                        }
                        else
                        {
                            //when user does not upload image
                            var uploads = Path.Combine(webRootPath, @"Images\" + SD.DefaultProductImage);
                            System.IO.File.Copy(uploads, webRootPath + @"\Images\" + resultProduct.ProductName + resultProduct.PKProductId + ".png");
                            productImage.ImagePath = @"\Images\" + resultProduct.ProductName + resultProduct.PKProductId + ".png";
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


        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int _productId =(Int32) id;
            ProductViewModel productViewModel = GetProductByProductId(_productId);

            if (productViewModel == null)
                return NotFound();
            ViewBag.SubCategoryId = new SelectList(_subCategoryService.GetAllSubCategories(), "SubCategoryOneId", "SubCategoryName", productViewModel.SubCategoryId);
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Product product = PrepareProductEntityFromProductViewModel(productViewModel,false);
                    var resultProduct = _productService.UpdateProduct(product);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductViewModel productViewModel = GetProductByProductId(id);

            if (productViewModel == null)
                return NotFound();
            ViewBag.SubCategoryId = new SelectList(_subCategoryService.GetAllSubCategories(), "SubCategoryOneId", "SubCategoryName", productViewModel.SubCategoryId);
            return View(productViewModel);
        }

        private ProductViewModel GetProductByProductId(int productId)
        {
            ProductViewModel productViewModel = null;
            var retrievedProduct = _productService.GetProductByProductId(productId);
            if (retrievedProduct != null)
            {
                productViewModel = PrepareProductViewModelFromProductEntity(retrievedProduct);

                var productImages = _productService.GetAllProductImagesById(retrievedProduct.PKProductId);
                if (productImages != null && productImages.Count() > 0)
                {
                    productViewModel.ProductImageModel.Add(AssignProdudctImageToProduct(productImages));
                }
            }
            else
            {
                return null;
            }
            return productViewModel;
        }

        /// <summary>
        /// Assign ProdudctImages To Product
        /// </summary>
        /// <param name="prodImages"></param>
        /// <returns></returns>
        private ProductImageViewModel AssignProdudctImageToProduct(IEnumerable<ProductImage> prodImages)
        {
            ProductImageViewModel productImageViewModel = null;
            foreach (var img in prodImages)
            {
                if (img.IsMainImage)
                {
                    productImageViewModel = new ProductImageViewModel
                    {
                        ImageName = img.ImageName,
                        ImagePath = img.ImagePath,
                        IsMainImage = img.IsMainImage,
                        CreatedDate = img.CreatedDate,
                        PKImageId = img.PKImageId,
                        UpdatedDate = img.UpdatedDate,
                        FKProductId = img.Product.PKProductId

                    };
                    return productImageViewModel;
                }
            }
            return productImageViewModel;
        }


        /// <summary>
        /// PrepareProductEntityFromProductViewModel
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns></returns>
        private Product PrepareProductEntityFromProductViewModel(ProductViewModel productModel,bool IsNewProduct)
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
                SubCategoryOneId = Convert.ToInt32(productModel.SubCategoryId)
                //SubCategoryOne = new SubCategoryOne
                //{ SubCategoryOneId= Convert.ToInt32(productModel.SubCategoryId) }
            };
            if (IsNewProduct)
            {
                product.PKProductId = productModel.ProductId;
            }
            return product;
        }

        /// <summary>
        /// PrepareProductViewModelFromProductEntity
        /// </summary>
        /// <param name="retrievedProduct"></param>
        /// <returns></returns>
        private ProductViewModel PrepareProductViewModelFromProductEntity(Product retrievedProduct)
        {
            try
            {
                ProductViewModel productViewModel = new ProductViewModel()
                {
                    ProductId = retrievedProduct.PKProductId,
                    ProductName = retrievedProduct.ProductName,
                    Description = retrievedProduct.Description,
                    ShortDescription = retrievedProduct.ShortDescription,
                    DisplayOrder = retrievedProduct.DisplayOrder,
                    Price = retrievedProduct.Price,
                    Quantity = retrievedProduct.Quantity,
                    SubCategoryId = retrievedProduct.SubCategoryOneId.ToString()
                };
                return productViewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Importing Categories.
        /// </summary>
        /// <param name="postedExcelFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImporProducts(IFormFile postedExcelFile)
        {
            if (ModelState.IsValid)
            {
                if (postedExcelFile == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                    TempData["Error"] = "Please Upload Your file.";
                    return RedirectToAction("Index");
                }
                else if (postedExcelFile.Length > UtilityConstants.MaxContentLength)
                {
                    TempData["Error"] = "SizeExceed";
                    return RedirectToAction("Index");
                }
                else if (postedExcelFile.Length > 0)
                {
                    string path = ExcelHelper.SavePathForThePostedFile(postedExcelFile);

                    if (!(path.Contains("xlsx") || path.Contains("xls")))
                        return Content("FileFormatError");

                    try
                    {
                        //List<CategoryImportExcel> records = ExcelHelper.ReadSheet<CategoryImportExcel>(path, true, 0, null, true).ToList();
                        //records = records.Where(r => !string.IsNullOrEmpty(r.CategoryName) && !string.IsNullOrEmpty(r.CreatedByUser)).ToList();
                        //if (records.Count > 0)
                        //{
                        //    //objCategoryBO.InsertCategoryInBulk(records);
                        //    bool result = _categoryService.InsertCategorInBulk(records);
                        //    System.IO.File.Delete(path);
                        //    TempData["Success"] = $"\"NumberOfRecords Uploaded\" : {records.Count()}";
                        //    return RedirectToAction("Index");
                        //}
                        //System.IO.File.Delete(path);
                        //TempData["Success"] = $"\"NumberOfRecords Uploaded\" : 0";
                        //return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("InValidZipCode"))
                        {
                            return Content(ex.Message);
                        }
                    }
                }
            }
            return Content("Error");
        }

    }
}