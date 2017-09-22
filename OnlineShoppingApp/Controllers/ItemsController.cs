using OnlineShoppingApp.Models;
using OnlineShoppingApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

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

            if (item.Id == 0)
            {
                context.Items.Add(item);
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Items");

        }
    }
}