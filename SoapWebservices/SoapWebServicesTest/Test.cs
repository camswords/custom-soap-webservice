using NUnit.Framework;

namespace SoapWebServicesTest
{
    [TestFixture]
    public class Mytest
    {
        [Test]
        public void should_pass()
        {
            Assert.AreEqual(true, true);
        }

        [Test]
        public void should_run_again()
        {
            Assert.That("fuck you", Is.EqualTo("you fuck"));
        }

    }
}
