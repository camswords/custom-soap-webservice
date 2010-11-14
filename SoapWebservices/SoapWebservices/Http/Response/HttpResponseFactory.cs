using System.Net;
using System.IO;
using SoapWebservices.Http.Utilities;

namespace SoapWebservices.Http.Response
{
    public class HttpResponseFactory
    {
        public HttpResponse Create(HttpWebResponse response)
        {
            var statusCode = response.StatusCode;
            var statusDescription = response.StatusDescription;

            Stream dataStream = null;
            StreamReader responseStream = null;

            try
            {
                dataStream = response.GetResponseStream();
                responseStream = new StreamReader(dataStream);
                var content = responseStream.ReadToEnd();

                return new HttpResponse(statusCode, statusDescription, content);
            }
            finally
            {
                new SuppressExceptions().On(() => responseStream.Close());
                new SuppressExceptions().On(() => dataStream.Close());
                new SuppressExceptions().On(() => response.Close());
            }
        }
    }
}
