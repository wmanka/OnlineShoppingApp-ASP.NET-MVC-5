using Microsoft.AspNet.Identity;
using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShoppingApp.Controllers.API
{
    public class WishlistController : ApiController
    {
        private ApplicationDbContext context;

        public WishlistController()
        {
            context = new ApplicationDbContext();
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult AddToWishlist(int id)
        {
            var userId = User.Identity.GetUserId();

            if (context.Wishlists.Any(w => w.UserId == userId && w.ItemId == id))
                return BadRequest();

            var wishlistItem = new Wishlist()
            {
                UserId = userId,
                ItemId = id
            };

            context.Wishlists.Add(wishlistItem);
            context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult RemoveFromWishlist(int id)
        {
            var userId = User.Identity.GetUserId();

            var wishlistItem = context.Wishlists.Single(w => w.ItemId == id && w.UserId == userId);

            context.Wishlists.Remove(wishlistItem);
            context.SaveChanges();

            return Ok();
        }
    }
}
