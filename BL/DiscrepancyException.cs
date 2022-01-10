using System;
using System.Runtime.Serialization;
using BE;

namespace BL
{
    [Serializable]
    internal class DiscrepancyException : Exception
    {
        public Layers Layer { get; set; }

        public DiscrepancyException()
        {
        }

        public DiscrepancyException(string message) : base(message)
        {
        }

        public DiscrepancyException(Layers layer, string message) : base(message)
        {
            Layer = layer;
        }

        public DiscrepancyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DiscrepancyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}