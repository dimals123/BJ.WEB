using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.AccountViews
{
    public class GetAllAccountView
    {
        public List<AccountGetAllAccountViewItem> AccountNames { get; set; }

        public GetAllAccountView()
        {
            AccountNames = new List<AccountGetAllAccountViewItem>();
        }
    }

    public class AccountGetAllAccountViewItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
