using System.Collections.Generic;
using Newtonsoft.Json;
namespace SharpCrush4.Results
{
    /// <summary>
    /// Result codes from the <see cref="SharpCrush.UploadFile(byte[])"/> routine.
    /// </summary>
    public enum FileUploadResults
    {
        /// <summary>
        /// The file was uploaded correctly.
        /// </summary>
        Successful = 200,

        /// <summary>
        /// The file was already uploaded.
        /// </summary>
        AlreadyUploaded = 409,

        /// <summary>
        /// The rate limit was exceeded. Simmer down with the uploads. k?
        /// </summary>
        RateLimitExceeded = 420, // yoloswag420blazeit //

        /// <summary>
        /// The file extension is not acceptable. File rejected.
        /// </summary>
        FileRejected = 415,

        /// <summary>
        /// Non-API result. Used when there is an error getting any result
        /// </summary>
        Error
    }

    /// <summary>
    /// The wrapper class for results from file uploading.
    /// </summary>
    public class FileUploadResult
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
        public FileUploadResults Result { get; internal set; }

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
