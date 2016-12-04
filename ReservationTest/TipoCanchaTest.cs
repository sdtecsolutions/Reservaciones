using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace ReservationTest
{
    [TestClass]
    public class TipoCanchaTest
    {
        [TestMethod]
        public void Test1()
        {
            // Prueba de creación de Tipo de deporte vía HTTP POST
            string postdata = "{\"COD_TIPO_CANC\":12,\"ALF_TIPO_CANC\":\"DESCONOCIDO\",\"COD_TIPO_DEPO\":\"1\",\"ALF_TIPO_DEPO\":\"Futbol\",\"NUM_JUGA\":\"7\",\"MON_PREC\":\"80.00\"}"; //JSON
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:2588/ServiceApp/TipoCancha.svc/tipocanchas");
            req.Method = "POST";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream());
            string tipocanchaJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            BETipoCancha tipocanchaCreado = js.Deserialize<BETipoCancha>(tipocanchaJson);           
            Assert.AreEqual("DESCONOCIDO", tipocanchaCreado.ALF_TIPO_CANC);
        }

        [TestMethod]
        public void Test2()
        {
            // Prueba de creación de tipo de deporte repetido vía HTTP POST
            string postdata = "{\"COD_TIPO_CANC\":12,\"ALF_TIPO_CANC\":\"DESCONOCIDO\",\"COD_TIPO_DEPO\":\"1\",\"ALF_TIPO_DEPO\":\"Futbol\",\"NUM_JUGA\":\"7\",\"MON_PREC\":\"80.00\"}"; //JSON
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:2588/ServiceApp/TipoCancha.svc/tipocanchas");
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
