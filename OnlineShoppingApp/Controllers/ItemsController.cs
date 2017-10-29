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
using System.Net;
using OnlineShoppingApp.Interfaces;
using OnlineShoppingApp.Repositories;

namespace OnlineShoppingApp.Controllers
{
    public class ItemsController : Controller
    {
        private GenericUnitOfWork unitOfWork = null;

        public ItemsController()
        {
            unitOfWork = new GenericUnitOfWork();
        }

        public ItemsController(GenericUnitOfWork unitOW)
        {
            this.unitOfWork = unitOW;
        }

        public ActionResult Index()
        {
            var viewModel = new ItemsViewModel()
            {
                Items = GetItems(),
                Categories = GetCategories()
            };

            return View(viewModel);
        }

        public ActionResult Filtered(int? categoryId)
        {
            var viewModel = new ItemsViewModel()
            {
                Items = GetItems().Where(m => m.CategoryId == categoryId),
                Categories = GetCategories()
            };

            return View("Index", viewModel);
        }

        [Authorize]
        public ActionResult MyItems()
        {
            var viewModel = new ItemsViewModel()
            {
                Items = GetItems().Where(m => m.User == GetUser()),
                Categories = GetCategories()
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new ItemFormViewModel()
            {
                Item = new Item(),
                Categories = GetCategories()
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
                    Categories = GetCategories()
                };

                return View("ItemForm", viewModel);
            }

            item.User = GetUser();

            if (item.Id == 0)
            {
                unitOfWork.Repository<Item>().Add(item);
            }
            else
            {
                var itemInDb = unitOfWork.Repository<Item>().GetDetail(i => i.Id == item.Id);
                itemInDb.Name = item.Name;
                itemInDb.Description = item.Description;
                itemInDb.Price = item.Price;
                itemInDb.PictureUrl = item.PictureUrl;
            }

            unitOfWork.SaveChanges();

            return RedirectToAction("Index", "Items");

        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var item = unitOfWork.Repository<Item>().GetDetail(i => i.Id == id);

            if (!(item.User == GetUser()))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (item == null)
                return HttpNotFound();

            var viewModel = new ItemFormViewModel()
            {
                Item = item,
                Categories = GetCategories()
            };

            return View("ItemForm", viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var item = unitOfWork.Repository<Item>().GetDetail(i => i.Id == id);
            
            if (item == null)
                return HttpNotFound();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string searchtext)
        {
            var items = GetItems().Where(m => m.Name.Contains(searchtext) || m.Description.Contains(searchtext));

            var viewModel = new ItemsViewModel()
            {
                Categories = GetCategories(),
                Items = items
            };

            return View("Index", viewModel);
        }



        // private methods

        private ApplicationUser GetUser()
        {
            var userId = User.Identity.GetUserId();
            var user = unitOfWork.Repository<ApplicationUser>().GetDetail(u => u.Id == userId);

            return user;
        }

        private IEnumerable<Item> GetItems()
        {
            return unitOfWork.Repository<Item>().GetAll().OrderBy(m => m.Name);
        }

        private IEnumerable<Category> GetCategories()
        {
            return unitOfWork.Repository<Category>().GetAll();
        }
    }
}