using OnlineShoppingApp.Interfaces;
using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OnlineShoppingApp.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationDbContext context = null;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Category entity)
        {
            context.Categories.Add(entity);
        }

        public void Delete(Category entity)
        {
            context.Categories.Remove(entity);
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category GetDetail(Expression<Func<Category, bool>> predicate)
        {
            return context.Categories.SingleOrDefault(predicate);
        }

        public IEnumerable<Category> GetOverview(Expression<Func<Category, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return context.Categories.Where(predicate);
            }

            return context.Categories;
        }
    }
}