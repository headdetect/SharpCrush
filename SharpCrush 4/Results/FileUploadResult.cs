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
        FileRejected = 415

    }

    public class FileUploadResult
    {
        [JsonProperty("error")]
        public FileUploadResults Result { get; set; }

        [JsonProperty("hash")]
        public string FileHash { get; set; }

        public SharpCrushMediaFile MediaFile { get; set; }
    }
}
