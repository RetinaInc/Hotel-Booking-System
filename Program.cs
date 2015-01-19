using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace DsodAsgmnt2
{
    class Program    // main class which starts the execution
    {
        static void Main(string[] args)
        {
            Console.SetBufferSize(85, 725);
            try
            {
                Console.WriteLine("******* HOTEL BLOCK BOOKING SYSTEM *******\n");
                Console.WriteLine("\n  1 Hotel Supllier\n  5 Travel Agencies\n");
                Thread hotelSup = new Thread(new ThreadStart(HotelSupplier.supFunc));
                hotelSup.Name = "HotelSupplier";
                HotelSupplier.priceCut += new HotelSupplier.PriceCutEvent(TravelAgency.bookRooms);
                Thread[] taThreads = new Thread[5];
                for (Int32 i = 0; i < 5; i++)
                {
                    taThreads[i] = new Thread(new ThreadStart(TravelAgency.taFunc));
                    taThreads[i].Name = "TravelAgency" + (i + 1).ToString();
                    taThreads[i].Start();
                }
                hotelSup.Start();
                hotelSup.Join();
            }
            catch (Exception a)
            {
                Console.WriteLine("Execption in main" + a.Message);
            }
        }
    }
}
