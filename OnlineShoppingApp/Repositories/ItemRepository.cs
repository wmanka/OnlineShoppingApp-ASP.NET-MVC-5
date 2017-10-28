using OnlineShoppingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnlineShoppingApp.Models;

namespace OnlineShoppingApp.Repositories
{
    public class ItemRepository : IItemRepository, IDisposable
    {
        private ApplicationDbContext context;

        public ItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void CreateItem(Item item)
        {
            context.Items.Add(item);
        }

        public void DeleteItem(int id)
        {
            var item = context.Items.Find(id);
            context.Items.Remove(item);
        }

        public Item GetItemById(int id)
        {
            return context.Items.Find(id);
        }

        public IEnumerable<Item> GetItems()
        {
            return context.Items.ToList();
        }

        public void UpdateItem(Item item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}