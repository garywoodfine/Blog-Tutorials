using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Boleyn.Database.Entities.Authors;
using MediatR;
using Threenine.Data;

namespace Cms.Endpoints.Salutations.Get
{
    public class GetSalutationHandler : IRequestHandler<GetSalutationQuery, GetSalutationResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetSalutationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetSalutationResponse> Handle(GetSalutationQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadOnlyRepositoryAsync<Salutation>();
            var salutation = await repo.SingleOrDefaultAsync(x => x.Id == request.Id);
            return _mapper.Map<GetSalutationResponse>(salutation);
        }
    }
}