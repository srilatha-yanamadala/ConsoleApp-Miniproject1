using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_miniproject
{
    public class Device

    {

        public Device(string modelName, double price, DateTime purchaseDate, string devicetype,string office)
        {
            ModelName = modelName;
            Price = price;
            PurchaseDate = purchaseDate;
            Devicetype = devicetype;
            Office = office;
        }

        public string ModelName { get; set; }
        public double Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Devicetype { get; set; }
        public string Office { get; set; }

       

        
    }




}
