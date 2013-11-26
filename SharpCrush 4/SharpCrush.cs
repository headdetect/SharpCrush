using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using SharpCrush4.Results;

namespace SharpCrush4
{
    /// <summary>
    /// MediaCrush API Wrapper
    /// </summary>
    public class SharpCrush
    {
        #region Urls

        /// <summary>
        /// Parameters: none.
        /// Returns: information about the file whose hash is &lt;hash&gt;.
        /// </summary>
        public const string SingleFileApiUrl = "/api/{0}";

        /// <summary>
        /// Parameters: list, a comma-separated list of hashes.
        /// Returns: an array of file objects.
        /// </summary>
        public const string MultiFileApiUrl = "/api/info?list={0}";


        /// <summary>
        /// Parameters: none.
        /// Returns: a dictionary answering the question of whether a hash exists.
        /// </summary>
        public const string FileExistsApiUrl = "/api/{0}/exists";

        /// <summary>
        /// Parameters: none.
        /// Returns: a dictionary describing whether the delete operation succeeded. In most cases it is easier to check the HTTP status code.
        /// </summary>
        public const string FileDeleteApiUrl = "/api/{0}/delete";

        /// <summary>
        /// Parameters: none.
        /// Returns: the processing status of the file identified by the hash.
        /// </summary>
        public const string FileStatusApiUrl = "/api/{0}/status";

        /// <summary>
        /// Parameters: <c>file</c>, the file to upload.
        /// Returns: a dictionary with the hash of the file in case the upload succeeded, a dictionary containing the error code if it did not succeed.
        /// </summary>
        public const string FileUploadApiUrl = "/api/upload/file";


        /// <summary>
        /// Parameters: <c>url</c>, the URL from where to fetch the file to upload.
        /// Returns: the same as /api/upload/file.
        /// </summary>
        public const string UrlUploadApiUrl = "/api/upload/url";

        /// <summary>
        /// The base url of the mediacrush server. Because the media crush platform is opensource,
        /// A different server/url can be used. It just needs to follow the same type of api.
        /// </summary>
        public static string BaseApiUrl = "https://mediacru.sh";

        #endregion

        #region Public Routines

        /// <summary>
        /// Gets information about the file from the specified hash.
        /// </summary>
        /// <param name="hash">The hash of the file to get.</param>
        /// <returns>information about the file whose hash is specified</returns>
        /// <remarks>API Doc Url: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihash </remarks>
        public static SharpCrushMediaFile GetFileInfo(string hash)
        {
            string json = Get(BaseApiUrl + SingleFileApiUrl, hash);

            SharpCrushMediaFile file = JsonConvert.DeserializeObject<SharpCrushMediaFile>(json);

            return file;
        }

        /// <summary>
        /// Gets information about the files whose hashes are specified.
        /// </summary>
        /// <param name="hashes">the hashes of the files to get</param>
        /// <returns>information about the files whose hashes are specified.</returns>
        /// <remarks>API Doc Url: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apiinfolisthash </remarks>
        public static Dictionary<string, SharpCrushMediaFile> GetFileInfos(params string[] hashes)
        {
            string json = Get(BaseApiUrl + MultiFileApiUrl, string.Join(",", hashes));

            Dictionary<string, SharpCrushMediaFile> files = JsonConvert.DeserializeObject<Dictionary<string, SharpCrushMediaFile>>(json);

            return files;
        }

        /// <summary>
        /// Returns <c>true</c> if the specified file exists; <c>false</c> otherwise
        /// </summary>
        /// <param name="hash">The file hash to check</param>
        /// <returns>Returns <c>true</c> if the specified file exists; <c>false</c> otherwise</returns>
        /// <remarks>API Doc Url: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihashexists </remarks>
        public static bool GetFileExists(string hash)
        {
            string json = Get(BaseApiUrl + FileExistsApiUrl, hash);

            bool exists = JsonConvert.DeserializeObject<dynamic>(json).exists;

            return exists;
        }

        /// <summary>
        /// Attempts to delete the specified files
        /// </summary>
        /// <param name="hash">The hash of the file to delete</param>
        /// <returns>Returns one <see cref="DeleteFileResult"/></returns>
        /// <remarks>API Doc Url: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihashexists </remarks>
        public static DeleteFileResult DeleteFile(string hash)
        {
            string json = Get(BaseApiUrl + FileDeleteApiUrl, hash);

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            if (dictionary.ContainsKey("status"))
            {
                if (dictionary["status"].ToString().Equals("success", StringComparison.CurrentCultureIgnoreCase))
                    return DeleteFileResult.Successful;
            }

            if (dictionary.ContainsKey("error"))
            {
                int result;
                if(!int.TryParse(dictionary["error"].ToString(), out result))
                    return DeleteFileResult.NotAllowed;
                return (DeleteFileResult) result;
            }

            return DeleteFileResult.NotAllowed;
        }

        /// <summary>
        /// Attempts to get the status of the specified file
        /// </summary>
        /// <param name="hash">The hash of the file to get the status of.</param>
        /// <returns>Returns one <see cref="GetFileStatusResult"/></returns>
        /// <remarks>API Doc Url: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apihashstatus </remarks>
        public static GetFileStatusResult GetFileStatus(string hash)
        {
            string json = Get(BaseApiUrl + FileStatusApiUrl, hash);

            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            if (dictionary.ContainsKey("status"))
            {
                if (dictionary["status"].ToString().Equals("done", StringComparison.CurrentCultureIgnoreCase))
                    return GetFileStatusResult.Done;
                if (dictionary["status"].ToString().Equals("processing", StringComparison.CurrentCultureIgnoreCase))
                    return GetFileStatusResult.Processing;
                if (dictionary["status"].ToString().Equals("error", StringComparison.CurrentCultureIgnoreCase))
                    return GetFileStatusResult.Error;
                if (dictionary["status"].ToString().Equals("timeout", StringComparison.CurrentCultureIgnoreCase))
                    return GetFileStatusResult.TimedOut;
            }

            if (dictionary.ContainsKey("error"))
            {
                int result;
                if (!int.TryParse(dictionary["error"].ToString(), out result))
                    return GetFileStatusResult.FileNotFound;
                return (GetFileStatusResult)result;
            }

            return GetFileStatusResult.Error;
        }

        /// <summary>
        /// Upload the specified file to the MediaCrush (or other) server.
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <returns>Returns one <see cref="FileUploadResult"/></returns>
        /// <remarks>API Doc Url: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apiuploadfile </remarks>
        public static FileUploadResult UploadFile(string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException("Specified file doesn't exist", file);

            string json = Upload(BaseApiUrl + FileUploadApiUrl, file);

            FileUploadResult result = JsonConvert.DeserializeObject<FileUploadResult>(json);

            // Because the hash is the key of the SharpCrushMediaFile (I have no idea why), dynamic objects are needed //
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(json);

            // Sometimes it returns a somewhat malformed array. //
            // https://github.com/MediaCrush/MediaCrush/issues/356 //
            if (dynamicObject[result.FileHash] is JArray)
            {
                // We going to have to get media files a different way //
                var mediaFile = GetFileInfo(result.FileHash);
                result.MediaFile = mediaFile;
            }

            if (dynamicObject[result.FileHash] is JObject)
            {

                // This is the expected/faster way //
                var jObject = (JObject)dynamicObject[result.FileHash];
                result.MediaFile = jObject.ToObject<SharpCrushMediaFile>();
            }

            return result;
        }

        /// <summary>
        /// Upload the specified file from url to the MediaCrush (or other) server.
        /// </summary>
        /// <param name="url">The url to upload</param>
        /// <returns>Returns one <see cref="UrlUploadResult"/></returns>
        /// <remarks>API Doc Url: https://github.com/MediaCrush/MediaCrush/blob/master/docs/api.md#apiuploadurl </remarks>
        public static UrlUploadResult UploadUrl(string url)
        {
            string json = Post(BaseApiUrl + UrlUploadApiUrl, "url=" + url);

            return UrlUploadResult.FileRejected;
        }

        #endregion

        #region Private Routines

        private static string Get(string url, params object[] formatArgs)
        {
            using (var client = new WebClient())
            {
                try
                {
                    return client.DownloadString(string.Format(url, formatArgs));
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                        return new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    return string.Empty;
                }
            }
        }

        private static string Post(string url, string post)
        {
            using (var client = new WebClient())
            {
                try
                {
                    return client.UploadString(url, "POST", post);
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                        return new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    return string.Empty;
                }
            }
        }

        private static string Upload(string url, string filePath)
        {
            using (var client = new WebClient())
            {
                try
                {
                    return Encoding.UTF8.GetString(client.UploadFile(url, filePath));
                }
                catch (WebException ex)
                {
                    if (ex.Response != null)
                        return new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    return string.Empty;
                }
            }
        }

        #endregion

    }
}
