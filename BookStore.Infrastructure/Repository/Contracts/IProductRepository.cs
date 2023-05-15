using BookStore.Infrastructure.Models;

namespace BookStore.Infrastructure.Repository.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
