namespace SharpCrush4.Results
{
    /// <summary>
    /// Result codes from the <see cref="SharpCrush.UploadUrl(string)"/> routine.
    /// </summary>
    public enum UrlUploadResult
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
}
