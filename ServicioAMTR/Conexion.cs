using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace ServicioAMTR
{
    public class Conexion
    {
        public XmlDocument LeerXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\adria\Documents\Visual Studio 2017\Projects\ServicioAMTR\ServicioAMTR\Respuesta4.xml");
            //doc.LoadXml(Directory.GetCurrentDirectory() + "\\XMLFile1.xml");

            //ServicioAMTR(doc);
            return doc;
        }
        //public XmlDocument ConetarServicio()
        //{
        //    XmlDocument n = new XmlDocument();

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://localhost:55386/Services.asmx");
        //    string postData = "This is a test that posts this string to a Web server.";
        //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //    request.Accept = "gzip,deflate";
        //    request.ContentType = "application/soap+xml;charset=UTF-8;";
        //    request.Method = "POST";
        //    request.UserAgent = "Apache-HttpClient/4.1.1 (java 1.5)";
        //    request.ContentLength = 326;//byteArray.Length;

        //    //Stream dataStream = request.GetRequestStream();
        //    //dataStream.Write(byteArray, 0, byteArray.Length);
        //   //dataStream.Close();
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    Stream receiveStream = response.GetResponseStream();
        //    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

        //    //dataStream = response.GetResponseStream();
        //    //StreamReader reader = new StreamReader(dataStream);

        //    //reader.Close();
        //    //dataStream.Close();
        //    response.Close();
        //    readStream.Close();

        //    return n;
        //}

        public XmlDocument ConetarServicio()
        {
            XmlDocument n = new XmlDocument();

            string reqString = "Servicion Web";
            byte[] requestData = Encoding.UTF8.GetBytes(reqString);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://localhost:55386/Services.asmx?WSDL");
            request.Proxy = null;
            //request.CookieContainer = cc;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = requestData.Length;
            request.ContentType = "application / soap + xml; charset = UTF - 8;";

            using (Stream s = request.GetRequestStream())
                s.Write(requestData, 0, requestData.Length);

            using (var response = (HttpWebResponse)request.GetResponse())

                return n;
        }

        //public XmlDocument ConetarServicio()
        //{
        //    XmlDocument n = new XmlDocument();

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://localhost:55386/Services.asmx");
        //    request.BeginGetResponse(new AsyncCallback(ProcesaRequest), request);

        //    return n;
        //}

        private void ProcesaRequest(IAsyncResult ar)
        {
            HttpWebRequest request = ar.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(ar) as HttpWebResponse;

            foreach (Cookie c in response.Cookies)
            {
                Console.WriteLine(" Name: " + c.Name);
                Console.WriteLine(" Value: " + c.Value);
                Console.WriteLine(" Path: " + c.Path);
                Console.WriteLine(" Domain: " + c.Domain);
            }
        }
    }
}