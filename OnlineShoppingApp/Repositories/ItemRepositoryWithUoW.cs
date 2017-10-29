using OnlineShoppingApp.Interfaces;
using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;

namespace OnlineShoppingApp.Repositories
{
    public class ItemRepositoryWithUoW : IRepository<Item>
    {
        private ApplicationDbContext context = null;

        public ItemRepositoryWithUoW(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(Item entity)
        {
            context.Items.Add(entity);
        }

        public void Delete(Item entity)
        {
            context.Items.Remove(entity);
        }

        public IEnumerable<Item> GetAll()
        {
            return context.Items.Include(m => m.Category).ToList();
        }

        public Item GetDetail(Expression<Func<Item, bool>> predicate)
        {
            return context.Items.SingleOrDefault(predicate);
        }

        public IEnumerable<Item> GetOverview(Expression<Func<Item, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return context.Items.Where(predicate);
            }

            return context.Items;
        }
    }
}