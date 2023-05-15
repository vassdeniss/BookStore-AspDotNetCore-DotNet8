using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;

namespace BookStore.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly BookStoreDbContext context;

        public ProductRepository(BookStoreDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public void Update(Product product)
        {
            this.context.Products.Update(product);
        }
    }
}
