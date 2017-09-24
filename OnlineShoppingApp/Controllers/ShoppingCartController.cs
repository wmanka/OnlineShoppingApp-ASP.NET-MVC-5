using OnlineShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext context;

        public ShoppingCartController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if(Session["Cart"] == null)
            {
                List<Cart> lsCart = new List<Cart>
                {
                    new Cart(context.Items.Find(id), 1)
                };

                Session["Cart"] = lsCart;
            }
            else
            {
                List<Cart> lscart = (List<Cart>)Session["Cart"];

                var check = IsExisting(id);
                if (check == -1)
                    lscart.Add(new Cart(context.Items.Find(id), 1));
                else
                    lscart[check].Quantity++;

                Session["Cart"] = lscart;
            }

            return View("Index");
        }

        private int IsExisting(int? id)
        {
            List<Cart> lsCart = (List<Cart>)Session["Cart"];
            for(int i = 0; i < lsCart.Count; i++)
            {
                if (lsCart[i].Item.Id == id)
                    return i;
            }
            return -1;
        }

        public ActionResult Delete (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var check = IsExisting(id);
            List<Cart> lsCart = (List<Cart>)Session["Cart"];
            lsCart.RemoveAt(check);

            return View("Index");
        }
    }
}
