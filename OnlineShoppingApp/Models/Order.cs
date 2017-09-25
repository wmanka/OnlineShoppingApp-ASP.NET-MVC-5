using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime DateOrdered { get; set; }

        public PaymentType PaymentType { get; set; }

        [Required]
        public int PaymentTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerAddress { get; set; }

        [Required]
        public int CustomerPhone { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

    }
}