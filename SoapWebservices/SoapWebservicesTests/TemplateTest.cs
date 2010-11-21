using SoapWebservices.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SoapWebservicesTests
{
    [TestFixture]
    public class TemplateTest
    {
        [Test]
        public void should_render_template()
        {
            var template = new Template("SoapWebservicesTests.Templates.mmm-tea.st");
            var contents = template.Render(new Dictionary<string, object>());

            Assert.That(contents, Is.EqualTo("mmmmm tea"));
        }

        [Test]
        public void should_render_replacing_parameters_with_their_value()
        {
            var template = new Template("SoapWebservicesTests.Templates.params-test.st");

            var contents = template.Render(new Dictionary<string, object>() { { "property", "value" } });
            Assert.That(contents, Is.EqualTo("the replaced property is value"));
        }

        [Test]
        public void should_throw_exception_when_cannot_find_template()
        {
            try
            {
                new Template("not.found.st");
                Assert.Fail("should throw exception");
            }
            catch (FileNotFoundException e)
            {
                var assembly = Assembly.GetExecutingAssembly().GetName();
                Assert.That(e.Message, Text.Contains("cannot find embedded resource not.found.st in assembly " + assembly));
                Assert.That(e.Message, Text.Contains("candidate templates include"));
            }
        }
    }
}
