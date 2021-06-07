using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductPicture;
using System.Collections.Generic;


namespace ShopManagement.Domain.ProductPictureAgg
{
   public interface IProductPictureRepository : IRepository<long , ProductPicture>
    {
        ProductPicture GetWithProductAndCatefory(long id);
        EditProductPicture GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
