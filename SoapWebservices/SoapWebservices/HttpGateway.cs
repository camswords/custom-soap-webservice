using System.Net;
using System.IO;
using System.Text;
using System;
using System.Linq;

namespace SoapWebservices
{
    public class HttpGateway
    {
        public HttpResponse Post(HttpPost post)
        {
            var request = HttpWebRequest.Create(post.Uri);
            request.Method = post.Method;

            if (post.Headers.ContainsKey("Content-Type"))
            {
                request.ContentType = post.ContentType;
            }
            if (post.Headers.ContainsKey("Content-Length"))
            {
                request.ContentLength = post.ContentLength;
            }

            post.Headers.Keys
                .Where(key => !key.Equals("Content-Type") && !key.Equals("Content-Length"))
                .ToList().ForEach(key => request.Headers.Add(key, post.Headers[key]));

            if (post.HasBody())
            {
                using (var requestStream = request.GetRequestStream())
                {
                    var data = post.ToBytes();
                    requestStream.Write(data, 0, data.Length);
                }
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
