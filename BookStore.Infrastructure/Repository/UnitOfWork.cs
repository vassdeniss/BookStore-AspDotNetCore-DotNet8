using System.Threading.Tasks;

using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repository.Contracts;

namespace BookStore.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreDbContext context;

        public UnitOfWork(BookStoreDbContext context)
        {
            this.context = context;
            this.CategoryRepository = new CategoryRepository(context);
            this.ProductRepository = new ProductRepository(context);
        }

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
