using _0_Framework.Domain;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;


namespace ShopManagement.Domain.ProductAgg
{
   public interface IProductRepository : IRepository<long , Product>
    {
        Product GetProductWithCategory(long id);
        EditProduct GetDetails(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        List<ProductViewModel> GetProducts();
    }
}
