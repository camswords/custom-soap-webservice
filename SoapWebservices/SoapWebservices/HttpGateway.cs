using System.Net;
using System.IO;
using System.Text;
using System;
using System.Linq;

namespace SoapWebservices
{
    public class HttpGateway
    {
        public HttpResponse Execute(HttpMethod http)
        {
            var request = HttpWebRequest.Create(http.Uri);
            request.Method = http.Method;
            http.Headers.Keys.ForEach(name => request.Headers.Add(name, http.Headers[name]));

            if (http.CanSendBody())
            {
                var body = http.GetBody();
                request.ContentLength = body.ContentLength;

                if (http.HasBody())
                {
                    request.ContentType = body.ContentType;

                    using (var requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(body.Data, 0, (int) body.ContentLength);
                    }
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
            return Execute(get);
        }
    }
}
