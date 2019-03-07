using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories
{
    public class PointUserRepository:GenericRepository<PointUser>, IPointUserRepository
    {
        public PointUserRepository(BJContext context): base(context)
        {

        }
        public PointUser GetMax(string userId, Guid gameId, List<PointUser> pointsUser)
        {
            
            var pointMaxUser = pointsUser.FirstOrDefault();
            if (pointMaxUser != null)
            {  
                for (int i = 0; i < pointsUser.Count - 1; i++)
                {
                    if (pointMaxUser.CountPoint < pointsUser[i + 1].CountPoint)
                    {
                        pointMaxUser = pointsUser[i + 1];
                    }
                }
                return pointMaxUser;
            }
            else
                return null;
        }

       
    }
}
