//using _01_FashionStoreQuery.Contract.Article;
//using BlogManagement.Infrastructure.EFCore;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _01_FashionStoreQuery.Query
//{
//   public class ArticleQuery : IArticleQuery
//    {
//        private readonly ArticleContext _articleContext;

//        public ArticleQuery(ArticleContext articleContext)
//        {
//            _articleContext = articleContext;
//        }
//        public List<ArticleQueryModel> LatestArticles()
//        {
//            return _articleContext.ArticleCategories.Where(x => x.PublishDate <= DateTime.Now).Select(x => new ArticleQueryModel()
//            {
//                Title = x.Title,
//                Slug = x.Slug,
//                Picture = x.Picture,
//                PictureAlt = x.PictureAlt,
//                PictureTitle = x.PictureTitle,
//                PublishDate = x.PublishDate.ToFarsi(),
//                ShortDescription = x.ShortDescription,
//            }).ToList();
//        }
//    }
//}
