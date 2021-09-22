using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Boleyn.Countries.Content.Providers;
using MediatR;
using WorldBank.Models;

namespace Boleyn.Countries.Activities.Sample.Get
{
    public class Handler : IRequestHandler<Query, Response>
    {
        private readonly IProvider<WorldBank.Models.Country> _provider;
        private readonly IMapper _mapper;

        public Handler(IProvider<WorldBank.Models.Country> provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
        {
            try
            {
                var country = await _provider.Get(query.CountryCode);
                return country != null ? _mapper.Map<Response>(country) : null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}