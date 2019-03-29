using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.BusinessLogic.Helpers.Interfaces
{
    public interface ICardsHelper
    {
        void Swap(List<Card> cards);
    }
}
