using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Net;

using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.Web.Mvc;
using MVC_AIPay.Models;

namespace MVC_AIPay.Controllers
{
    public class HomeController : Controller
    {
        Payrequest PR = new Payrequest();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            string Tok_id = "";


            try
            {

                string passphrase = "A4476C2062FFA58980DC8F79EB6A799E";
                string salt = "A4476C2062FFA58980DC8F79EB6A799E";
                string passphrase1 = "75AEF0FA1B94B3C10D4F5B268F757F11";
                string salt1 = "75AEF0FA1B94B3C10D4F5B268F757F11";
                byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                int iterations = 65536;
                string mid = "317157";
                string uId = "317157";
                string pwd = "Test@123";
                string mtxnid = "39";
                string mtxndate = "2023-05-24 17:41:27";
                string amt = "200";
                string prod = "Multi";
                string prd = "NSE";
                string txncur = "INR";
                string cacc = "5464567453453435";
                string p1 = "NSE";
                string pamt1 = "100";
                string p2 = "AIPAY";
                string pamt2 = "100";
                string cmail = "suresh.kore@atomtech.in";
                string cmob = "9404336081";
                string u1 = "3979";
                string u2 = "";
                string u3 = "";
             
                string json = "{\"payInstrument\":{\"headDetails\":{\"version\":\"OTSv1.1\",\"api\":\"AUTH\",\"platform\":\"FLASH\"},\"merchDetails\":{\"merchId\":\"" + mid + "\",\"userId\":\"" + uId + "\",\"password\":\"" + pwd + "\",\"merchTxnId\":\"" + mtxnid + "\",\"merchTxnDate\":\"" + mtxndate + "\"},\"payDetails\":{\"amount\":\"" + amt + "\",\"product\":\"" + prd + "\",\"txnCurrency\":\"" + txncur + "\",\"custAccNo\":\"" + cacc + "\"},\"custDetails\":{\"custEmail\":\"" + cmail + "\",\"custMobile\":\"" + cmob + "\"},\"extras\":{\"udf1\":\"" + u1 + "\",\"udf2\":\"\",\"udf3\":\"\",\"udf4\":\"\",\"udf5\":\"\",\"udf21\":\"1111~NTTDATA~http://nttdatapay.in\"}}}"; ///singleprod
                // string json = "{\"payInstrument\":{\"headDetails\":{\"version\":\"OTSv1.1\",\"api\":\"AUTH\",\"platform\":\"FLASH\"},\"merchDetails\":{\"merchId\":\"" + mid + "\",\"userId\":\"" + uId + "\",\"password\":\""+pwd+"\",\"merchTxnId\":\"" + mtxnid + "\",\"merchTxnDate\":\"" + mtxndate + "\"},\"payDetails\":{\"amount\":\"" + amt + "\",\"product\":\"" + prod + "\",\"txnCurrency\":\"" + txncur + "\",\"custAccNo\":\"" + cacc + "\",\"prodDetails\":[{\"prodName\":\"" + p1 + "\",\"prodAmount\":\"" + pamt1 + "\"},{\"prodName\":\"" + p2 + "\",\"prodAmount\":\"" + pamt2 + "\"}] },\"custDetails\":{\"custEmail\":\"" + cmail + "\",\"custMobile\":\"" + cmob + "\"},\"extras\":{\"udf1\":\"" + u1 + "\",\"udf2\":\"\",\"udf3\":\"\",\"udf4\":\"\",\"udf5\":\"\",\"udf21\":\"5999~YEIDA~http://yeida.solarman.in\"}}}"; ///multiprod
            
                string Encryptval = Encrypt(json, passphrase, salt, iv, iterations);

                string testurleq = "https://caller.atomtech.in/ots/aipay/auth?merchId=317157&encData=" +Encryptval;
                ServicePointManager.Expect100Continue = true;
              //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testurleq);
                request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(json);
                request.ProtocolVersion = HttpVersion.Version11;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                //request.Timeout = 600000;
                Stream stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                //Console.WriteLine(stream);
                // Console.WriteLine(json);
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string jsonresponse = response.ToString();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                ////  string jsonresponse = post;
                string temp = null;
                string status = "";
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonresponse += temp;
                }
                //InitiateOrderResEq.RootObject objectres = new InitiateOrderResEq.RootObject();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var result = jsonresponse.Replace("System.Net.HttpWebResponse", "");
                var uri = new Uri("http://atom.in?" + result);
                var query = HttpUtility.ParseQueryString(uri.Query);

                string encData = query.Get("encData");
                string Decryptval = decrypt(encData, passphrase1, salt1, iv, iterations);
                 Payverify objectres = new Payverify();
                objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify>(Decryptval);
                string txnMessage = objectres.responseDetails.txnMessage;
                Tok_id = objectres.atomTokenId;  
                ViewBag.Status = Tok_id;
                 return View("Index");
            }

            catch (Exception ex)
            {


            }

            return View();
        }

  


     public String Encrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        string data = ByteArrayToHexString(Encrypt(plainBytes, GetSymmetricAlgorithm(passphrase, salt, iv, iterations))).ToUpper();


        return data;
    }

     public byte[] Encrypt(byte[] plainBytes, SymmetricAlgorithm sa)
        {
            return sa.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

        }
     public String decrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
    {
        byte[] str = HexStringToByte(plainText);

        string data1 = Encoding.UTF8.GetString(decrypt(str, GetSymmetricAlgorithm(passphrase, salt, iv, iterations)));
        return data1;
    }

    public byte[] decrypt(byte[] plainBytes, SymmetricAlgorithm sa)
    {
        return sa.CreateDecryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    }
    public SymmetricAlgorithm GetSymmetricAlgorithm(String passphrase, String salt, Byte[] iv, int iterations)
    {
        var saltBytes = new byte[16];
        var ivBytes = new byte[16];
        Rfc2898DeriveBytes rfcdb = new System.Security.Cryptography.Rfc2898DeriveBytes(passphrase, Encoding.UTF8.GetBytes(salt), iterations,HashAlgorithmName.SHA512);
        saltBytes = rfcdb.GetBytes(32);
        var tempBytes = iv;
        Array.Copy(tempBytes, ivBytes, Math.Min(ivBytes.Length, tempBytes.Length));
        var rij = new RijndaelManaged();
        rij.Mode = CipherMode.CBC;
        rij.Padding = PaddingMode.PKCS7;
        rij.FeedbackSize = 128;
        rij.KeySize = 128;

        rij.BlockSize = 128;
        rij.Key = saltBytes;
        rij.IV = ivBytes;
        return rij;
    }
    protected static byte[] HexStringToByte(string hexString)
    {
        try
        {
            int bytesCount = (hexString.Length) / 2;
            byte[] bytes = new byte[bytesCount];
            for (int x = 0; x < bytesCount; ++x)
            {
                bytes[x] = Convert.ToByte(hexString.Substring(x * 2, 2), 16);
            }
            return bytes;
        }
        catch
        {
            throw;
        }
    }
    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

        public class Payverify
        {
            public string atomTokenId { get; set; }

            public ResponseDetails responseDetails { get; set; }
            
        }
        public class ResponseDetails
        {
            public string txnStatusCode { get; set; }
            public string txnMessage { get; set; }
            public string txnDescription { get; set; }


        }

    }
}