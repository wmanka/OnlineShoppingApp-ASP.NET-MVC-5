using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingApp.Models
{
    public class Cart
    {
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public Cart(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}