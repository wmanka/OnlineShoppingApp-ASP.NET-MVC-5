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

        public ApplicationUser User { get; set; }

        public bool IsPayed { get; set; }

        [Required]
        [Display(Name = "Payment")]
        public int PaymentTypeId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Address")]
        public string CustomerAddress { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public int CustomerPhone { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }

    }
}