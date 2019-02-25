using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.DAL.Repositories
{
    public class PointAccountRepository:GenericRepository<PointAccount>, IPointAccountRepository
    {
        public PointAccountRepository(BJContext context): base(context)
        {

        }
    }
}
