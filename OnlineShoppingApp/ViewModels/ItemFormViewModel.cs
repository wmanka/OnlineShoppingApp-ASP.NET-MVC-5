using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingApp.ViewModels
{
    public class ItemFormViewModel
    {
        public Item Item { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}