using System;
using System.Runtime.Serialization;
namespace ToshkentGullari {
    [Serializable]
    public class InvalidRestMethodException : Exception {
        public InvalidRestMethodException() { }
        public InvalidRestMethodException(String message) : base(message) { }
        public InvalidRestMethodException(String message, Exception inner) : base(message, inner) { }
        protected InvalidRestMethodException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}