using BJ.DAL.Entities;
using BJ.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.DAL.Repositories
{
    public class PointUserRepository:GenericRepository<PointAccount>, IPointUserRepository
    {
        public PointUserRepository(BJContext context): base(context)
        {

        }
    }
}
