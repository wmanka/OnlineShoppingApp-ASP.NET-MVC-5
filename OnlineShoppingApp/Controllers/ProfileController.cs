using Microsoft.AspNet.Identity;
using OnlineShoppingApp.Models;
using OnlineShoppingApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingApp.Controllers
{
    public class ProfileController : Controller
    {
        private GenericUnitOfWork unitOfWork;

        public ProfileController()
        {
            unitOfWork = new GenericUnitOfWork();
        }

        public ProfileController(GenericUnitOfWork uow)
        {
            this.unitOfWork = uow;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = unitOfWork.Repository<ApplicationUser>().GetDetail(u => u.Id == userId);

            return View(currentUser);
        }
    }
}