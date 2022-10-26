using System;
using System.Linq;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Logic
{
    public class MatchLogic : IMatchLogic
    {
        public MatchLogic(IRepository<Match> repo)
        {
            _repo = repo;
        }

        // CRUD
        public IQueryable<Match> ReadAll()
        {
            return _repo.ReadAll();
        }
        public Match Read(int id)
        {
            Match match = _repo.Read(id);
            return match ?? throw new ArgumentException("Match with id does not exist");
        }
        public void Create(Match item)
        {
            _repo.Create(item);
        }
        public void Update(Match item)
        {
            _repo.Update(item);
        }
        public void Delete(int id)
        {
            Match match = _repo.Read(id);
            if (match == null)
                throw new ArgumentException("Match with id does not exist");

            _repo.Delete(id);
        }

        // NON-CRUD
        public float AvgDamagePerGame()
        {
            return 0.0f;
        }
        public IEnumerable<Match> GetPlayersBestMatch()
        {
            return null;
        }
        public Match LongestMatchInDiamond()
        {
            return null;
        }

        private IRepository<Match> _repo;
    }
}
