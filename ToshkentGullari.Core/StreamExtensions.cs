using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshkentGullari
{
    public static class StreamExtensions
    {
        public static String ReadAsText(this Stream value)
        {
            // Again, we have a value to read from, but we must
            // defend ourselves from being treated with null values
            if (value == null)
                throw new ArgumentNullException("value");

            // next, we must make sure that we can read this stream. If it is not
            // readable, the whole method doesn't make sense.
            if (!value.CanRead)
                throw new Exception("Unreadable stream");

            // Once we get there, we can safely start reading data
            // Thus, let's create a byte array that will store our bytes temporarily
            // Its size will be the same as the lenght of the stream
            var buffer = new Byte[value.Length];

            // This is the actual method of reading
            // Read it into buffer starting from 0 till the last byte
            value.Read(buffer, 0, buffer.Length);

            // Encoding is a class that allows conversion from strings into bytes
            // and vice versa. Here we need to get string from the byte array
            return Encoding.UTF8.GetString(buffer);
        }
    }
}
