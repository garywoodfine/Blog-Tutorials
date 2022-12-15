using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Article.Get;

public class Query 
{
    [FromRoute(Name = "id")] public string Id { get; set; }
    [FromRoute(Name = "category")] public string Category { get; set; }
}