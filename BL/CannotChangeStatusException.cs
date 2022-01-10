using System;
using System.Runtime.Serialization;
using BE;

namespace BL
{
    [Serializable]
    internal class CannotChangeStatusException : Exception
    {
        public Layers Layer { get; set; }

        public CannotChangeStatusException()
        {
        }

        public CannotChangeStatusException(string message) : base(message)
        {
        }

        public CannotChangeStatusException(Layers layer, string message) : base(message)
        {
            Layer = layer;
        }

        public CannotChangeStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotChangeStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}