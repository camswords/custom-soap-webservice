using System.IO;

namespace SoapWebservicesTests
{
    public interface IRequestHandler
    {
        void Handle(string request, StreamWriter response);
    }
}
