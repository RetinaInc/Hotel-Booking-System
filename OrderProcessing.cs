using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DsodAsgmnt2
{
    //this class is used for processing the orders. It is used by the hotel supplier class
    class OrderProcessing
    {
        static Int32 tax = 60;
        static Int32 amount = 0;
        static Object lckObj = new Object();
        public static void orderProcess(OrderClass orderObj, Int32 price)
        {
            try
            {
                lock (lckObj)
                {
                    if (orderObj.GetCardNo() >= 5000 && orderObj.GetCardNo() <=7000)
                    {
                        amount = price + tax;
                        Console.WriteLine("\nOrder Details:\n {0} placed a order for {1} rooms. Total Price = ${2}\n", orderObj.GetSenderId(), orderObj.GetNoRooms(), amount);
                        TravelAgency.orderConfirm(orderObj);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("\n error occured in processing the order" + err.Message);
            }
        }
    }
}
