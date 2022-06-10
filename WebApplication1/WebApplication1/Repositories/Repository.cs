using System.Linq;
using WebApplication1.Context;
using WebApplication1.Interfaces;
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApplication1.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;

        }

        public void add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<T> GetByNome(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public void update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
