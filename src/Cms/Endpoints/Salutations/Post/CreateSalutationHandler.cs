using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Boleyn.Database.Entities.Authors;
using MediatR;
using Threenine.Data;

namespace Cms.Endpoints.Salutations.Post
{
    public class CreateSalutationHandler : IRequestHandler<CreateSalutationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSalutationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateSalutationCommand request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetRepositoryAsync<Salutation>();
            var salutation = _mapper.Map<Salutation>(request);
            await repo.InsertAsync(salutation, cancellationToken);
            await _unitOfWork.CommitAsync();
            return salutation.Id;
        }
    }
}