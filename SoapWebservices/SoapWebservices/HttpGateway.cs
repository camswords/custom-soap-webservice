using System.Net;
using System.IO;
using System.Text;
using System;
using System.Linq;

namespace SoapWebservices
{
    public class HttpGateway
    {
        public HttpResponse Execute(HttpMethod httpMethod)
        {
            var request = HttpWebRequest.Create(httpMethod.Uri);
            request.Method = httpMethod.Method;
            httpMethod.Headers.Keys.ForEach(name => request.Headers.Add(name, httpMethod.Headers[name]));

            if (httpMethod.CanSendBody())
            {
                var body = httpMethod.GetBody();
                request.ContentLength = body.ContentLength;

                if (body.HasContent())
                {
                    request.ContentType = body.ContentType;

                    using (var requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(body.Data, 0, body.ContentLength);
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
    }
}
