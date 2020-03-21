using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xfilehash;

namespace xFilehash.test
{
    [TestClass]
    public class FileHashAttributeTest
    {
        [TestMethod]
        [XFilehashAttribute("")]
        public void RunCommand()
        {
            try
            {

            }
            catch (Exception error)
            {
                Assert.AreEqual("", error.Message);
            }
        }

    }
}