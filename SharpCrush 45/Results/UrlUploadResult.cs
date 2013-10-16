namespace SharpCrush45.Results
{
    /// <summary>
    /// Result codes from the <see cref="SharpCrush.UploadUrl(string)"/> routine.
    /// </summary>
    public enum UrlUploadResult
    {
        /// <summary>
        /// The file was uploaded correctly.
        /// </summary>
        Successful,

        /// <summary>
        /// The URL is invalid.
        /// </summary>
        InvalidUrl,

        /// <summary>
        /// The requested file does not exist.
        /// </summary>
        FileNotExist,

        /// <summary>
        /// The file was already uploaded.
        /// </summary>
        AlreadyUploaded,

        /// <summary>
        /// The file extension is not acceptable. File rejected.
        /// </summary>
        FileRejected,

        /// <summary>
        /// The rate limit was exceeded. Simmer down with the uploads. k?
        /// </summary>
        RateLimitExceeded
    }
}
