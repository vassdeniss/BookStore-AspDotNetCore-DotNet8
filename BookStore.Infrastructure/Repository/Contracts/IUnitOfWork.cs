using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repository.Contracts
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }

        IProductRepository ProductRepository { get; }

        Task SaveAsync();
    }
}
