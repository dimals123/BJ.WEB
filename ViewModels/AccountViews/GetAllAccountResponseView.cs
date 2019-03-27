using System.Collections.Generic;

namespace BJ.ViewModels.AccountViews
{
    public class GetAllAccountResponseView
    {
        public List<string> AccountNames { get; set; }

        public GetAllAccountResponseView()
        {
            AccountNames = new List<string>();
        }
    }

    
}
