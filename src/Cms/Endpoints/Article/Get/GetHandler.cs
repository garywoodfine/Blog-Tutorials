using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Boleyn.Database.Entities.Contents;
using MediatR;
using Threenine.Data;

namespace Cms.Endpoints.Article.Get
{
    public class GetHandler : IRequestHandler<GetArticleQuery, GetArticleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetArticleResponse> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var repo = _unitOfWork.GetReadOnlyRepositoryAsync<Content>();
            var content = await repo.SingleOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<GetArticleResponse>(content);
        }
    }
}