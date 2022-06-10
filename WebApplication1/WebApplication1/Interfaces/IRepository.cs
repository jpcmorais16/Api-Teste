using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> GetById(Expression<Func<T, bool>> expression);
        void add(T entity);
        void update(T entity);
        Task<T> GetByNome(Expression<Func<T, bool>> expression);
        void delete(T entity);
    }
}
