using _0_Framework.Application;
using _01_FashionStoreQuery.Contract.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_FashionStoreQuery.Query
{
   public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discount = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var products = _shopContext.Products.Include(x => x.Category).Select(x => new ProductQueryModel()
            {
                Id = x.Id,
                Category = x.Category.Name,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    var discounts = discount.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discounts != null)
                    {
                        int discountRate = discounts.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return products;
        }

        public ProductQueryModel GetProductDetails(string slug)
        {
            var invenrory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();
            var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

            var product = _shopContext.Products.Include(x => x.Category).Include(x => x.ProductPictures).Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Code = x.Code,
                Category = x.Category.Name,
                Name = x.Name,
                Slug = x.Slug,
                CategorySlug = x.Category.Slug,
                Description = x.Description,
                KeyWords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
                //ProductPictures = MapProductPicture(x.ProductPictures)
            }).FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return new ProductQueryModel();

            var productInventory = invenrory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                product.InStock = productInventory.InStock;
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    int discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();

                }
            }

            return product;
        }

        //private static List<ProductPictureQueryModel> MapProductPicture(List<ProductPicture> productPictures)
        //{
        //    return productPictures.Select(x => new ProductPictureQueryModel()
        //    {
        //        Picture = x.Picture,
        //        ProductId = x.ProductId,
        //        PictureAlt = x.PictureAlt,
        //        PictureTitle = x.PictureTitle
        //    }).Where(x => !x.IsRemoved).ToList();
        //}

        //public List<ProductQueryModel> Search(string value)
        //{
        //    var inventory = _inventoryContext.inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
        //    var discounts = _discountContext.CustomerDiscounts.Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Select(x => new { x.ProductId, x.DiscountRate, x.EndDate }).ToList();

        //    var query = _shopContext.Products.Include(x => x.Category).Select(x => new ProductQueryModel()
        //    {
        //        Id = x.Id,
        //        Category = x.Category.Name,
        //        Name = x.Name,
        //        Picture = x.Picture,
        //        PictureAlt = x.PictureAlt,
        //        PictureTitle = x.PictureTitle,
        //        Slug = x.Slug,
        //        ShortDescription = x.ShortDescription
        //    }).AsNoTracking();

        //    if (!string.IsNullOrWhiteSpace(value))
        //        query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

        //    var products = query.OrderByDescending(x => x.Id).ToList();

        //    foreach (var product in products)
        //    {
        //        var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
        //        if (productInventory != null)
        //        {
        //            var price = productInventory.UnitPrice;
        //            product.Price = price.ToMoney();
        //            var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
        //            if (discount != null)
        //            {
        //                int discountRate = discount.DiscountRate;
        //                product.DiscountRate = discountRate;
        //                product.HasDiscount = discountRate > 0;
        //                product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
        //                var discountAmount = Math.Round((price * discountRate) / 100);
        //                product.PriceWithDiscount = (price - discountAmount).ToMoney();
        //            }
        //        }
        //    }
        //    return products;
        //}
    }
}
