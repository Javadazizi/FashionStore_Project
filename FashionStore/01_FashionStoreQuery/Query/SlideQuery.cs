using _01_FashionStoreQuery.Contract.Slide;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_FashionStoreQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides.Where(x => x.IsRemoved == false).Select(x => new SlideQueryModel()
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                Link = x.Link,
                Title = x.Title
            }).ToList();
        }
    }
}
