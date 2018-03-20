using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace ServicioAMTR
{
    public class ProcesaXml
    {

        public List<Item> EmparejarTags(XmlNodeList nodeList)
        {
            List<Item> Nodos = new List<Item>();

            foreach (XmlNode node in nodeList)
            {
                Item Nodo = new Item();
                int CantidadNodos = nodeList.Count;
                string NombreNodo = node.Name;
                int i = NombreNodo.Length;

                string numero = NombreNodo.Substring(i - 3);
                bool EsNumero = VerifiaSiEsNumero(numero);

                if (!EsNumero)
                {
                    numero = NombreNodo.Substring(i - 2);
                    EsNumero = VerifiaSiEsNumero(numero);
                }

                if (EsNumero)
                {
                    if (Nodos.Count == 0)
                    {
                        Nodo.Numero = Convert.ToInt32(numero);
                        Nodo.Valor1 = NombreNodo;
                        Nodos.Add(Nodo);
                    }
                    else if (Nodos.Count > 0)
                    {
                        bool Encontrado = false;
                        bool Agregado = false;
                        int CantidadNodosAgregados = Nodos.Count;

                        for (int j = 0; j < CantidadNodosAgregados; j++)
                        {
                            if (Nodos[j].Numero == Convert.ToInt32(numero))
                            {
                                Encontrado = true;
                                if (Nodos[j].Valor2 == null)
                                {
                                    Nodos[j].Valor2 = NombreNodo;
                                    Agregado = true;
                                }
                                if (Nodos[j].Valor3 == null && !Agregado)
                                {
                                    Nodos[j].Valor3 = NombreNodo;
                                    Agregado = true;
                                }
                                if (Nodos[j].Valor4 == null && !Agregado)
                                {
                                    Nodos[j].Valor4 = NombreNodo;
                                }
                            }

                            if (Encontrado)
                            {
                                break;
                            }
                        }

                        if (!Encontrado)
                        {
                            Nodo.Numero = Convert.ToInt32(numero);
                            Nodo.Valor1 = NombreNodo;
                            Nodos.Add(Nodo);
                        }
                    }
                }else
                {
                    Nodo.Valor1 = NombreNodo;
                    Nodos.Add(Nodo);
                }
            }

            return Nodos;
        }
        //public List<Item> EmparejarTags(XmlNodeList nodeList)
        //{
        //    List<Item> Nodos = new List<Item>();

        //    try
        //    {            

        //        foreach (XmlNode item in nodeList)
        //        {
        //            Item Nodo = new Item();
        //            int CantidadNodos = nodeList.Count;
        //            string NombreNodo = item.Name;
        //            int TamanioNodo = NombreNodo.Length;
        //            string Numero = NombreNodo.Substring(TamanioNodo - 3);
        //            bool EsNumero = VerifiaSiEsNumero(Numero);

        //            if (!EsNumero)
        //            {
        //                Numero = NombreNodo.Substring(TamanioNodo - 2);
        //                EsNumero = VerifiaSiEsNumero(Numero);
        //            }

        //            if (EsNumero)
        //            {
        //                if (Nodos.Count == 0)
        //                {
        //                    Nodo.Numero = Convert.ToInt32(Numero);
        //                    Nodo.Valor1 = NombreNodo;
        //                    Nodos.Add(Nodo);
        //                }
        //                else if (Nodos.Count > 0)
        //                {
        //                    bool Encontrado = false;
        //                    bool Agregado = false;
        //                    int CantidadNodosAgregados = Nodos.Count;

        //                    for (int i = 0; i < CantidadNodosAgregados; i++)
        //                    {
        //                        if (Nodos[i].Numero == Convert.ToInt32(Numero))
        //                        {
        //                            Encontrado = true;
        //                            if (Nodos[i].Valor2 == null)
        //                            {
        //                                Nodos[i].Valor2 = NombreNodo;
        //                                Agregado = true;
        //                            }
        //                            if (Nodos[i].Valor3 == null && !Agregado)
        //                            {
        //                                Nodos[i].Valor3 = NombreNodo;
        //                            }
        //                        }

        //                        if (Encontrado)
        //                        {
        //                            break;
        //                        }

        //                        if (!Encontrado)
        //                        {
        //                            Nodo.Numero = Convert.ToInt32(Numero);
        //                            Nodo.Valor1 = NombreNodo;
        //                            Nodos.Add(Nodo);
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Nodo.Valor1 = NombreNodo;
        //                Nodos.Add(Nodo);
        //            }              
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    return Nodos;
        //}

        public bool VerifiaSiEsNumero(string str)
        {
            bool isNum;

            isNum = Double.TryParse(Convert.ToString(str), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out double retNum);

            return isNum;
        }

        public XmlDocument Respuesta(XmlDocument doc, List<Item> list)
        {
            XmlDocument respuesta = new XmlDocument();
            XmlElement rootEnvelope, item, Valor1, Valor2, Valor3, Valor4;

            rootEnvelope = respuesta.CreateElement("", "Info", null);
            respuesta.AppendChild(rootEnvelope);

            foreach (Item Registro in list)
            {
                if (Registro.Valor1 != null || Registro.Valor2 != null || Registro.Valor3 != null || Registro.Valor4 != null)
                {
                    item = respuesta.CreateElement("Item");
                    rootEnvelope.AppendChild(item);

                    if (Registro.Valor1 != null )
                    {
                        XmlNode Valor = doc.SelectSingleNode("//Info/" + Registro.Valor1);
                        Valor1 = respuesta.CreateElement(Registro.Valor1);
                        Valor1.InnerText = Valor.InnerText;

                        item.AppendChild(Valor1);
                    }

                    if (Registro.Valor2 != null)
                    {
                        XmlNode Valor = doc.SelectSingleNode("//Info/" + Registro.Valor2);
                        Valor2 = respuesta.CreateElement(Registro.Valor2);
                        Valor2.InnerText = Valor.InnerText;

                        item.AppendChild(Valor2);
                    }

                    if (Registro.Valor3 != null)
                    {
                        XmlNode Valor = doc.SelectSingleNode("//Info/" + Registro.Valor3);
                        Valor3 = respuesta.CreateElement(Registro.Valor3);
                        Valor3.InnerText = Valor.InnerText;

                        item.AppendChild(Valor3);
                    }

                    if (Registro.Valor4 != null)
                    {
                        XmlNode Valor = doc.SelectSingleNode("//Info/" + Registro.Valor4);
                        Valor4 = respuesta.CreateElement(Registro.Valor4);
                        Valor4.InnerText = Valor.InnerText;

                        item.AppendChild(Valor4);
                    }

                    //respuesta.DocumentElement.AppendChild(item);
                }
            }

            return respuesta;
        }
    }
}