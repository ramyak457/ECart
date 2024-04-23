using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECartWebApp.ViewModel
{
    public class ShoppingCartModel
    {
        public string ItemId { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal UnitPrice { get; set; }
        public Decimal Total { get; set; }
        public string ImagePath { get; set; }
        public string ItemName { get; set; }
    }
}