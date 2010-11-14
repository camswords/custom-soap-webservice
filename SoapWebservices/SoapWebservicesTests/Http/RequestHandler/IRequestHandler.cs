using System.IO;
using SoapWebservicesTests.Http.Request;

namespace SoapWebservicesTests.Http.RequestHandler
{
    public interface IRequestHandler
    {
        void Handle(HttpRequest request, TextWriter response);
    }
}
