using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using SoapWebservices;

namespace SoapWebservicesTests
{
    [TestFixture]
    public class XmlDocumentTest
    {
        [Test]
        public void should_get_node_value()
        {
            var xml = new XmlDocument("<root><node>value</node></root>");
            var node = xml.GetNode("/root/node");

            Assert.That(node, Is.InstanceOfType(typeof(XmlNode)));
        }

        [Test]
        public void should_throw_exception_when_getting_node_that_doesnt_exist()
        {
            var xml = new XmlDocument("<root></root>");

            try
            {
                xml.GetNode("//non-existant-node");
                Assert.Fail("should throw exception");
            }
            catch (XPathException e)
            {
                Assert.That(e.Message, Is.EqualTo("unable to find node //non-existant-node in <root></root>"));
            }
        }
    }
}
