using HiFi.Data;
using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiFi.Repository
{
    public partial interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        bool Insert(T obj);
        bool Update(T obj);
        bool Delete(object id);
        bool Save();
        bool BulkCreate(IList<T> categoriesList);
        ApplicationUser GetApplicationUser(string userid="");
    }
}
