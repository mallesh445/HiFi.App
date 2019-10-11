using HiFi.Common;
using HiFi.Common.ViewModel;
using HiFi.Data;
using HiFi.Data.Data;
using HiFi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HiFi.Repository
{
    public partial class EfRepository<T> : IRepository<T> where T : class
    {

        private ApplicationDBContext _context = null;
        private DbSet<T> tableEntity = null;

        public EfRepository(ApplicationDBContext context)
        {
            this._context = context;
            tableEntity = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return tableEntity.ToList();
        }
        public IEnumerable<T> GetAll(string[] includes)
        {
            if (includes != null && includes.Count() > 0)
            {
                foreach (string include in includes)
                {
                    tableEntity.Include(include);
                }
                return tableEntity;
                #region table joins generic not work 
                //var qry = tableEntity.ToList();
                //foreach (var inc in includes)
                //    qry = qry.Include(inc);
                //return qry;
                //return includes.Aggregate(tableEntity.AsQueryable(), (query, path) => query.Include(path)); 
                #endregion
            }
            else
            {
                return tableEntity.ToList();
            }
        }
        public T FindBy(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<ProductImage> GetAllById(int pKProductId)
        {
            return _context.ProductImage.Where(a => a.Product.PKProductId == pKProductId);
        }

        public async Task<T> GetById(object id)
        {
            return tableEntity.Find(id);
        }
        public async Task<bool> Insert(T obj)
        {
            tableEntity.Add(obj);
            return await Save();
        }
        public async Task<T> InsertData(T entity)
        {
            var storedEntity = tableEntity.Add(entity);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
            return storedEntity.Entity;
        }
        public async Task<bool> Update(T obj)
        {
            tableEntity.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return await Save();
        }
        public async Task<bool> Delete(object id)
        {
            T existing = tableEntity.Find(id);
            tableEntity.Remove(existing);
            return await Save();
        }
        public async Task<bool> Save()
        {
            int result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> BulkCreate(IList<T> categoriesList)
        {
            tableEntity.AddRange(categoriesList);
            return await Save();
        }

        public ApplicationUser GetApplicationUser(string userid = "")
        {
            if (userid != "")
            {
                return _context.ApplicationUser.Where(a => a.Id == userid).FirstOrDefault();
            }
            else
            {
                return _context.ApplicationUser.FirstOrDefault();
            }
        }

        public IEnumerable<SubCategoryOne> GetSubCategoriesByCategoryId(int categoryId = 0)
        {
            if (categoryId == 0)
            {
                var data = _context.SubCategoryOne.ToList();
                return data;
            }
            else
            {
                var subCategoryId = _context.SubCategoryOne.Where(c => c.CategoryId == categoryId)
                    .Select(sc => sc.SubCategoryOneId).ToList();
                var subCategoriesData = _context.SubCategoryOne.Include(f => f.Category)
                    .Where(c => c.CategoryId == categoryId).ToList();
                //var data = _context.Product.Where(a => a.SubCategoryOneId == categoryId)
                //        .Include(sc => sc.SubCategoryOne).Include(pi => pi.ProductImage);
                return subCategoriesData;
            }
        }
        public IEnumerable<Product> GetProductsBySubCategoryId(int subCategoryId = 0)
        {
            if (subCategoryId == 0)
            {
                var data = _context.Product.Include(sc => sc.SubCategoryOne).Include(pi => pi.ProductImage);
                return data;
            }
            else
            {
                var data = _context.Product.Where(a => a.SubCategoryOneId == subCategoryId)
                        .Include(sc => sc.SubCategoryOne).Include(pi => pi.ProductImage);
                return data;
            }
        }

        /// <summary>
        /// Get Product By ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProductByProductId(int productId)
        {
            var data = _context.Product.Where(a => a.PKProductId == productId)
                    .Include(sc => sc.SubCategoryOne).Include(pi => pi.ProductImage).FirstOrDefault();
            return data;
        }

        public async Task<Product> GetProductById(int id)
        {
            await Task.Delay(1);
            var data = GetProductByProductId(id);
            return data;
        }

        public int GetProductCategoriesByProductId(int currentProductId)
        {
            var subCatId = _context.Product.Where(s => s.PKProductId == currentProductId).
                Select(c => c.SubCategoryOneId).FirstOrDefault();
            return _context.SubCategoryOne.Where(s => s.SubCategoryOneId == subCatId).Select(c => c.CategoryId).FirstOrDefault();
        }

        public List<CategoryChildsCount> GetNoOfProductsAndSubCategoriesByCategories()
        {
            try
            {
                var data = _context.CategoryChildsCounts.FromSql(@"SELECT c.CategoryId,C.CategoryName, count(distinct s.SubCategoryName) as SubCategorycount,  count(Ci.ProductName) as ProductCount
                                FROM Category c
                                INNER JOIN SubCategoryOne s
                                 on c.CategoryId = s.CategoryId
                                left JOIN Product ci
                                  ON s.SubCategoryOneId = ci.SubCategoryOneId
                                GROUP BY C.CategoryId, C.CategoryName").ToList();
                return data;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public List<CategoryNavViewModel> GetCategoriesAndSubCategories()
        {
            try
            {
                List<CategoryNavViewModel> categoryNavViewModels = new List<CategoryNavViewModel>();
                var categoryNavs = _context.CategorySubCategoryTable.FromSql(@"
                                    Select  c.CategoryId,c.CategoryName  ,ISNULL(sc.SubCategoryOneId,0)  AS SubCategoryOneId,ISNULL(sc.SubCategoryName,'') AS SubCategoryName,count(pkproductid) as ProductsCount
                                    From Category c 
                                    left join SubCategoryOne sc on c.CategoryId = sc.CategoryId 
                                    left join Product p on sc.SubCategoryOneId = p.SubCategoryOneId
                                    group by c.CategoryId,c.CategoryName,sc.SubCategoryOneId,sc.SubCategoryName order by c.CategoryId
                                    ").ToList();
                //Added by Ashok
                List<string> categoryList=  categoryNavs.Select(x => x.CategoryName).Distinct().ToList();

                foreach (string item in categoryList)
                {
                    CategoryNavViewModel categoryNav = categoryNavs.Where(x => x.CategoryName == item).Select(x => new CategoryNavViewModel { CategoryId = x.CategoryId, CategoryName = x.CategoryName }).FirstOrDefault();

                    List<SubCategoriesNavViewModel> subCatList = categoryNavs.Where(x => x.CategoryName == item).Select(x => new SubCategoriesNavViewModel { SubCategoryId = x.SubCategoryOneId, SubCategoryName = x.SubCategoryName,NumberOfProducts=x.ProductsCount }).ToList();

                    categoryNav.SubCategories = subCatList;
                    categoryNavViewModels.Add(categoryNav);

                }
                //end by Ashok


                //foreach (var row in categoryNavs)
                //{
                //    CategoryNavViewModel categoryNav = new CategoryNavViewModel();
                //    categoryNav.CategoryId = row.CategoryId;
                //    categoryNav.CategoryName = row.CategoryName;
                //    List<SubCategoriesNavViewModel> subCatList=new List<SubCategoriesNavViewModel>();

                //    categoryNav.SubCategories = subCatList;
                //}
                return categoryNavViewModels;
            }
            catch (Exception e)
            {
                throw;
            }

        }

    }
}
