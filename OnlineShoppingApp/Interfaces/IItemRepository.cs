using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.Interfaces
{
    public interface IItemRepository : IDisposable
    {
        IEnumerable<Item> GetItems();
        Item GetItemById(int id);
        void CreateItem(Item item);
        void DeleteItem(int id);
        void UpdateItem(Item item);
        void Save();
    }
}
