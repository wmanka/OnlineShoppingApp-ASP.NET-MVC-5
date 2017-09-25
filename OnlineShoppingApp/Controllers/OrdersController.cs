using Microsoft.AspNet.Identity;
using OnlineShoppingApp.Models;
using OnlineShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

        [Authorize]
        public ActionResult Confirmation(Order order)
        {
            return View(order);
        }

        [Authorize]
        public ActionResult MyOrders()
        {
            var userId = User.Identity.GetUserId();

            var orders = context.Orders.Where(o => o.User.Id == userId).ToList();

            return View("MyOrders", orders);
        }

        [Authorize]
        public ActionResult Details(int? orderId)
        {
            if (orderId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = new OrderDetailsViewModel()
            {
                Order = context.Orders.Single(o => o.Id == orderId),
                OrderItems = context.OrderItems.Include(m => m.Item).Where(o => o.OrderId == orderId).ToList()
            };

            return View("Details", viewModel);
        }
    }
}