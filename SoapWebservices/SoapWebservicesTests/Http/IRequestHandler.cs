using System.IO;

namespace SoapWebservicesTests.Http
{
    public interface IRequestHandler
    {
        void Handle(HttpRequest request, TextWriter response);
    }
}
