
namespace SoapWebservices
{
    public class HttpBody
    {
        public static readonly HttpBody EMPTY = new HttpBody(new byte[0], "text/xml;charset=utf-8");

        private readonly byte[] data;
        private readonly string contentTypeHeader;

        public HttpBody(byte[] data, string contentTypeHeader)
        {
            this.data = data;
            this.contentTypeHeader = contentTypeHeader;
        }

        public string ContentType
        {
            get { return contentTypeHeader; }
        }

        public long ContentLength
        {
            get { return data.Length; }
        }

        public byte[] Data
        {
            get { return data; }
        }
    }
}
