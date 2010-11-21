using System.Xml.XPath;
using System.IO;

namespace SoapWebservices
{
    public class XmlDocument
    {
        private readonly XPathDocument document;
        private readonly string xml;

        public XmlDocument(string xml)
        {
            this.xml = xml;
            this.document = new XPathDocument(new StringReader(xml));
        }

        public XmlNode GetNode(string xpath)
        {
            var node = document.CreateNavigator().SelectSingleNode(xpath);

            if (node == null)
            {
                throw new XPathException(string.Format("unable to find node {0} in {1}", xpath, xml));
            }
            return new XmlNode(node);
        }
    }
}
