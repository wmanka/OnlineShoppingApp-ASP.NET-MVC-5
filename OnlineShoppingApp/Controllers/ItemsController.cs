using OnlineShoppingApp.Models;
using OnlineShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace OnlineShoppingApp.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext context;
        public ItemsController()
        {
            context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            var viewModel = new ItemsViewModel()
            {
                Items = context.Items.Include(m => m.Category).ToList(),
                Categories = context.Categories.ToList()
            };

            return View(viewModel);
        }

        public ActionResult MyItems()
        {
            var userId = User.Identity.GetUserId();
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            var items = context.Items.Include(m => m.Category).ToList().Where(m => m.User == user);

            var viewModel = new ItemsViewModel()
            {
                Items = items,
                Categories = context.Categories.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new ItemFormViewModel()
            {
                Item = new Item(),
                Categories = context.Categories.ToList()
            };

            return View("ItemForm", viewModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(Item item)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ItemFormViewModel()
                {
                    Item = item,
                    Categories = context.Categories.ToList()
                };

                return View("ItemForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var currentUser = context.Users.Single(u => u.Id == userId);

            item.User = currentUser;

            if (item.Id == 0)
            {
                context.Items.Add(item);
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Items");

        }
    }
}