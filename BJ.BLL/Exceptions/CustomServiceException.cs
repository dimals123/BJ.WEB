using System;

namespace BJ.BLL.Exceptions
{
    public class CustomServiceException:ApplicationException
    {
        public CustomServiceException(string message) : base(message) { }
    }
}
