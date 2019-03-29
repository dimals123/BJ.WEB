using BJ.DataAccess.Entities;
using BJ.DataAccess.Entities.Enums;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DataAccess.Repositories
{
    public class CardRepository:GenericRepository<Card>, ICardRepository
    {
        public CardRepository(BJContext context):base(context)
        {

        }

        public async Task<List<Card>> GetAllByGameId(Guid gameId)
        {
            var deck = await _dbSet.Select(x => x)
                .Where(x => x.GameId == gameId)
                .ToListAsync();
            return deck;
        }


        public async Task<List<Card>> CreateDeck(Guid gameId)
        {
            var cards = new List<Card>();

         

            foreach (var suit in Enum.GetValues(typeof(SuitType)))
            {
                foreach(var rank in Enum.GetValues(typeof(RankType)))
                {
                    cards.Add(new Card
                    {
                        Suit = (SuitType)suit,
                        Rank = (RankType)rank,
                        GameId = gameId
                    });
                }
            }
            Swap(cards);
            await _dbSet.AddRangeAsync(cards);
            return cards;

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

    }
}
