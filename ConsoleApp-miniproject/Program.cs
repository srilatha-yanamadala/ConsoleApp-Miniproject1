using ConsoleApp_miniproject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkpoint2
{

    class Program
    {
        static void Main(string[] args)
        {
            string deviceType = "";

            //Create a list of Category objects, ask user to entery category names one by one, add them to the list.
            List<Device> computerList = new List<Device>();
            List<Device> mobileList = new List<Device>();
            string ModelName = "";
            double price = 0;
            DateTime PurchaseDate = default;
            
            while (!deviceType.Trim().ToUpper().Equals("Q"))
            {
                deviceType = "";
                Console.WriteLine("\n Please enter your device type (laptop/mobile)::press q for exit.");
                deviceType = Console.ReadLine();

                if (!deviceType.Trim().ToUpper().Equals("Q") &&
                    (deviceType.Trim().ToUpper().Equals("LAPTOP") || deviceType.Trim().ToUpper().Equals("MOBILE")))
                {
                    ModelName = "";
                    while (!ModelName.Trim().ToUpper().Equals("Q"))
                    {
                        Console.WriteLine("\t");
                        Console.WriteLine("Please enter the Model NAME of your " + deviceType);
                        ModelName = Console.ReadLine();

                        if (!ModelName.Trim().ToUpper().Equals("Q"))
                        {
                            Console.WriteLine($"Enter the {ModelName} PRICE in USD $ :");
                            price = double.Parse(Console.ReadLine());

                            Console.WriteLine($"Enter the {ModelName} PURCHASE DATE (yyyy-mm-dd) :");
                            String purchaseDateStr = Console.ReadLine();
                            PurchaseDate = Convert.ToDateTime(purchaseDateStr);

                            Console.WriteLine($"Enter your reporting OFFICE location (Sweden/India/USA):");
                            String officeLocation = Console.ReadLine();


                            if (deviceType.Equals("laptop") || deviceType.Equals("mobile"))
                                computerList.Add(new Device(ModelName, price, PurchaseDate, deviceType, officeLocation));
                            else
                                Console.WriteLine(" Please enter proper device type. (laptop or mobile are only allowed) ");
                           
                        }
                    }

                }
            } //End of first While

            //Sort the list by their price
            if (computerList.Count > 0)
            {
                List<Device> sortedComputerList = computerList.OrderBy(device => device.Office).ThenBy(device => device.PurchaseDate).ToList();
                printListContents(sortedComputerList);

            }
            if (mobileList.Count > 0)
            {
                List<Device> sortedMobileList = mobileList.OrderBy(device => device.Office).ThenBy(device => device.PurchaseDate).ToList();
                printListContents(sortedMobileList);
            }

        }  //End of main

        public static void printListContents(List<Device> printList)
        {
            Console.WriteLine("\n\n ################################### ");

            foreach (Device element in printList)
            {
                Console.WriteLine($"\n\n\t Your {element.Devicetype} brand is:" + element.ModelName.ToUpper());
                convertCurrencyAndPrint(element);
                CheckForDateValidityAndPrint(element);
            }
        }


        public static void convertCurrencyAndPrint(Device device)
        {
            if (device.Office.Trim().ToUpper().Equals("INDIA"))
            {
                double inr = device.Price * 73.48;
                Console.WriteLine($"\n\t Your {device.ModelName.ToUpper()} {device.Devicetype} price in USD is:: $"+device.Price+" and the price in "+device.Office.ToUpper()+" is:: INR " + inr);
            }
            else if (device.Office.Trim().ToUpper().Equals("SWEDEN"))
            {
                double sek = device.Price * 8.2;
                Console.WriteLine($"\n\t Your {device.ModelName.ToUpper()} {device.Devicetype} price is::" + sek+" SEK");
            }
            else
            {
                Console.WriteLine($"\n\t Your {device.ModelName.ToUpper()} {device.Devicetype} price is:: $" + device.Price);
            }

        }
        public static void CheckForDateValidityAndPrint(Device element)
        {
            String colorValue = GetApplicableColorAfterExpiryDateCheck(element.PurchaseDate);
            //Set default value to White
            Console.ForegroundColor = ConsoleColor.White;
            if (colorValue.Length > 0)
            {
                if (colorValue.Equals("RED"))
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (colorValue.Equals("YELLOW"))
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else
                    Console.ForegroundColor = ConsoleColor.White;
            }
  
            Console.WriteLine($"\n\t##### Your {element.ModelName.ToUpper()} {element.Devicetype} purchase date is::" + element.PurchaseDate,Console.ForegroundColor);
            //Reset it back to White, so that other console write statement won't get affected.
            Console.ForegroundColor = ConsoleColor.White;
        }


        //If Purchase date is: 2017-01-01 or 2017-09-03  then it is getting expired in 3 months and the color is RED
        //If Purchase date is: 2018-05-05 or 2018-06-06  then it is getting expired in 6 moths and the color is YELLOW
        //If Purchase date is: 2019-01-01  then it is VALID (Less than 3 years since purchased)
        public static string GetApplicableColorAfterExpiryDateCheck(DateTime purchaseDate)
        {
            String returnColor = "";
            DateTime expiryDate = purchaseDate.AddYears(3);
            DateTime todayDate = DateTime.Today;

            TimeSpan timeSpan = expiryDate - todayDate;
            double days = timeSpan.TotalDays;
            int daysLeft = Convert.ToInt32(days);

            Console.Write("\tDays left ::" + daysLeft+" till the expiry date");

            if (daysLeft > 90 && daysLeft < 180)
                returnColor= "YELLOW";
            else if (daysLeft < 90)
                returnColor= "RED";
            else
                returnColor = "WHITE";

            return returnColor;
        }
    }
}