﻿using BJ.DAL.Entities;
using BJ.DAL.Entities.Enums;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.BLL.Configurations
{
    public class DbInitialize
    {
        #region(Bots)
        private static readonly string[] _botsName = new string[] { "Olivia","Amelia","Isla","Emily","Ava","Lily","Mia", "Sofia", "Isabella","Grace",
                                                             "Oliver","Harry","Jack","George","Noah","Charlie", "Jacob","Alfie","Freddie","Oscar"};

        public async static Task<List<Bot>> Init(IUnitOfWork unitOfWork, int countBots)
        {
            List<Bot> bots = new List<Bot>();
            for (int i = 0; i < countBots; i++)
            {
                var bot = new Bot()
                {
                    Name = _botsName[i]
                };
                bots.Add(bot);

            }
            await unitOfWork.Bots.CreateRange(bots);
            unitOfWork.Save();
            return bots;
        }
        #endregion

        #region(Cards)
        private static List<Card> _cards = new List<Card>();

        public async static Task<List<Card>> Init(IUnitOfWork unitOfWork)
        {
            for (int i = 0; i < Enum.GetNames(typeof(SuitType)).Length; i++)
            {
                for (int j = 0; j < Enum.GetNames(typeof(RankType)).Length; j++)
                {
                    _cards.Add(new Card { Suit = (SuitType)Enum.GetNames(typeof(SuitType)).GetValue(i), Value = (RankType)Enum.GetNames(typeof(RankType)).GetValue(j) });
                }
            }
            Swap(_cards);
            await unitOfWork.Cards.CreateRange(_cards);
            return _cards;
        }

        private static void Swap(List<Card> cards)
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
        #endregion
    }
}
