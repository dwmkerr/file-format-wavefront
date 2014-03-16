namespace FileFormatWavefront
{
    /// <summary>
    /// Represents a message type for a loading message.
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// A warning message - indicates something may be wrong with the data.
        /// </summary>
        Warning,

        /// <summary>
        /// An error message - indicates that we KNOW something is wrong with the data.
        /// </summary>
        Error
    }
}