using BookStore.Infrastructure.Models;

namespace BookStore.Infrastructure.Repository.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
