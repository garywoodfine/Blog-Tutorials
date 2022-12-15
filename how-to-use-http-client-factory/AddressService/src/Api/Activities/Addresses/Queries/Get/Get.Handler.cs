using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Services;
using Threenine.ApiResponse;


namespace Threenine.Activities.Addresses.Queries.Get;

public class Handler : IRequestHandler<Query, SingleResponse<Response>>
{
    private readonly IAddressDataProvider _provider;
    private readonly IMapper _mapper;

    public Handler(IAddressDataProvider provider, IMapper mapper)
    {
        _provider = provider;
        _mapper = mapper;
    }

    public async Task<SingleResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
    {
        var addresses = await _provider.GetByPostCode(request.PostCode, cancellationToken);
        return new SingleResponse<Response>(new Response { Addresses = _mapper.Map<Address[]>(addresses.Item)});
    }
}
