using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DsodAsgmnt2
{
    //the supplier class which processes the orders of the travel agencies placed in the multicell buffer and also it generates pricecut events.

    class HotelSupplier
    {
        public delegate void PriceCutEvent(Int32 price); 
        public static event PriceCutEvent priceCut;
        public static Random randomRoom = new Random();
        public static Int32 p = 0;
        public static Int32 hotelPrice = 1000;
        public static Int32 oldhotelPrice = 1000;
        static object lckObj = new object();

        public static void supFunc()
        {
            try
            {
                while(true)
                {
                    Thread.Sleep(1000);
                    lock(lckObj)
                    {
                        if (p >= 10)
                        {
                            Console.WriteLine("\n\n Maximum price cuts reached.\n No more blocking.\n Exit\n");
                            break;
                        }
                        // Take the order from the queue of the orders;
                        //Console.WriteLine("check");
                        string encMsg = MultiCellBuffer.getOneCell();
                        OrderClass orderObj = EncodeDecode.decode(encMsg);
                        //Console.WriteLine("{0}", encMsg);
                        // Change the price
                        Int32 newhotelPrice = pricingModel();
                        changehotelPrice(newhotelPrice);

                        // Processing the already taken order
                        
                        Thread ordrprocThrd = new Thread(() => OrderProcessing.orderProcess(orderObj, hotelPrice));
                        ordrprocThrd.Start();
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("\n Error in Hotel Supplier" + err.Message);
            }
        }
        
        public static int pricingModel()
        {
            return randomRoom.Next(800, 1400);
        }

        public static void changehotelPrice(Int32 newhotelPrice)
        {
            if (newhotelPrice < hotelPrice)
            {
                    p++;
                    priceCut(newhotelPrice);
            }
            oldhotelPrice = hotelPrice;
            hotelPrice = newhotelPrice;
        }
    }
}
