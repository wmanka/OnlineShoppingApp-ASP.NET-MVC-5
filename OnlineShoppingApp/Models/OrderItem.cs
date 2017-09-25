using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShoppingApp.Models
{
    public class OrderItem
    {
        public Order Order { get; set; }

        public Item Item { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        public int ItemId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}