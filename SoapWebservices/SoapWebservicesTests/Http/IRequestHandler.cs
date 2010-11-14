using System.IO;

namespace SoapWebservicesTests.Http
{
    public interface IRequestHandler
    {
        void Handle(string request, TextWriter response);
    }
}
