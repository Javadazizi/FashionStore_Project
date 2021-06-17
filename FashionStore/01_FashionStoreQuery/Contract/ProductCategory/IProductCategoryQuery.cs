
using System.Collections.Generic;

namespace _01_FashionStoreQuery.Contract.ProductCategory
{
   public interface IProductCategoryQuery
    {
        //ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
        List<ProductCategoryQueryModel> GetProductCategories();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}
