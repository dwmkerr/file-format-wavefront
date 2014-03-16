using System;

namespace FileFormatWavefront
{
    /// <summary>
    /// Represents a message of a specific severity relating to the loading of data from a file.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The message type.
        /// </summary>
        private readonly MessageType messageType;

        /// <summary>
        /// The file name. May be null.
        /// </summary>
        private readonly string fileName;

        /// <summary>
        /// The line number. May be null.
        /// </summary>
        private readonly int? lineNumber;

        /// <summary>
        /// The actual message details.
        /// </summary>
        private readonly string details;

        /// <summary>
        /// The exception associated with the message, may be null.
        /// </summary>
        private readonly Exception exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="lineNumber">The line number.</param>
        /// <param name="details">The message details.</param>
        /// <param name="exception">The exception.</param>
        public Message(MessageType messageType, string fileName, int? lineNumber, string details, Exception exception = null)
        {
            this.messageType = messageType;
            this.fileName = fileName;
            this.lineNumber = lineNumber;
            this.details = details;
            this.exception = exception;
        }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        public MessageType MessageType
        {
            get { return messageType; }
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// Gets the line number.
        /// </summary>
        public int? LineNumber
        {
            get { return lineNumber; }
        }

        /// <summary>
        /// Gets the details.
        /// </summary>
        public string Details
        {
            get { return details; }
        }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Exception Exception
        {
            get { return exception; }
        }
    }
}