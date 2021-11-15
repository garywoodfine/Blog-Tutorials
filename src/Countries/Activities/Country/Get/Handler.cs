using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Boleyn.Countries.Content.Providers;
using MediatR;
using Threenine.ApiResponse;

namespace Boleyn.Countries.Activities.Country.Get
{
    public class Handler : IRequestHandler<Query, SingleResponse<CountryDetail>>
    {
        private readonly IProvider<WorldBank.Models.Country> _provider;
        private readonly IMapper _mapper;

        public Handler(IProvider<WorldBank.Models.Country> provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        public async Task<SingleResponse<CountryDetail>> Handle(Query query, CancellationToken cancellationToken)
        {
                var country = await _provider.Get(query.IsoCode);
                return new SingleResponse<CountryDetail>(_mapper.Map<CountryDetail>(country));
        }
    }
}