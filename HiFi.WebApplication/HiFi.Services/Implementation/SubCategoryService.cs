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
        public SubCategoryService(IRepository<SubCategoryOne> subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
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
    }
}
