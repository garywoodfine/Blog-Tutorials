using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Security.KeyVault.Secrets;
using MediatR;
using Threenine.ApiResponse;


namespace Api.Activities.WhatYouGot.Queries.Get;

public class Handler : IRequestHandler<Query, SingleResponse<Response>>
{
    private readonly SecretClient _secretClient;
  

    public Handler(SecretClient secretClient)
    {
        _secretClient = secretClient;
      
    }

    public async Task<SingleResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var secret = await _secretClient.GetSecretAsync("SecretKey", cancellationToken: cancellationToken);
        return new SingleResponse<Response>(new Response{ WhatYouGot = secret.Value.Value });
    }
}
