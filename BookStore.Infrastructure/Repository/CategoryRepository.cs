using System.Threading.Tasks;

using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Models;
using BookStore.Infrastructure.Repository.Contracts;

namespace BookStore.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly BookStoreDbContext context;

        public CategoryRepository(BookStoreDbContext context)
            : base(context)
        {
            this.context = context;
        }

        public void Update(Category category)
        {
            this.context.Categories.Update(category);
        }

        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
