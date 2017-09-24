using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShoppingApp.Models
{
    public class Wishlist
    {
        public Item Item { get; set; }

        public ApplicationUser User { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ItemId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }

    }
}