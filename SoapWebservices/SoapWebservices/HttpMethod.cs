using System.Collections.Generic;

namespace SoapWebservices
{
    public interface HttpMethod
    {
        string Uri { get; }

        string Method { get; }

        IDictionary<string, string> Headers { get; }

        bool CanSendBody();

        bool HasBody();

        byte[] ToBytes();

        HttpBody GetBody();

    }
}
