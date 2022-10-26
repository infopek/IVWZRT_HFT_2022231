using System.Collections.Generic;
using System.Linq;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Logic
{
    public interface IPlayerLogic
    {
        // CRUD
        IQueryable<Player> ReadAll();
        Player Read(int id);
        void Create(Player item);
        void Update(Player item);
        void Delete(int id);

        // NON-CRUD
        IEnumerable<Player> PlayersWithGreaterKD(string rank);
        int NumTimesTopThree(string username);
    }
}
