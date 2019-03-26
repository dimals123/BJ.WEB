using BJ.DAL.Entities;
using BJ.DAL.Entities.Enums;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async static Task<List<Bot>> InitBots(IUnitOfWork unitOfWork, int countBots, Guid gameId)
        {

            var bots = new List<Bot>();
            var CountBots = _botsName.Length;
            if (await unitOfWork.Bots.GetFirst() == null)
            {
                bots = _botsName.Select(x => new Bot
                {
                    Name = x
                }).ToList();

                //for (int i = 0; i < _botsName.Length; i++)
                //{
                //    var bot = new Bot()
                //    {
                //        Name = _botsName[i]
                //    };
                //    bots.Add(bot);

                //}
                await unitOfWork.Bots.CreateRange(bots);
                bots = await unitOfWork.Bots.GetRangeByCount(countBots);
            }
            else if (CountBots < countBots)
            {
                throw new ValidationException("too many bots!");
            }
            else
            {
                var botInGames = await unitOfWork.BotInGames.GetAllByGameId(gameId);
                bots = botInGames.Select(x=>x.Bot).ToList();
                if(bots.FirstOrDefault() == null)
                {
                    bots = await unitOfWork.Bots.GetRangeByCount(countBots);
                }
            }
            //transactionScope.Complete();
            return bots;

        }
        #endregion

        #region(Cards)
       

        public async static Task<List<Card>> InitCards(IUnitOfWork unitOfWork, Guid gameId)
        {
            //using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled))
            //{
            var cards = await unitOfWork.Cards.GetAll();
            if (cards.FirstOrDefault() != null)
                await unitOfWork.Cards.DeleteRange(cards);

            var _cards = new List<Card>();
            



            for (int i = 0; i < Enum.GetNames(typeof(SuitType)).Length; i++)
            {
                for (int j = 0; j < Enum.GetNames(typeof(RankType)).Length; j++)
                {
                    _cards.Add(new Card
                    {
                        Suit = (SuitType)Enum.GetValues(typeof(SuitType)).GetValue(i),
                        Rank = (RankType)Enum.GetValues(typeof(RankType)).GetValue(j)
                        //GameId = gameId
                    });
                }
            }
            Swap(_cards);
            await unitOfWork.Cards.CreateRange(_cards);
            //transactionScope.Complete();
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
