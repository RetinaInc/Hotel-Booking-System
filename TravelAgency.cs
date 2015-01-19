using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DsodAsgmnt2
{
    // this class contains the function of the travel agents and it keeps tha order into the multicell buffer for processing by the hotel supplier.
    class TravelAgency
    {
        static Random noOfRooms = new Random();
        static Random randomcardNo = new Random();
        static Int32 roomNo;
        static Int32 cardNo;
        static Object lckObj = new Object();

        public static void taFunc()
        {
            try
            {
                while (true)
                {
                    lock(lckObj)
                    {
                        if (HotelSupplier.oldhotelPrice < HotelSupplier.hotelPrice)
                            roomNo = noOfRooms.Next(2, 5);
                        else
                            roomNo = noOfRooms.Next(5, 9);

                        cardNo = randomcardNo.Next(5000, 7000);
                        OrderClass orderObj = new OrderClass(Thread.CurrentThread.Name, cardNo, roomNo, DateTime.Now);
                        string msg = EncodeDecode.encode(orderObj);
                        MultiCellBuffer.setOneCell(msg);
                        Console.WriteLine("\n{0} has sent an order of {1} rooms.\n", Thread.CurrentThread.Name, roomNo);
                    }
                    Thread.Sleep(3000);
                }
            }
            catch(Exception err)
            {
                Console.WriteLine("\n error occured in travel agency class" + err.Message);
            }
        }

        public static void bookRooms(Int32 hotelPrice)
        {
            try
            {
              
                Console.WriteLine("\n\n ********* Hotel Rooms Price Cut to ${0} *********\n\n", hotelPrice);
            }
            catch (Exception err)
            {
                Console.WriteLine("\n Error occured in event handling" + err.Message);
            }
        }
    
        public static void orderConfirm(OrderClass orderObj)
        {
            try
            {
                Console.WriteLine("\nOrder Receipt:\n Order Processed for {0} and recieved confirmation for {1} rooms.\n", orderObj.GetSenderId(), orderObj.GetNoRooms()); //displaying the order receipt
                Console.WriteLine("Time taken for the order to be processed  = {0} seconds\n", System.Math.Round((DateTime.Now - orderObj.orderStartTime).TotalSeconds, 2)); //calculating the time taken for each order and displaying it
            }
            catch (Exception err)
            {
                Console.WriteLine("\n Error occurred during order confirmation. " + err.Message);
            }
        }
    }
}
