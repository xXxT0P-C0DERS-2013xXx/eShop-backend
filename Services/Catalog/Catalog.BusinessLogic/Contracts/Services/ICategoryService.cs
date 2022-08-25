﻿namespace Catalog.BusinessLogic.Contracts.Services;

public interface ICategoryService
{  
    Task AddCategory(CategoryModel model);
    Task<CategoryModel?> GetCategoryById(Guid id);
    Task UpdateCategoryById(CategoryModel model, Guid id);
    Task DeleteCategoryById(Guid id);
    Task<List<CategoryModel>> GetAllCategories();
}