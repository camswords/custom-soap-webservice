using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SoapWebservices;
using System.Xml.XPath;
using System.IO;

namespace SoapWebservicesTests
{
    [TestFixture]
    public class XmlNodeTest
    {
        [Test]
        public void should_return_value_of_node()
        {
            var document = new XPathDocument(new StringReader("<root><node>value</node></root>"));
            var node = new XmlNode(document.CreateNavigator());

            Assert.That(node.GetValue(), Is.EqualTo("value"));
        }
    }
}
