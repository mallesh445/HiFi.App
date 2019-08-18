using HiFi.Data;
using HiFi.Data.Data;
using HiFi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public IEnumerable<T> GetAll()
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

        public T GetById(object id)
        {
            return tableEntity.Find(id);
        }
        public bool Insert(T obj)
        {
            tableEntity.Add(obj);
            return Save();
        }
        public T InsertData(T entity)
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
        public bool Update(T obj)
        {
            tableEntity.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return Save();
        }
        public bool Delete(object id)
        {
            T existing = tableEntity.Find(id);
            tableEntity.Remove(existing);
            return Save();
        }
        public bool Save()
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

        public bool BulkCreate(IList<T> categoriesList)
        {
            tableEntity.AddRange(categoriesList);
            return Save();
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

        public IEnumerable<Product> GetProductsBySubCategoryId(int subcaId = 0)
        {
            if (subcaId == 0)
            {
                var data = _context.Product.Include(sc => sc.SubCategoryOne).Include(pi => pi.ProductImage);
                return data;
            }
            else
            {
                var data = _context.Product.Where(a => a.SubCategoryOneId == subcaId)
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
    }
}
