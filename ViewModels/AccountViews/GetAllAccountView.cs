﻿using System.Collections.Generic;

namespace ViewModels.AccountViews
{
    public class GetAllAccountView
    {
        public List<string> AccountNames { get; set; }

        public GetAllAccountView()
        {
            AccountNames = new List<string>();
        }
    }

    
}
