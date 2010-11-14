
namespace SoapWebservices.Http.Method
{
    public class HttpBody
    {
        public static readonly HttpBody EMPTY = new HttpBody(new byte[0], "text/xml", "utf-8");

        private readonly byte[] data;
        private readonly string contentType;

        public HttpBody(byte[] data, string contentType, string charset)
        {
            this.data = data;
            this.contentType = string.Format("{0};charset={1}", contentType, charset);
        }

        public string ContentType
        {
            get { return contentType; }
        }

        public int ContentLength
        {
            get { return data.Length; }
        }

        public bool HasContent()
        {
            return data.Length > 0;
        }

        public byte[] Data
        {
            get { return data; }
        }
    }
}
