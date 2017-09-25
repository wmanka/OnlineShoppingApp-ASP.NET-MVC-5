using Microsoft.AspNet.Identity;
using OnlineShoppingApp.Models;
using OnlineShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingApp.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext context;

        public OrdersController()
        {
            context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new OrderViewModel()
            {
                Order = new Order(),
                PaymentTypes = context.PaymentTypes.ToList()
            };

            return View("Create", viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Order order)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new OrderViewModel()
                {
                    Order = order,
                    PaymentTypes = context.PaymentTypes.ToList()
                };

                return View("Create", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var currentUser = context.Users.Single(u => u.Id == userId);

            order.User = currentUser;
            order.DateOrdered = DateTime.Now;
            order.IsPayed = false;

            context.Orders.Add(order);
            context.SaveChanges();

            var cartItems = (List<Cart>)Session["Cart"];

            foreach(var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    ItemId = item.Item.Id,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    Price = item.Item.Price * item.Quantity
                };

                context.OrderItems.Add(orderItem);
            }

            context.SaveChanges();

            return RedirectToAction("Confirmation", order);
        }

        public ActionResult Confirmation(Order order)
        {
            return View(order);
        }
    }
}