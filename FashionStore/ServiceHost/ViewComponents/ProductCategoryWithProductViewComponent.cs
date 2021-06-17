using _01_FashionStoreQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc;


namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategory;

        public ProductCategoryWithProductViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategory = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _productCategory.GetProductCategoriesWithProducts();
            return View(categories);
        }
    }
}
