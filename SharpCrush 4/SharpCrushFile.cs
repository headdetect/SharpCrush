using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SharpCrush4
{
    public class SharpCrushFile
    {
        /// <summary>
        /// Gets the hash of the file.
        /// </summary>
        [JsonProperty("file")]
        public string FilePath { get; internal set; }


        /// <summary>
        /// Gets the type of the file.
        /// </summary>
        [JsonProperty("type")]
        public string FileType { get; internal set; }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((SharpCrushFile)obj);
        }

        protected bool Equals(SharpCrushFile other)
        {
            return string.Equals(FilePath, other.FilePath) && 
                   string.Equals(FileType, other.FileType);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((FilePath != null ? FilePath.GetHashCode() : 0) * 397) ^ (FileType != null ? FileType.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return FilePath;
        }
    }
}
