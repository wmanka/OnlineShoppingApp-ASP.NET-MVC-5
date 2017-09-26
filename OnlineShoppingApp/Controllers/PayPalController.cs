using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingApp.Controllers
{
    public class PayPalController : Controller
    {
        private ApplicationDbContext context;

        public PayPalController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Pay()
        {
            return View();
        }
    }
}