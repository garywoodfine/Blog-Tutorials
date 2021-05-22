using MediatR;

namespace Cms.Endpoints.Salutations.Post
{
    public class CreateSalutationCommand : IRequest<int>
    {
        public string Abbreviation { get; set; }
        public string FullWord  { get; set; }
        public string Description { get; set; }
    }
}