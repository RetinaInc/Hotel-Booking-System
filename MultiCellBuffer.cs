using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DsodAsgmnt2
{
    //this class is used as the communucation meedium between the supplier and agency.
    class MultiCellBuffer
    {
        const Int32 buffLength = 2;
        static string[] buffCell = new string[buffLength];
        static Semaphore buffFull = new Semaphore(1, 1);
        static Semaphore buffEmpty = new Semaphore(0, 1);
        static Int32 read = 0;
        static Int32 write = 0;
        static Object lckObj = new Object();

        public static void setOneCell(string msg)
        {
            try
            {
                buffFull.WaitOne();
                lock (lckObj)
                {
                    write %= buffCell.Length;
                    buffCell[write] = msg;
                    write++;
                }
                buffEmpty.Release();
            }
            catch (Exception err)
            {
                Console.WriteLine("\n Error occurred during saving the message." + err.Message);
            }
        }

        public static string getOneCell()
        {
            string msg = "";
            try
            {
                buffEmpty.WaitOne();
                lock(lckObj)
                {
                    read %= buffCell.Length;
                    msg = buffCell[read];
                    read++;
                }
                buffFull.Release();
                return msg;
            }
            catch (Exception err)
            {
                Console.WriteLine("\n Error occurred during retrieval of the message " + err.Message);
                return "error!";
            }
        }
    }
}
