using HiFi.Data;
using HiFi.Data.Data;
using HiFi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiFi.Repository
{
    public partial class EfRepository<T> : IRepository<T> where T : class
    {

        private ApplicationDBContext _context = null;
        private DbSet<T> table = null;
        
        public EfRepository(ApplicationDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public IEnumerable<ProductImage> GetAllById(int pKProductId)
        {
            return _context.ProductImage.Where(a => a.Product.PKProductId == pKProductId);
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }
        public bool Insert(T obj)
        {
            table.Add(obj);
            return Save();
        }
        public T InsertData(T entity)
        {
            var storedEntity = table.Add(entity);
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
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return Save();
        }
        public bool Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
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
            table.AddRange(categoriesList);
            return Save();
        }

        public ApplicationUser GetApplicationUser(string userid ="")
        {
            if (userid!="")
            {
                return _context.ApplicationUser.Where(a => a.Id == userid).FirstOrDefault();
            }
            else
            {
                return _context.ApplicationUser.FirstOrDefault();
            }
        }

    }
}
