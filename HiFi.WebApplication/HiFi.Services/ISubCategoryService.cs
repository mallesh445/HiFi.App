using HiFi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiFi.Services
{
    public interface ISubCategoryService
    {
        IEnumerable<SubCategoryOne> GetAllSubCategories();

        bool InsertSubCategory(SubCategoryOne subCategory);
        bool UpdateSubCategory(SubCategoryOne subCategory);
        bool DeleteSubCategory(SubCategoryOne subCategory);
    }
}
