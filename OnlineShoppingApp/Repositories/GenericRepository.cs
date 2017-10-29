using OnlineShoppingApp.Interfaces;
using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OnlineShoppingApp.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext context = null;

        IDbSet<T> _objectSet;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            _objectSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }
        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _objectSet.ToList();
        }

        public T GetDetail(Expression<Func<T, bool>> predicate)
        {
            return _objectSet.First(predicate);
        }
        
        public IEnumerable<T> GetOverview(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return _objectSet.Where(predicate);
            return _objectSet.AsEnumerable();
        }
    }
}