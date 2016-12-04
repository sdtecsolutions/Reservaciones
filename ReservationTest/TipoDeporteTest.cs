using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace ReservationTest
{
    [TestClass]
    public class TipoDeporteTest
    {
        [TestMethod]
        public void Test1()
        {
            // Prueba de creación de alumno vía HTTP POST
            string postdata = "{\"COD_TIPO_DEPO\":6,\"ALF_TIPO_DEPO\":\"yyy\"}"; //JSON
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:2588/ServiceApp/TipoDeporte.svc/TipoDeporte");
            req.Method = "POST";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream());
            string tipodeporteJson = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            BETipoDeporte tipodeporteCreado = js.Deserialize<BETipoDeporte>(tipodeporteJson);
            Assert.AreEqual(6, tipodeporteCreado.COD_TIPO_DEPO);
            Assert.AreEqual("yyy", tipodeporteCreado.ALF_TIPO_DEPO);
        }

        [TestMethod]
        public void Test2()
        {
            // Prueba de creación de tipo de deporte repetido vía HTTP POST
            string postdata = "{\"COD_TIPO_DEPO\":3,\"ALF_TIPO_DEPO\":\"Tenis\"}"; //JSON
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:2588/ServiceApp/TipoDeporte.svc/TipoDeporte/");
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
                string tipodeporteJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                BETipoDeporte tipodeporteCreado = js.Deserialize<BETipoDeporte>(tipodeporteJson);
                Assert.AreEqual(3, tipodeporteCreado.COD_TIPO_DEPO);
                Assert.AreEqual("Tenis", tipodeporteCreado.ALF_TIPO_DEPO);
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("404", mensaje);
            }

        }
        //[TestMethod]
        //public void Test2()
        //{
        //    // Prueba de obtención de alumno vía HTTP GET
        //    string postdata = "{\"COD_TIPO_DEPO\":3,\"ALF_TIPO_DEPO\":\"Tenis\"}"; //JSON
        //    byte[] data = Encoding.UTF8.GetBytes(postdata);
        //    HttpWebRequest req2 = (HttpWebRequest)WebRequest
        //        .Create("http://localhost:2588/ServiceApp/TipoDeporte.svc/TipoDeporte/3");
        //    req2.Method = "GET";
        //    HttpWebResponse res2 = (HttpWebResponse)req2.GetResponse();
        //    StreamReader reader2 = new StreamReader(res2.GetResponseStream());
        //    string tipodeporteJson2 = reader2.ReadToEnd();
        //    JavaScriptSerializer js2 = new JavaScriptSerializer();
        //    BETipoDeporte tipodeporteObtenido = js2.Deserialize<BETipoDeporte>(tipodeporteJson2);
        //    Assert.AreEqual(3, tipodeporteObtenido.COD_TIPO_DEPO);
        //    Assert.AreEqual("Tenis", tipodeporteObtenido.ALF_TIPO_DEPO);
        //}
        [TestMethod]
        public void Test3()
        {
            // Prueba de modificacion de alumno vía HTTP PUT
            string postdata = "{\"COD_TIPO_DEPO\":6,\"ALF_TIPO_DEPO\":\"Golf\"}"; //JSON
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req3 = (HttpWebRequest)WebRequest
                .Create("http://localhost:2588/ServiceApp/TipoDeporte.svc/TipoDeporte");
            req3.Method = "PUT";
            req3.ContentLength = data.Length;
            req3.ContentType = "application/json";
            var reqStream3 = req3.GetRequestStream();
            reqStream3.Write(data, 0, data.Length);
            HttpWebResponse res3 = (HttpWebResponse)req3.GetResponse();
            StreamReader reader3 = new StreamReader(res3.GetResponseStream());
            string tipodeporteJson3 = reader3.ReadToEnd();
            JavaScriptSerializer js3 = new JavaScriptSerializer();
            BETipoDeporte tipodeporteModificado = js3.Deserialize<BETipoDeporte>(tipodeporteJson3);
            Assert.AreEqual(6, tipodeporteModificado.COD_TIPO_DEPO);
            Assert.AreEqual("Golf", tipodeporteModificado.ALF_TIPO_DEPO);

        }

        [TestMethod]
        public void Test4()
        {
            // Prueba de eliminar de alumno vía HTTP DELETE
            string postdata = "{\"COD_TIPO_DEPO\":6,\"ALF_TIPO_DEPO\":\"Golf\"}"; //JSON
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req4 = (HttpWebRequest)WebRequest
                .Create("http://localhost:2588/ServiceApp/TipoDeporte.svc/TipoDeporte/6");
            req4.Method = "DELETE";
            HttpWebResponse res4 = (HttpWebResponse)req4.GetResponse();
            StreamReader reader4 = new StreamReader(res4.GetResponseStream());
            string tipodeporteJson4 = reader4.ReadToEnd();
            JavaScriptSerializer js4 = new JavaScriptSerializer();

            //HttpWebRequest req2 = (HttpWebRequest)WebRequest
            //.Create("http://localhost:2588/ServiceApp/TipoDeporte.svc/TipoDeporte/6");
            //req2.Method = "GET";
            //HttpWebResponse res2 = (HttpWebResponse)req2.GetResponse();
            //StreamReader reader2 = new StreamReader(res2.GetResponseStream());
            //string tipodeporteJson2 = reader2.ReadToEnd();
            //JavaScriptSerializer js2 = new JavaScriptSerializer();
            //BETipoDeporte tipodeporteObtenido = js2.Deserialize<BETipoDeporte>(tipodeporteJson2);
            //Assert.AreEqual(null, tipodeporteObtenido);

        }
    }
}
