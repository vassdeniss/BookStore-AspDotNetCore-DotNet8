using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repository.Contracts
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }

        Task SaveAsync();
    }
}
