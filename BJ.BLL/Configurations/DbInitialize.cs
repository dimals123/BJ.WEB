using BJ.DAL.Entities;
using BJ.DAL.Entities.Enums;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BJ.BLL.Configurations
{
    public class DbInitialize
    {
        #region(Bots)
        private static readonly string[] _botsName = new string[] { "Olivia","Amelia","Isla","Emily","Ava","Lily","Mia", "Sofia", "Isabella","Grace",
                                                             "Oliver","Harry","Jack","George","Noah","Charlie", "Jacob","Alfie","Freddie","Oscar"};

        public async static Task<List<Bot>> InitBots(IUnitOfWork unitOfWork, int countBots)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted}, TransactionScopeAsyncFlowOption.Enabled))
            {
                List<Bot> bots = new List<Bot>();
                var CountBots = (await unitOfWork.Bots.GetAll()).Count;
                if (await unitOfWork.Bots.GetFirst() == null)
                {
                    for (int i = 0; i < countBots; i++)
                    {
                        var bot = new Bot()
                        {
                            Name = _botsName[i]
                        };
                        bots.Add(bot);

                    }
                    await unitOfWork.Bots.CreateRange(bots);
                    await unitOfWork.Save();
                }
                else if (CountBots < countBots)
                {
                    for (int i = 0; i < countBots - CountBots; i++)
                    {
                        var bot = new Bot()
                        {
                            Name = _botsName[CountBots + i]
                        };
                        bots.Add(bot);
                    }
                    await unitOfWork.Bots.CreateRange(bots);
                    await unitOfWork.Save();
                }
                else bots = await unitOfWork.Bots.GetAll();
                transactionScope.Complete();
                return bots;
            }
        }
        #endregion

        #region(Cards)
        private static List<Card> _cards = new List<Card>();

        public async static Task<List<Card>> InitCards(IUnitOfWork unitOfWork)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                var cards = await unitOfWork.Cards.GetAll();
                if (cards.FirstOrDefault() != null)
                    unitOfWork.Cards.DeleteRange(cards);


                for (int i = 0; i < Enum.GetNames(typeof(SuitType)).Length; i++)
                {
                    for (int j = 0; j < Enum.GetNames(typeof(RankType)).Length; j++)
                    {
                        _cards.Add(new Card { Suit = (SuitType)Enum.GetValues(typeof(SuitType)).GetValue(i), Rank = (RankType)Enum.GetValues(typeof(RankType)).GetValue(j) });
                    }
                }
                Swap(_cards);
                await unitOfWork.Cards.CreateRange(_cards);
                await unitOfWork.Save();
                transactionScope.Complete();
                return _cards;
            }
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
