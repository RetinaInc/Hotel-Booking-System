using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DsodAsgmnt2
{
    //It stores the retailer details and order details and helps the server to access these details.
    
    class OrderClass
    {
        private string senderId;
        private Int32 cardNo;
        private Int32 rooms;
        public DateTime orderStartTime { get; set; }

        public OrderClass(string senderId, Int32 cardNo, Int32 rooms, DateTime orderStartTime)
        {
            this.senderId = senderId;
            this.cardNo = cardNo;
            this.rooms = rooms;
            this.orderStartTime = orderStartTime;
        }
        public string GetSenderId()
        {
            return senderId;
        }
        public void SetSenderId(string senderId)
        {
            this.senderId = senderId;
        }

        public Int32 GetCardNo()
        {
            return cardNo;
        }
        public void SetCardNo(Int32 cardNo)
        {
            this.cardNo = cardNo;
        }

        public Int32 GetNoRooms()
        {
            return rooms;
        }
        public void SetNoRooms(Int32 rooms)
        {
            this.rooms = rooms;
        }
    }
}
