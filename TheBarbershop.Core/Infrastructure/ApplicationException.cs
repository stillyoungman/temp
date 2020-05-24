using System;
using System.Collections.Generic;
using System.Text;

namespace TheBarbershop.Core.Infrastructure
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message)
        {
        }
    }
}
