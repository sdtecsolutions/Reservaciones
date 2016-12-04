using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;

namespace ReservationTest
{
    [TestClass]
    public class ReservasTest
    {
        [TestMethod]
        public void Test1()
        {
            // Prueba de creación de Reserva
            string postdata = "{\"COD_RESE\":12,\"COD_PEDI\":\"9\",\"MON_PAGA\":\"20\",\"MON_PAGO\":\"110\",\"MON_DEUD\":\"0\",\"IND_ESTA\":\"D\"}"; //JSON
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:2588/ServiceApp/Reserva.svc/reservas");
            req.Method = "POST";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            HttpWebResponse res = null;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string tipocanchaJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                BETipoCancha tipocanchaCreado = js.Deserialize<BETipoCancha>(tipocanchaJson);


            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("Registro duplicado", mensaje);
            }
        }

       
    }
    
}
