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
using OnlineShoppingApp.Repositories;

namespace OnlineShoppingApp.Controllers
{
    public class OrdersController : Controller
    {
        private GenericUnitOfWork unitOfWork = null;

        public OrdersController()
        {
            unitOfWork = new GenericUnitOfWork();
        }

        public OrdersController(GenericUnitOfWork unitOW)
        {
            this.unitOfWork = unitOW;
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new OrderViewModel()
            {
                Order = new Order(),
                PaymentTypes = GetPaymentTypes()
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
                    PaymentTypes = GetPaymentTypes()
                };

                return View("Create", viewModel);
            }

            var paymentTypeId = order.PaymentTypeId;

            var cartItems = (List<Cart>)Session[Cart];

            double fullPrice = 0;
            foreach (var item in cartItems)
            {
                fullPrice += item.Item.Price * item.Quantity;
            }

            order.User = GetCurrentUser();
            order.DateOrdered = DateTime.Now;
            order.IsPayed = false;
            order.HasBeenShipped = false;
            order.FullPrice = fullPrice;

            unitOfWork.Repository<Order>().Add(order);
            unitOfWork.SaveChanges();

            foreach(var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    ItemId = item.Item.Id,
                    OrderId = order.Id,
                    Quantity = item.Quantity,
                    Price = item.Item.Price * item.Quantity
                };

                unitOfWork.Repository<OrderItem>().Add(orderItem);
            }

            unitOfWork.SaveChanges(); 

            if(paymentTypeId == 3)
                return RedirectToAction("Pay", "PayPal");

            return RedirectToAction("Confirmation", "Orders");
        }

        // Payment page

        [Authorize]
        public ActionResult Confirmation(Order order)
        {
            return View(order);
        }

        [Authorize]
        public ActionResult MyOrders()
        {
            var orders = GetOrders().Where(o => o.User == GetCurrentUser());

            return View("MyOrders", orders);
        }

        [Authorize]
        public ActionResult Details(int? orderId)
        {
            if (orderId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = new OrderDetailsViewModel()
            {
                Order = unitOfWork.Repository<Order>().GetDetail(o => o.Id == orderId),
                OrderItems = unitOfWork.Repository<OrderItem>().GetAll().Where(o => o.OrderId == orderId)
                // Include(m => m.Item)
            };

            return View("Details", viewModel);
        }

        [Authorize]
        public ActionResult RedirectAndClearCart()
        {
            Session.Remove(Cart);

            unitOfWork.SaveChanges();

            return RedirectToAction("MyOrders");
        }


        //

        private string Cart = "Cart";

        private IEnumerable<PaymentType> GetPaymentTypes()
        {
            return unitOfWork.Repository<PaymentType>().GetAll();
        }

        private ApplicationUser GetCurrentUser()
        {
            var userId = User.Identity.GetUserId();
            return unitOfWork.Repository<ApplicationUser>().GetDetail(u => u.Id == userId);
        }

        private IEnumerable<Order> GetOrders()
        {
            return unitOfWork.Repository<Order>().GetAll();
        }
    }
}