using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_FashionStoreQuery.Contract.Article
{
   public interface IArticleQuery
    {
        List<ArticleQueryModel> LatestArticles();

    }
}
