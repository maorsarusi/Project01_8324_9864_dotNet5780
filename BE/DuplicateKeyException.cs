using System;
using System.Runtime.Serialization;

namespace BE
{
    [Serializable]
    public class DuplicateKeyException : Exception
    {
        public Layers Layer { get; set; }
        public int Key { get; set; }

        public DuplicateKeyException()
        {
        }

        public DuplicateKeyException(string message) : base(message)
        {
        }

        public DuplicateKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public DuplicateKeyException(Layers layer, string message, int key) : base(message)
        {
            Layer = layer;
            Key = key;
        }

        protected DuplicateKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}