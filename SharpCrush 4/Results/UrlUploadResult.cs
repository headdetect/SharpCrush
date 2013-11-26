using Newtonsoft.Json;
namespace SharpCrush4.Results
{
    /// <summary>
    /// Result codes from the <see cref="SharpCrush.UploadUrl(string)"/> routine.
    /// </summary>
    public enum UrlUploadResults
    {
        /// <summary>
        /// The file was uploaded correctly.
        /// </summary>
        Successful = 200,

        /// <summary>
        /// The URL is invalid.
        /// </summary>
        InvalidUrl = 400,

        /// <summary>
        /// The requested file does not exist.
        /// </summary>
        FileNotExist = 404,

        /// <summary>
        /// The file was already uploaded.
        /// </summary>
        AlreadyUploaded = 409,

        /// <summary>
        /// The file extension is not acceptable. File rejected.
        /// </summary>
        FileRejected = 415,

        /// <summary>
        /// The rate limit was exceeded. Simmer down with the uploads. k?
        /// </summary>
        RateLimitExceeded = 420 // xXYoloSwag420Xx //
    }


    /// <summary>
    /// The wrapper class for results from file uploading.
    /// </summary>
    public class UrlUploadResult
    {
        /// <summary>
        /// Gets the hash of the file
        /// </summary>
        /// <value>
        /// The file hash.
        /// </value>
        [JsonProperty("hash")]
        public string FileHash { get; internal set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        [JsonProperty("error")]
        public UrlUploadResults Result { get; internal set; }

        /// <summary>
        /// Gets the media file associated with the FileHash.
        /// </summary>
        /// <value>
        /// The media file.
        /// </value>
        public SharpCrushMediaFile MediaFile { get; internal set; }

        /// <summary>
        /// Gets the status of the upload
        /// </summary>
        /// <returns>The file status of the upload</returns>
        /// <remarks>Same as doing <see cref="SharpCrush.GetFileStatus(string)"/></remarks>
        public GetFileStatusResult Status
        {
            get { return SharpCrush.GetFileStatus(FileHash); }
        }

        /// <summary>
        /// Deletes this file. 
        /// </summary>
        /// <returns>The result of the file deletion</returns>
        /// <remarks>Same as doing <see cref="SharpCrush.DeleteFile(string)"/></remarks>
        public DeleteFileResult Delete()
        {
            return SharpCrush.DeleteFile(FileHash);
        }

    }
}
