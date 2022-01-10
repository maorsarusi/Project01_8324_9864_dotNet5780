using System;
using System.Runtime.Serialization;
using BE;

namespace BL
{
    [Serializable]
    internal class MailException : Exception
    {
        private Layers Layer { get; set; }

        public MailException()
        {
        }

        public MailException(string message) : base(message)
        {
        }

        public MailException(Layers layer, string message) : base(message)
        {
            Layer = layer;
        }

        public MailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}