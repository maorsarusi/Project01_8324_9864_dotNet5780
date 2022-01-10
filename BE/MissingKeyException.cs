using System;
using System.Runtime.Serialization;

namespace BE
{
    [Serializable]
    public class MissingKeyException : Exception
    {
        public Layers Layer { get; set; }
        public int Key { get; set; }

        public MissingKeyException()
        {
        }

        public MissingKeyException(string message) : base(message)
        {
        }

        public MissingKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MissingKeyException(Layers layer, string message, int key) : base(message)
        {
            Layer = layer;
            Key = key;
        }

        protected MissingKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}