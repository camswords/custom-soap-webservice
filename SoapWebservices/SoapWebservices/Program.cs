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
            string request = new RandomGoogleSearchMessageBuilder().Build();


            var post = new HttpPost("http://www.ghettodriveby.com/soap/index.php", request, "text/xml", "utf-8");
            post.AddHeader("SOAPAction", "urn:RandomGoogleSearch#RandomGoogleSearch#getRandomGoogleSearch");

            var response = new HttpGateway().Post(post);

            Console.WriteLine("content: " + response.Content);
            Console.ReadLine();
        }
    }
}
