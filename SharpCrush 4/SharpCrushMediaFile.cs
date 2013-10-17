using Newtonsoft.Json;

namespace SharpCrush4
{
    public class SharpCrushMediaFile
    {


        /// <summary>
        /// Gets the compression of the media file
        /// </summary>
        [JsonProperty("compression")]
        public float Compression { get; set; }

        /// <summary>
        /// Gets the list of files in this media file
        /// </summary>
        [JsonProperty("files")]
        public SharpCrushFile[] Files { get; set; }

        /// <summary>
        /// Gets the original file uploaded
        /// </summary>
        [JsonProperty("original")]
        public string Original { get; set; }

        /// <summary>
        /// Gets the original file type uploaded
        /// </summary>
        [JsonProperty("type")]
        public string OriginalFileType { get; set; }

        /// <summary>
        /// Gets an error if there is one.
        /// </summary>
        [JsonProperty("error")]
        public int StatusCode { get; set; }

        internal SharpCrushMediaFile(float compression, string original, string originalFileType, params SharpCrushFile[] files)
        {
            Compression = compression;
            Files = files;
            Original = original;
            OriginalFileType = originalFileType;

            StatusCode = 200; // not really an error... //
        }

        internal SharpCrushMediaFile()
        {
            StatusCode = 200; // not really an error... //
        }

        public override string ToString()
        {
            return Original;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() &&
                   Equals((SharpCrushMediaFile) obj);
        }

        protected bool Equals(SharpCrushMediaFile other)
        {
            return Compression.Equals(other.Compression) &&
                   Equals(Files, other.Files) &&
                   string.Equals(Original, other.Original) &&
                   string.Equals(OriginalFileType, other.OriginalFileType) &&
                   StatusCode == other.StatusCode;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Compression.GetHashCode();
                hashCode = (hashCode * 397) ^ (Files != null ? Files.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Original != null ? Original.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OriginalFileType != null ? OriginalFileType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ StatusCode;
                return hashCode;
            }
        }

    }
}
