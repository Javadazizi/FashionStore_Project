

using _01_FashionStoreQuery.Contract.Product;
using System.Collections.Generic;

namespace _01_FashionStoreQuery.Contract.ProductCategory
{
  public  class ProductCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string ClassName { get; set; }
        public string Slug { get; set; }
        public string Desription { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public List<ProductQueryModel> Products { get; set; }
    }
}
