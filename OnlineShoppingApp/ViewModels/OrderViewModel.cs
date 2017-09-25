using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingApp.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        public IEnumerable<PaymentType> PaymentTypes { get; set; }
    }
}