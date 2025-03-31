using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_AIPay.Models
{
    public class Payrequest
    {
        public HeadDetails headDetails { get; set; }
        // public MsgBdy msgBdy { get; set; }
        public MerchDetails merchDetails { get; set; }
        public PayDetails payDetails { get; set; }
        public CustDetails custDetails { get; set; }
        public Extras extras { get; set; }





    }
    public class HeadDetails
    {
        public string version { get; set; }
        public string api { get; set; }
        public string platform { get; set; }

    }
    public class MerchDetails
    {

        public string merchId { get; set; }
        public string userId { get; set; }
        public string password { get; set; }
        public string merchTxnDate { get; set; }
        public string merchTxnId { get; set; }


    }
    public class PayDetails
    {
        public string amount { get; set; }
        public string product { get; set; }
        public string custAccNo { get; set; }
        public string txnCurrency { get; set; }




    }
    public class CustDetails
    {
        public string custEmail { get; set; }
        public string custMobile { get; set; }


    }
    public class Extras
    {
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }

    }

    public class MsgBdy
    {
        public HeadDetails headDetails { get; set; }
        public MerchDetails merchDetails { get; set; }
        public PayDetails payDetails { get; set; }
        public CustDetails custDetails { get; set; }
        public Extras extras { get; set; }




    }


    public class RootObject
    {
        public Payrequest payInstrument { get; set; }
    }
}