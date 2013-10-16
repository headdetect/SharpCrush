namespace SharpCrush4.Results
{
    /// <summary>
    /// Result codes from the <see cref="SharpCrush.DeleteFile(string)"/> routine.
    /// </summary>
    /// <remarks></remarks>
    public enum DeleteFileResult
    {
        /// <summary>
        /// The IP matches the stored hash and the file was deleted.
        /// </summary>
        Successful = 200,

        /// <summary>
        /// There is no file with that hash.
        /// </summary>
        FileNotFound = 404,

        /// <summary>
        /// The IP does not match the stored hash.
        /// </summary>
        NotAllowed = 401
    }
}
