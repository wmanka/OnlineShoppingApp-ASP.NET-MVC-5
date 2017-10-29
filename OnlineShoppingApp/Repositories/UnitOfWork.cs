using OnlineShoppingApp.Interfaces;
using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingApp.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context;

        public UnitOfWork()
        {
            context = new ApplicationDbContext();
        }

        private IRepository<Item> itemRepository = null;
        private IRepository<Category> categoryRepository = null;

        public IRepository<Item> ItemRepository
        {
            get
            {
                if (itemRepository == null)
                    itemRepository = new ItemRepositoryWithUoW(context);
                return itemRepository;
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(context);
                return categoryRepository;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    context.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}