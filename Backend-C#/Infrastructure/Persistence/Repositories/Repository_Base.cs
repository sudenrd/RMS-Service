using Application.Abstractions.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class Repository_Base<T> : IRepository_Base<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository_Base(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public List<T> read()
        {
            return _dbSet.ToList();
        }

        public void update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public int delete(T entity)
        {
            _dbSet.Remove(entity);
            return _context.SaveChanges();
        }
    }
}