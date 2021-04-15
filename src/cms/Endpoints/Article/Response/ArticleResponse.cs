using System;

namespace Cms.Endpoints.Article.Response
{
    public class ArticleResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string SubHeading { get; set; }
        public string Content { get; set; }
        public DateTime Published { get; set; }
    }
}