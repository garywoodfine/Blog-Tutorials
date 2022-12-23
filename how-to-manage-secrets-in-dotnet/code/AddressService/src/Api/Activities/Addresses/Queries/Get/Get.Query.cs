using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Threenine.Activities.Addresses.Queries.Get;

public class Query : IRequest<SingleResponse<Response>>
{
       [FromQuery(Name = "postCode")] public string PostCode { get; set; } 
}


