using System;

namespace BJ.BusinessLogic.Exceptions
{
    public class CustomServiceException:ApplicationException
    {
        public CustomServiceException(string message) : base(message) { }
    }
}
