using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    public class FakerException : Exception
    {
        public FakerException() : base() { }
        public FakerException(string message): base(message) { }
        public FakerException(string message, Exception innerException): base(message, innerException) { }
    }
}
