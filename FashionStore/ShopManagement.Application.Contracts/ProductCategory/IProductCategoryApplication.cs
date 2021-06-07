using _0_Framework.Application;
using System.Collections.Generic;


namespace ShopManagement.Application.Contracts.ProductCategory
{
   public interface IProductCategoryApplication
    {
        OperationResult Craete(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> GetProductCategories();
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
