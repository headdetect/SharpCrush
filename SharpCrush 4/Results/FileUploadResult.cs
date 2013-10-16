namespace SharpCrush.Results
{
    /// <summary>
    /// Result codes from the <see cref="SharpCrush.UploadFile(object)"/> routine.
    /// </summary>
    public enum FileUploadResult
    {
        /// <summary>
        /// The file was uploaded correctly.
        /// </summary>
        Successful,

        /// <summary>
        /// The file was already uploaded.
        /// </summary>
        AlreadyUploaded,

        /// <summary>
        /// The rate limit was exceeded. Simmer down with the uploads. k?
        /// </summary>
        RateLimitExceeded,

        /// <summary>
        /// The file extension is not acceptable. File rejected.
        /// </summary>
        FileRejected

    }
}
