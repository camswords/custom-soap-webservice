using System.IO;

namespace SoapWebservicesTests.Http
{
    public interface IRequestHandler
    {
        void Handle(string requestContent, HttpRequest request, TextWriter response);
    }
}
