using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}