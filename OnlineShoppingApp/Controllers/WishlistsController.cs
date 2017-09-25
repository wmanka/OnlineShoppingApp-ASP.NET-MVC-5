using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingApp.Models;
using System.Net;
using System.Data.Entity;

namespace OnlineShoppingApp.Controllers
{
    public class WishlistsController : Controller
    {
        private ApplicationDbContext context;

        public WishlistsController()
        {
            context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var wishlist = context.Wishlists.Include(w => w.Item.Category).Where(w => w.UserId == userId).ToList();

            return View(wishlist);
        }

        [Authorize]
        public ActionResult AddToWishlist(int id)
        {
            var userId = User.Identity.GetUserId();

            if(userId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (context.Wishlists.Any(w => w.UserId == userId && w.ItemId == id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var wishlistItem = new Wishlist()
            {
                UserId = userId,
                ItemId = id
            };

            context.Wishlists.Add(wishlistItem);
            context.SaveChanges();

            return RedirectToAction("Index", "Wishlists");
        }

        [Authorize]
        public ActionResult RemoveFromWishlist(int id)
        {
            var wishlistItem = context.Wishlists.Single(w => w.ItemId == id);

            context.Wishlists.Remove(wishlistItem);
            context.SaveChanges();

            return RedirectToAction("Index", "Wishlists");
        }
    }
}