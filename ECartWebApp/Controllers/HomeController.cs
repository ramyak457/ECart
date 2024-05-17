using ECartWebApp.Models;
using ECartWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECartWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ECartDBEntities objectECartDB;
        public HomeController()
        {
            objectECartDB = new ECartDBEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
            ItemViewModel objItemList = new ItemViewModel();
            objItemList.CategorySelectListItem = (from categoryList in objectECartDB.Categories
                                                  select new SelectListItem()
                                                  {
                                                      Text = categoryList.CategoryName,
                                                      Value = categoryList.CategoryId.ToString(),
                                                      Selected = true
                                                  });
            return View(objItemList);
        }
    }
}