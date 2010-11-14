using System.IO;
using SoapWebservicesTests.Http.Request;

namespace SoapWebservicesTests.Http.Request.Handler
{
    public interface IRequestHandler
    {
        void Handle(HttpRequest request, TextWriter response);
    }
}
