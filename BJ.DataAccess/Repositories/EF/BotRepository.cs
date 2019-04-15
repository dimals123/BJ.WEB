using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BJ.DataAccess.Entities;
using BJ.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BJ.DataAccess.Repositories.EF
{
    public class BotRepository:BaseRepository<Bot>, IBotRepository
    {
        public BotRepository(BJContext context):base(context)
        {

        }

      

    }
}
