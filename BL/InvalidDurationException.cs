using System;
using System.Runtime.Serialization;
using BE;

namespace BL
{
    [Serializable]
    internal class InvalidDurationException : Exception
    {
        public Layers Layer { get; set; }
        public int Duration { get; set; }

        public InvalidDurationException()
        {
        }

        public InvalidDurationException(string message) : base(message)
        {
        }

        public InvalidDurationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidDurationException(Layers layer, string message, int duration) : base(message)
        {
            Layer = layer;
            Duration = duration;
        }

        protected InvalidDurationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}