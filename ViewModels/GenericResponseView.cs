using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class GenericResponseView<T>
    {
        public T Model { get; set; }
        public string Error { get; set; }
    }
}
