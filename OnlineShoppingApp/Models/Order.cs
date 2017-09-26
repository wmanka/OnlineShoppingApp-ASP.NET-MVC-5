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

        public bool HasBeenShipped { get; set; }

        public double FullPrice { get; set; }

        [Required]
        [Display(Name = "Payment")]
        public int PaymentTypeId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Street")]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "City")]
        public string CustomerCity { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "PostalCode")]
        public string CustomerPostalCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Country")]
        public string CustomerCountry { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public int CustomerPhone { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }

    }
}