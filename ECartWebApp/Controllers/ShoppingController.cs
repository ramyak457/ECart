using ECartWebApp.Models;
using ECartWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECartWebApp.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        private ECartDBEntities objECartDB;
        private List<ShoppingCartModel> listOfShoppingCartModels;
        public ShoppingController()
        {
            objECartDB = new ECartDBEntities();
            listOfShoppingCartModels = new List<ShoppingCartModel>();
        }
        public ActionResult Index()
        {
            IEnumerable<ShoppingViewModel> listOfItems = (from objItem in objECartDB.Items join objCate in objECartDB.Categories on objItem.CategoryId equals objCate.CategoryId
                                                          select new ShoppingViewModel()
                                                          {
                                                              ImagePath = objItem.ImagePath,
                                                              ItemId = objItem.ItemId,
                                                              ItemName = objItem.ItemName,
                                                              ItemPrice = objItem.ItemPrice,
                                                              Description = objItem.Description,
                                                              Category = objCate.CategoryName
                                                          }
            ).ToList();          
            return View(listOfItems);
        }
        [HttpPost]
        public JsonResult Index(string ItemId)
        {
            ShoppingCartModel addItem = new ShoppingCartModel();
            Item choosenItem = objECartDB.Items.Single(item => item.ItemId.ToString() == ItemId);
            if (Session["CartCounter"]!=null)
            {
                listOfShoppingCartModels = Session["CartItems"] as List<ShoppingCartModel>;
            }
            if(listOfShoppingCartModels.Any(x=> x.ItemId == ItemId))
            {
                addItem = listOfShoppingCartModels.Single(x => x.ItemId == ItemId);
                addItem.Quantity += 1;
                addItem.Total *= addItem.Quantity;
            }
            else
            {
                addItem.ItemId = ItemId;
                addItem.ItemName = choosenItem.ItemName;
                addItem.Quantity = 1;
                addItem.Total = choosenItem.ItemPrice;
                addItem.ImagePath = choosenItem.ImagePath;
                addItem.UnitPrice = choosenItem.ItemPrice;
                listOfShoppingCartModels.Add(addItem);
            }
            Session["CartCounter"] = listOfShoppingCartModels.Count();
            Session["CartItems"] = listOfShoppingCartModels;
            return Json(new { Success = "true", Counter = listOfShoppingCartModels.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShoppingCart()
        {
            listOfShoppingCartModels = Session["CartItems"] as List<ShoppingCartModel>;
            return View(listOfShoppingCartModels);
        }

        [HttpPost]
        public ActionResult SaveOrder()
        {
            int OrderId = 0;
            listOfShoppingCartModels = Session["CartItems"] as List<ShoppingCartModel>;
            Order orderList = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = String.Format("{0:ddmmyyyyHHmmsss}",DateTime.Now)
            };
            objECartDB.Orders.Add(orderList);
            objECartDB.SaveChanges();
            OrderId = orderList.OrderId;
            foreach(var item in listOfShoppingCartModels)
            {
                OrderDetail orderDetails = new OrderDetail()
                {
                    ItemId = item.ItemId,
                    OrderId = OrderId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Total = item.Total,              
                };
                objECartDB.OrderDetails.Add(orderDetails);
                objECartDB.SaveChanges();
            }
            Session["CartItem"] = null;
            Session["CartCounter"] = null;
            return RedirectToAction("Index");
        }
    }
}