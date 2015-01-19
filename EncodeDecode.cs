using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DsodAsgmnt2
{
    class EncodeDecode
    {
        public static String encode(OrderClass orderObj)
        {
            EncDecSvcRef.ServiceClient svc1 = new EncDecSvcRef.ServiceClient();
            String msg = orderObj.GetSenderId().ToString() + "#" + orderObj.GetCardNo().ToString() + "#" + orderObj.GetNoRooms().ToString() + "#" + orderObj.orderStartTime.ToString();
            msg = svc1.Encrypt(msg);
            return msg;
        }

        public static OrderClass decode(String encMsg)
        {
            EncDecSvcRef.ServiceClient svc2 = new EncDecSvcRef.ServiceClient();
            String decMsg = svc2.Decrypt(encMsg);
            String[] msg = decMsg.Split('#');
            Int32 a = Convert.ToInt32(msg[1]);
            Int32 b = Convert.ToInt32(msg[2]);
            DateTime c = Convert.ToDateTime(msg[3]);
            return new OrderClass(msg[0], a, b, c);
        }
    }
}
