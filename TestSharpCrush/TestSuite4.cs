using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCrush4;

namespace TestMediaCrush
{
    [TestClass]
    public class TestSuite4
    {
        [TestMethod]
        public void TestGetInfo()
        {
            var shouldBe404 = SharpCrush.GetFileInfo("LxqXxVPAvqqB");
            Assert.IsTrue(shouldBe404.StatusCode == 404);

            var shouldBeGood = SharpCrush.GetFileInfo("tVWMM_ziA3nm");
            Assert.IsTrue(shouldBeGood.StatusCode == 200);
            Assert.IsNotNull(shouldBeGood.Files);
            Assert.IsNotNull(shouldBeGood.Original);
            Assert.IsNotNull(shouldBeGood.OriginalFileType);
        }

        [TestMethod]
        public void TestGetInfos()
        {
            var files = SharpCrush.GetFileInfos("tVWMM_ziA3nm", "CPvuR5lRhmS0");

            Assert.IsTrue(files.Count == 2);

            var firstFile = files["CPvuR5lRhmS0"];

            Assert.IsTrue(firstFile.StatusCode == 200);
            Assert.IsNotNull(firstFile.Files);
            Assert.IsNotNull(firstFile.Original);
            Assert.IsNotNull(firstFile.OriginalFileType);

            var secondFile = files["tVWMM_ziA3nm"];

            Assert.IsTrue(secondFile.StatusCode == 200);
            Assert.IsNotNull(secondFile.Files);
            Assert.IsNotNull(secondFile.Original);
            Assert.IsNotNull(secondFile.OriginalFileType);

        }

        [TestMethod]
        public void TestFileExists()
        {
            Assert.IsFalse(SharpCrush.GetFileExists("LxqXxVPAvqqB"));
            Assert.IsTrue(SharpCrush.GetFileExists("tVWMM_ziA3nm"));
        }

        [TestMethod]
        public void TestDeleteFile()
        {
            // We need a file to delete //


        }

        [TestMethod]
        public void TestGetFileStatus()
        {
        }

        [TestMethod]
        public void TestUploadFile()
        {
            var jaypegResult = SharpCrush.UploadFile("./TestFiles/jaypeg.jpeg");

            Assert.IsTrue(jaypegResult.Result == SharpCrush4.Results.FileUploadResults.AlreadyUploaded || jaypegResult.Result == SharpCrush4.Results.FileUploadResults.Successful);

            var jiffyResult = SharpCrush.UploadFile("./TestFiles/jiffy.gif");

            Assert.IsTrue(jiffyResult.Result == SharpCrush4.Results.FileUploadResults.AlreadyUploaded || jiffyResult.Result == SharpCrush4.Results.FileUploadResults.Successful);

        }

        [TestMethod]
        public void TestUploadFileUrl()
        {

        }
    }
}
