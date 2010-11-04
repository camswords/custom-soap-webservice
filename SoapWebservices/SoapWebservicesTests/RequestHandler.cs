using System.IO;

namespace SoapWebservicesTests
{
    public interface RequestHandler
    {
        void Handle(string request, StreamWriter response);
    }
}
