using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using HiFi.WebApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HiFi.Data.Data;
using HiFi.Data.Models;
using HiFi.WebApplication.Areas.Admin.Services.Profile;
using HiFi.Services.Catalog;
using HiFi.Repository;
using HiFi.Services;
using HiFi.Services.Implementation;
using AutoMapper;
using HiFi.Data.ViewModels;
using HiFi.Data.DomainObjects;
using HiFi.WebApplication.Helpers;

namespace HiFi.WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
            }).AddDefaultTokenProviders().AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDBContext>();
            //services.AddDefaultIdentity<IdentityUser>()
            //    .AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<ApplicationDBContext>();

            services.AddMemoryCache();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddRazorPagesOptions(options =>
            {
                options.AllowAreas = true;
                //options.Conventions.AddPageRoute("/Admin/Index", "Admin");
            });
            services.Configure<Microsoft.AspNetCore.Mvc.Razor.RazorViewEngineOptions>(o =>
            {
                o.AreaViewLocationFormats.Add("/Areas/{2}/{0}" + Microsoft.AspNetCore.Mvc.Razor.RazorViewEngine.ViewExtension);
                o.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            //Dependencies
            ResolveDependencyServices(services);
            #region Automapper Configuration
            MapperConfiguration mapperConfiguration = ResolveMappers();
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper); 
            #endregion

            //services.AddAutoMapper(typeof(Startup).Assembly);
            //services.AddAutoMapper();
            services.AddAutoMapper(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseStatusCodePagesWithRedirects("~/error/{0}");
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseAuthentication();
            app.UseSession();

            #region RouteCommented
            //This is for Areas controller as home screen
            //app.UseMvc(routes =>
            //{
            //    routes.MapAreaRoute(
            //        name: "areas",
            //        areaName: "Admin",
            //        template: "{area:exists}/{controller=Dashboards}/{action=Dashboard1}/{id?}");

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //}); 
            #endregion

            //This is for showing normal controller as home screen
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Dashboards}/{action=Dashboard1}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }

        /// <summary>
        /// ResolveDependencyServices
        /// </summary>
        /// <param name="services"></param>
        private void ResolveDependencyServices(IServiceCollection services)
        {
            services.AddScoped<IRepository<Category>, EfRepository<Category>>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ISalesOrderService, SalesOrderService>();

            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShoppingCartRepository>(sp => ShoppingCartRepository.GetCart(sp));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //services.AddScoped(IDbContext, ApplicationDBContext);
            services.AddScoped<ProfileManager, ProfileManager>();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<ICacheService, CacheService>();
        }

        /// <summary>
        /// Mapping entities initializing.
        /// </summary>
        /// <returns></returns>
        private MapperConfiguration ResolveMappers()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubCategoryOne, SubCategoryViewModel>()
                .ForMember(dest=>dest.SubCategoryId,opt=>opt.MapFrom(src=>src.SubCategoryOneId));
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Product, LatestProductsViewModel>();
                cfg.CreateMap<Product, FeatureProductsViewModel>();
                cfg.CreateMap<ProductImage, ProductImageViewModel>();
                cfg.CreateMap<OrderDto, OrderHeader>();
            });
            return config;
        }
    }
}
