using System.Threading.Tasks;

namespace Boleyn.Countries.Content.Providers
{
    public interface IProvider<TEntity>
    {
        Task<TEntity> Get(string predicate);
    }
}