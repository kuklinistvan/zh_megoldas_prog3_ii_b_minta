using ii_zh_minta_b;
using ii_zh_minta_b_tests.Properties;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ii_zh_minta_b_tests
{
    [TestFixture]
    class ParserTester
    {
        [Test]
        public void TestTimeoutParser()
        {
            string testOutput = Resources.tracert_pelda;
            Assert.AreEqual(1, TracertResultParser.CountTimeoutsInTracerOutput(testOutput));
        }
    }
}
