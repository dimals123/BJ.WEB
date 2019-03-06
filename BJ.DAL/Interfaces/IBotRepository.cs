using BJ.DAL.Entities;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IBotRepository:IGeneric<Bot>
    {
        bool IsCard(PointBot pointBot);
       
    }
}
