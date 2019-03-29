using System.Collections.Generic;

namespace BJ.ViewModels.AccountViews
{
    public class GetAllAccountResponseView
    {
        public List<string> Names { get; set; }

        public GetAllAccountResponseView()
        {
            Names = new List<string>();
        }
    }

    
}
