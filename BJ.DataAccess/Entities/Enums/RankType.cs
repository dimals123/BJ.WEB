using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BJ.DataAccess.Entities.Enums
{
    public enum RankType
    {
        [Display(Name = "6")]
        [Description("6")]
        Six = 0,
        [Display(Name = "7")]
        [Description("7")]
        Seven = 1,
        [Display(Name = "8")]
        [Description("8")]
        Eight = 2,
        [Display(Name = "9")]
        [Description("9")]
        Nine = 3,
        [Display(Name = "10")]
        [Description("10")]
        Ten = 4,
        [Display(Name = "2")]
        [Description("2")]
        Jack = 5,
        [Display(Name = "3")]
        [Description("3")]
        Lady = 6,
        [Display(Name = "4")]
        [Description("4")]
        King = 7,
        [Display(Name = "11")]
        [Description("11")]
        Ace = 8
    }
}
