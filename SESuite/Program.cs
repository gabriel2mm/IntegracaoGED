using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using testeSuites.Models;
using testeSuites.WSGEDDocument;

namespace testeSuites
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var _url = "https://200.186.58.10/softexpert/webserviceproxy/se/ws/dc_ws.php";
            var _action = "";
            string xml = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:document""><soapenv:Header/><soapenv:Body><urn:searchCategory/></soapenv:Body></soapenv:Envelope>";

            string soapResult = getResponse(_url , xml, _action);

            XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
            using (TextReader reader = new StringReader(soapResult))
            {
                Envelope result = (Envelope)serializer.Deserialize(reader);

                foreach (Item i in result.Body.SearchCategoryResponse.Return.RESULTARRAY.Item)
                {
                    Console.WriteLine(i.CDCATEGORY);
                    Console.WriteLine(i.NMCATEGORY);
                    Console.WriteLine(i.CDCATEGORYOWNER);
                    Console.WriteLine("--------------------------");
                }
            }

            Console.ReadKey();
        }

        private static string getResponse(String _url, String xml, String _action)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            XmlDocument soapEnvelopeXml = CreateSoapEnvelope(xml);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            asyncResult.AsyncWaitHandle.WaitOne();

            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
            }

            return soapResult;
        }
        private static XmlDocument CreateSoapEnvelope(string xml)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(xml);
            return soapEnvelopeDocument;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("Authorization", "Basic aW50ZWdyYWNhbzppbnRlZ3JhY2FvQDIwMTg=");
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }


    }
}
