using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleApps.Interfaces.Exceptions
{
    public class GoogleApiException :Exception
    {
        public GoogleApiException(string message):base(message)
        {            
        }
        public GoogleApiException() { }
    }
}
