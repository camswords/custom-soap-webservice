using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace SoapWebservices
{
    class Program
    {
        static void Main(string[] args)
        {
            string request = @"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:RandomGoogleSearch""><soapenv:Header/><soapenv:Body><urn:getRandomGoogleSearch soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/""><word xsi:type=""xsd:string"">example2</word></urn:getRandomGoogleSearch></soapenv:Body></soapenv:Envelope>";

            byte[] data = Encoding.UTF8.GetBytes(request);

            var webRequest = HttpWebRequest.Create("http://www.ghettodriveby.com/soap/index.php");
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml;charset=UTF-8";
            webRequest.Headers.Add("SOAPAction", "urn:RandomGoogleSearch#RandomGoogleSearch#getRandomGoogleSearch");

            webRequest.ContentLength = data.Length;

            var stream = webRequest.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            var response = (HttpWebResponse) webRequest.GetResponse();
            var statusCode = response.StatusCode;

            var dataStream = response.GetResponseStream();
            var responseStream = new StreamReader(dataStream);
            var content = responseStream.ReadToEnd();

            responseStream.Close();
            dataStream.Close();
            response.Close();   

            Console.WriteLine("status code: " + statusCode);
            Console.WriteLine("content: " + content);
            Console.ReadLine();
        }
    }
}
