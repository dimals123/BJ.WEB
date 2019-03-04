using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.BLL.Exceptions
{
    public class CustomServiceException:ApplicationException
    {
        public CustomServiceException(string message) : base(message) { }
    }
}
