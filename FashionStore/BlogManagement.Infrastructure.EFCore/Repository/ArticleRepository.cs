using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly ArticleContext _articleContext;

        public ArticleRepository(ArticleContext articleContext) : base(articleContext)
        {
            _articleContext = articleContext;
        }

        public EditArticle GetDetails(long id)
        {
            return _articleContext.Articles.Select(x => new EditArticle()
            {
                Id = x.Id,
                CanonialAddress = x.CanonialAddress,
                Description = x.Description,
                Keyword = x.Keyword,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription,
                Slug = x.Slug,
                Title = x.Title,
                CategoryId = x.CategoryId
            }).FirstOrDefault(x => x.Id == id);
        }

        public Article GetWithCategory(long id)
        {
            return _articleContext.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);

        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _articleContext.Articles.Select(x => new ArticleViewModel()
            {
                Id = x.Id,
                Picture = x.Picture,
                Category = x.Category.Name,
                CategoryId = x.CategoryId,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription,
                Title = x.Title
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
