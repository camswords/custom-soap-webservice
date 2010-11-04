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

            var response = (HttpWebResponse)request.GetResponse();
            var statusCode = response.StatusCode;
            var statusDescription = response.StatusDescription;

            var dataStream = response.GetResponseStream();
            var responseStream = new StreamReader(dataStream);
            var content = responseStream.ReadToEnd();

            responseStream.Close();
            dataStream.Close();
            response.Close();

            return new HttpResponse(statusCode, statusDescription, content);
        }

        public HttpResponse Get(HttpGet get)
        {
            var request = HttpWebRequest.Create(get.Uri);

            var response = (HttpWebResponse)request.GetResponse();
            var statusCode = response.StatusCode;

            var dataStream = response.GetResponseStream();
            var responseStream = new StreamReader(dataStream);
            var content = responseStream.ReadToEnd();

            responseStream.Close();
            dataStream.Close();
            response.Close();

            return new HttpResponse(response.StatusCode, response.StatusDescription, content);
        }
    }
}
