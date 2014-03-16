namespace FileFormatWavefront.Model
{
    /// <summary>
    /// Represents an index.
    /// </summary>
    public struct Index
    {
        /// <summary>
        /// The vertex index.
        /// </summary>
        public int vertex;

        /// <summary>
        /// The uv index.
        /// </summary>
        public int? uv;

        /// <summary>
        /// The normal index.
        /// </summary>
        public int? normal;
    }
}