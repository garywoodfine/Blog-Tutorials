using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Boleyn.Database.Entities.Authors;
using MediatR;
using Threenine.Data;

namespace Cms.Endpoints.Salutations.GetAll
{
    public class GetAllSalutationHandler : IRequestHandler<GetAllSalutationQuery, IEnumerable<GetAllResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllSalutationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<GetAllResponse>> Handle(GetAllSalutationQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadOnlyRepositoryAsync<Salutation>();
            var salutations = await repo.GetListAsync(x => x.Description.Contains(request.Value));
            return _mapper.Map<IEnumerable<GetAllResponse>>(salutations);
        }
    }
}