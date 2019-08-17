using System.Threading.Tasks;

namespace Renetdux.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task StartTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
