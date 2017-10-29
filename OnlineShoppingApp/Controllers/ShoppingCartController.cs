using OnlineShoppingApp.Models;
using OnlineShoppingApp.Repositories;
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
        private GenericUnitOfWork unitOfWork = null;

        public ShoppingCartController()
        {
            unitOfWork = new GenericUnitOfWork();
        }

        public ShoppingCartController(GenericUnitOfWork unitOW)
        {
            this.unitOfWork = unitOW;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if(Session[Cart] == null)
            {
                var cartList = new List<Cart>
                {
                    new Cart(unitOfWork.Repository<Item>().GetDetail(m => m.Id == id), 1)
                };

                Session[Cart] = cartList;
            }
            else
            {
                var cartList = (List<Cart>)Session[Cart];

                var check = IsExisting(id);
                if (check == -1)
                    cartList.Add(new Cart(unitOfWork.Repository<Item>().GetDetail(m => m.Id == id), 1));
                else
                    cartList[check].Quantity++;

                Session[Cart] = cartList;
            }

            return View("Index");
        }

        private int IsExisting(int? id)
        {
            var cartList = (List<Cart>)Session[Cart];
            for(int i = 0; i < cartList.Count; i++)
            {
                if (cartList[i].Item.Id == id)
                    return i;
            }
            return -1;
        }

        public ActionResult Delete (int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var check = IsExisting(id);
            var cartList = (List<Cart>)Session[Cart];
            cartList.RemoveAt(check);

            return View("Index");
        }

        public ActionResult AddOne(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var check = IsExisting(id);
            var cartList = (List<Cart>)Session[Cart];
            cartList[check].Quantity++;

            return View("Index");
        }

        public ActionResult RemoveOne(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var check = IsExisting(id);
            var cartList = (List<Cart>)Session[Cart];
            cartList[check].Quantity--;
            if (cartList[check].Quantity == 0)
                cartList.RemoveAt(check);

            return View("Index");
        }


        private string Cart = "Cart";
    }
}
