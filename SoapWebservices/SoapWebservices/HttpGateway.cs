using System.Net;
using System.IO;
using System.Text;
using System;

namespace SoapWebservices
{
    public class HttpGateway
    {
        public HttpResponse Post(HttpPost post)
        {
            byte[] data = post.ToBytes();

            var request = HttpWebRequest.Create(post.Uri);
            request.Method = post.Method;
            request.ContentType = post.ContentType;
            request.ContentLength = post.ContentLength;

            foreach (var headerName in post.Headers.Keys)
            {
                request.Headers.Add(headerName, post.Headers[headerName]);
            }

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return new HttpResponseFactory().Create(response);
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                return new HttpResponseFactory().Create(response);
            }
        }

        public HttpResponse Get(HttpGet get)
        {
            var request = HttpWebRequest.Create(get.Uri);

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return new HttpResponseFactory().Create(response);
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse) e.Response;
                return new HttpResponseFactory().Create(response);
            }
        }
    }
}
