using System.Xml.XPath;

namespace SoapWebservices
{
    public class XmlNode
    {
        private readonly XPathNavigator node;

        public XmlNode(XPathNavigator node)
        {
            this.node = node;
        }

        public string GetValue()
        {
            return node.Value;
        }
    }
}
