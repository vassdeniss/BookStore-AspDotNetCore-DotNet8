﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly BookStoreDbContext context;
        private readonly DbSet<T> dbSet;

        public Repository(BookStoreDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = this.dbSet;
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = this.dbSet;
            return await query.Where(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        public void Remove(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            this.dbSet.RemoveRange(entities);
        }
    }
}
