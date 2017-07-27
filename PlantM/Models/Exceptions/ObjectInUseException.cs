using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantM.Models.Exceptions
{
    public class ObjectInUseException : InvalidOperationException
    {
        public ObjectInUseException(string message)
            :base(message)
        {
            
        }
    }
}