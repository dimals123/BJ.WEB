using BJ.BusinessLogic.Helpers.Interfaces;
using BJ.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.BusinessLogic.Helpers
{
    public class CardsHelper:ICardsHelper
    {
        public void Swap(List<Card> cards)
        {
            var random = new Random();
            int swapTo, swapFrom;
            for (int i = 0; i < 1000; i++)
            {
                swapFrom = random.Next(35);
                swapTo = random.Next(35);

                Card temp = cards[swapTo];
                cards[swapTo] = cards[swapFrom];
                cards[swapFrom] = temp;
            }
        }
    }
}
