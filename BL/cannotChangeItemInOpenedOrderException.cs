using System;
using System.Runtime.Serialization;
using BE;

namespace BL
{
    [Serializable]
    internal class CannotChangeItemInOpenedOrderException : Exception
    {
        public Layers layer;

        public CannotChangeItemInOpenedOrderException()
        {
        }

        public CannotChangeItemInOpenedOrderException(string message) : base(message)
        {
        }

        public CannotChangeItemInOpenedOrderException(Layers layer, string message) : base(message)
        {
            this.layer = layer;
        }

        public CannotChangeItemInOpenedOrderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CannotChangeItemInOpenedOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}