using System.Linq;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Logic
{
    public interface IStatLogic
    {
        // CRUD
        IQueryable<EndGameStat> ReadAll();
        EndGameStat Read(int id);
        void Create(EndGameStat item);
        void Update(EndGameStat item);
        void Delete(int id);
    }
}
