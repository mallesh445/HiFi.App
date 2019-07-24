using System;
using System.Collections.Generic;
using System.Text;
using HiFi.Data.Models;
using HiFi.Repository;

namespace HiFi.Services.Catalog
{
    public class CategoryService: ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public virtual IEnumerable<Category> GetAllCategories(int storeId = 0, bool showHidden = false, bool loadCacheableCopy = true)
        {
            var data = _categoryRepository.GetAll();
            return data;
        }

        public bool InsertCategory(Category category)
        {
            _categoryRepository.Insert(category);
            return true;
        }

        public bool UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
            return true;
        }

        public bool DeleteCategory(Category category)
        {
            _categoryRepository.Delete(category);
            return true;
        }
    }
}
