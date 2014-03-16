using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileFormatWavefront
{
    /// <summary>
    /// Represents the results of a file loading operation.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class FileLoadResult<TModel>
    {
        private readonly TModel model;
        private readonly List<Message> messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLoadResult{TModel}"/> class.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <param name="model">The model.</param>
        internal FileLoadResult(TModel model, List<Message> messages)
        {
            this.model = model;
            this.messages = messages;
        }

        /// <summary>
        /// Gets the loaded model.
        /// </summary>
        public TModel Model
        {
            get { return model; }
        }

        /// <summary>
        /// Gets the file load messages.
        /// </summary>
        public ReadOnlyCollection<Message> Messages
        {
            get { return messages.AsReadOnly(); }
        }
    }
}