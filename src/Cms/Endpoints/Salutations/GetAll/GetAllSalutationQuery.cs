using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Salutations.GetAll
{
    public class GetAllSalutationQuery : IRequest<IEnumerable<GetAllResponse>>
    {
       [FromRoute (Name = "filter")] public string Filter { get; set; }
       [FromRoute (Name = "value")] public string Value { get; set; }
        
    }
}