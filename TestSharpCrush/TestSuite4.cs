using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCrush4;
using SharpCrush4.Results;

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
            // Upload so we have something to delete //
            var jiffyResult = SharpCrush.UploadFile("./TestFiles/jiffy.gif");
            Assert.IsTrue(jiffyResult.Result == FileUploadResults.AlreadyUploaded || jiffyResult.Result == FileUploadResults.Successful);


            // == Try to delete out stuff == //

            var shouldDelete = SharpCrush.DeleteFile(jiffyResult.FileHash);

            // Deletion is broken: https://github.com/MediaCrush/MediaCrush/issues/356 //
            //TODO: Assert.IsTrue(shouldDelete == DeleteFileResult.Successful);



            // == Try to delete not our stuff == //

            var wontDelete = SharpCrush.DeleteFile("CPvuR5lRhmS0");

            Assert.IsTrue(wontDelete == DeleteFileResult.NotAllowed);



            // == Try to delete things that not exist == //

            var cantDelete = SharpCrush.DeleteFile("ThisDoesntExist");

            Assert.IsTrue(cantDelete == DeleteFileResult.FileNotFound);

        }

        [TestMethod]
        public void TestGetFileStatus()
        {
            var jaypeg = SharpCrush.UploadFile("./TestFiles/jaypeg.jpg");

            var stat1 = jaypeg.Status;

            // OR 

            var stat2 = SharpCrush.GetFileStatus(jaypeg.FileHash);

            // The only way this would fail, is if they status changed with in the execution of stat1 and stat2. So about ~ 5ms //
            Assert.IsTrue(stat1 == stat2);

            // Will fail. See: https://github.com/MediaCrush/MediaCrush/issues/356 //
            Assert.IsTrue(stat1 == GetFileStatusResult.Done || stat1 == GetFileStatusResult.Processing);

        }

        [TestMethod]
        public void TestUploadFile()
        {
            var jaypegResult = SharpCrush.UploadFile("./TestFiles/jaypeg.jpg");

            Assert.IsTrue(jaypegResult.Result == FileUploadResults.AlreadyUploaded || jaypegResult.Result == FileUploadResults.Successful);

            var jiffyResult = SharpCrush.UploadFile("./TestFiles/jiffy.gif");

            Assert.IsTrue(jiffyResult.Result == FileUploadResults.AlreadyUploaded || jiffyResult.Result == FileUploadResults.Successful);

        }

        [TestMethod]
        public void TestUploadFileUrl()
        {

        }
    }
}
