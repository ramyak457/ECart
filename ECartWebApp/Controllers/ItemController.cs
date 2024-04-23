using ECartWebApp.Models;
using ECartWebApp.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ECartWebApp.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        private ECartDBEntities objectECartDB;
        public ItemController()
        {
            objectECartDB = new ECartDBEntities();
        }
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

        [HttpPost]
        public JsonResult Index(ItemViewModel objItem)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItem.ImagePath.FileName);
            objItem.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Item item = new Item();
            item.ImagePath = "~/Images/" + NewImage;
            item.CategoryId = objItem.CategoryId;
            item.Description = objItem.Description;
            item.ItemCode = objItem.ItemCode;
            item.ItemId = Guid.NewGuid();
            item.ItemName = objItem.ItemName;
            item.ItemPrice = objItem.ItemPrice;
            objectECartDB.Items.Add(item);
            objectECartDB.SaveChanges();

            return Json(new { Success = true, Message = "Item is added Successfully." }, JsonRequestBehavior.AllowGet);
        }
    }
}