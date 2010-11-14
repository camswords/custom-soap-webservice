using System.Net;
using System.IO;
using System.Text;
using System;
using System.Linq;
using SoapWebservices.Http.Request;
using SoapWebservices.Http.Response;
using SoapWebservices.Http.Utilities;

namespace SoapWebservices.Http
{
    public class HttpGateway
    {
        public HttpResponse Execute(HttpMethod httpMethod)
        {
            var request = (HttpWebRequest) WebRequest.Create(httpMethod.Uri);
            var body = httpMethod.GetBody();

            request.Timeout = 5000;
            request.ReadWriteTimeout = 20000;
            request.Method = httpMethod.Method;
            request.ContentLength = body.ContentLength;
            httpMethod.Headers.Keys.ForEach(name => request.Headers.Add(name, httpMethod.Headers[name]));

            if (body.HasContent())
            {
                request.ContentType = body.ContentType;

                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(body.Data, 0, body.ContentLength);
                }
            }
            
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                return new HttpResponseFactory().Create(response);
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    throw;
                }

                var response = (HttpWebResponse)e.Response;
                return new HttpResponseFactory().Create(response);
            }
        }
    }
}
