using HiFi.Data.Models;
using HiFi.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Services.Implementation
{
    public class SubCategoryService: ISubCategoryService
    {
        private readonly IRepository<SubCategoryOne> _subCategoryRepository;

        private readonly IShoppingCartRepository _shoppingCartRepository;
        public SubCategoryService(IRepository<SubCategoryOne> subCategoryRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _subCategoryRepository = subCategoryRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }


        public virtual IEnumerable<SubCategoryOne> GetAllSubCategories()
        {
            var data = _subCategoryRepository.GetAll();
            return data;
        }

        public bool InsertSubCategory(SubCategoryOne subCategory)
        {
            _subCategoryRepository.Insert(subCategory);
            return true;
        }

        public bool UpdateSubCategory(SubCategoryOne subCategory)
        {
            _subCategoryRepository.Update(subCategory);
            return true;
        }

        public bool DeleteSubCategory(SubCategoryOne subCategory)
        {
            _subCategoryRepository.Delete(subCategory);
            return true;
        }

        public IEnumerable<SubCategoryOne> GetSubCategoriesByCategoryId(int categoryId)
        {
            return _subCategoryRepository.GetSubCategoriesByCategoryId(categoryId);
        }
    }
}
