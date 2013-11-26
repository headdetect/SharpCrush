using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCrush4;
using SharpCrush4.Results;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

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
            var jiffyResult = SharpCrush.UploadFile(getATempImageSoWeCanGetAWorkAroundForIssue365FoundOnGitHubDotComJpg("./TestFiles/jaypeg.jpg"));
            Assert.IsTrue(jiffyResult.Result == FileUploadResults.AlreadyUploaded || jiffyResult.Result == FileUploadResults.Successful);


            // == Try to delete out stuff == //

            var shouldDelete = SharpCrush.DeleteFile(jiffyResult.FileHash);

            Assert.IsTrue(shouldDelete == DeleteFileResult.Successful);



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
            var jaypeg = SharpCrush.UploadFile(getATempImageSoWeCanGetAWorkAroundForIssue365FoundOnGitHubDotComJpg("./TestFiles/jaypeg.jpg"));

            // Let the server do its thing... //
            Thread.Sleep(2000);

            var stat1 = jaypeg.Status;
            if (stat1 == GetFileStatusResult.Done || stat1 == GetFileStatusResult.Processing)
            {
                // We all good //
                return;
            }


            // Let the server do its thing... //
            Thread.Sleep(2000);

            // Try one more time //
            stat1 = jaypeg.Status;
            if (stat1 == GetFileStatusResult.Done || stat1 == GetFileStatusResult.Processing)
            {
                // We all good //
                return;
            }

            Assert.Fail();

        }

        [TestMethod]
        public void TestUploadFile()
        {
            var jaypegResult = SharpCrush.UploadFile(getATempImageSoWeCanGetAWorkAroundForIssue365FoundOnGitHubDotComJpg("./TestFiles/jaypeg.jpg"));

            Assert.IsTrue(jaypegResult.Result == FileUploadResults.AlreadyUploaded || jaypegResult.Result == FileUploadResults.Successful);

            var jiffyResult = SharpCrush.UploadFile(getATempImageSoWeCanGetAWorkAroundForIssue365FoundOnGitHubDotComGif("./TestFiles/jiffy.gif"));

            Assert.IsTrue(jiffyResult.Result == FileUploadResults.AlreadyUploaded || jiffyResult.Result == FileUploadResults.Successful);

        }

        [TestMethod]
        public void TestUploadFileUrl()
        {
            // Just for the sake of the test //
            int random = new Random().Next(100000000, 999999999);

            var randomResult = SharpCrush.UploadUrl("http://randomimage.setgetgo.com/get.php?key=" + random + "&height=50&width=50&type=png");

            Assert.IsTrue(randomResult.Result == UrlUploadResults.Successful);

            if (randomResult.Result == UrlUploadResults.AlreadyUploaded)
            {
                Assert.Inconclusive("Url key \"" + random + "\" was already uploaded...?");
            }
        }

        /// <summary>
        /// You're probably wondering. Wut da heil. Well, you see, https://github.com/MediaCrush/MediaCrush/issues/356 is kind of blocking some progress. 
        /// Code blocking, if you will. This is the best way I could get around it.
        /// </summary>
        private string getATempImageSoWeCanGetAWorkAroundForIssue365FoundOnGitHubDotComJpg(string src)
        {
            string newName = src.Replace(Path.GetFileName(src), DateTime.Now.ToString("yymmddMMss") + "." + Path.GetFileName(src));

            Bitmap map = new Bitmap(100, 50);
            using (var graphics = Graphics.FromImage(map))
            {
                graphics.DrawString(DateTime.Now.ToString("yymmddMMss"), SystemFonts.MenuFont, Brushes.Red, new PointF());
            }
            map.Save(newName);

            return newName;
        }

        /// <summary>
        /// You're probably wondering. Wut da heil. Well, you see, https://github.com/MediaCrush/MediaCrush/issues/356 is kind of blocking some progress. 
        /// Code blocking, if you will. This is the best way I could get around it.
        /// </summary>
        private string getATempImageSoWeCanGetAWorkAroundForIssue365FoundOnGitHubDotComGif(string src)
        {
            string newName = src.Replace(Path.GetFileName(src), DateTime.Now.ToString("yymmddMMss") + "." + Path.GetFileName(src));
            Image gifImg = Image.FromFile(src);

            using (var graphics = Graphics.FromImage(gifImg))
            {
                graphics.DrawString(DateTime.Now.ToString("yymmddMMss"), SystemFonts.MenuFont, Brushes.Red, new PointF());
            }

            gifImg.Save(newName);
            return newName;
        }
    }
}
