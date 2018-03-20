using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace ServicioAMTR
{
    /// <summary>
    /// Summary description for Servicios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Servicios : System.Web.Services.WebService
    {
        //public void LeerXml()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(@"C:\Users\adria\Documents\Visual Studio 2017\Projects\ConsoleApp3\ConsoleApp3\XMLFile1.xml");
        //    //doc.LoadXml(Directory.GetCurrentDirectory() + "\\XMLFile1.xml");

        //    ServicioAMTR(doc);
        //    //return doc;
        //}

        [WebMethod]
        public XmlDocument ServicioAMTR(XmlDocument doc)
        {
            string str = "al";
            XmlDocument xml = new XmlDocument();
            Conexion conexion = new Conexion();
            xml = conexion.LeerXml();

            XmlNodeList nodeList = xml.SelectNodes("//Info/*");
            List<Item> Items = new List<Item>();

            ProcesaXml procesaXml = new ProcesaXml();

            if (nodeList.Count > 0)
            {
                Items = procesaXml.EmparejarTags(nodeList);
            }

            XmlDocument Respuesta = new XmlDocument();

            Respuesta = procesaXml.Respuesta(xml, Items);

            return Respuesta;
        }
    }
}
