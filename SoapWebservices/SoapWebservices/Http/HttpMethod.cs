using System.Collections.Generic;

namespace SoapWebservices.Http
{
    public interface HttpMethod
    {
        string Uri { get; }

        string Method { get; }

        IDictionary<string, string> Headers { get; }

        bool CanSendBody();

        HttpBody GetBody();

    }
}
