using System.Linq;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Logic
{
    public interface IMatchLogic
    {
        // CRUD
        IQueryable<Match> ReadAll();
        Match Read(int id);
        void Create(Match item);
        void Update(Match item);
        void Delete(int id);

        // NON-CRUD     
        float AvgLengthOfGame(string gameMode);
        IEnumerable<string> MapsWithMostRamparts();
        IEnumerable<Match> LongestMatchesInDiamond();
    }
}

