using System;
using System.Linq;

using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Logic
{
    public class StatLogic : IStatLogic
    {
        public StatLogic(IRepository<EndGameStat> repo)
        {
            _repo = repo;
        }

        // CRUD
        public IQueryable<EndGameStat> ReadAll()
        {
            return _repo.ReadAll();
        }
        public EndGameStat Read(int id)
        {
            EndGameStat stat = _repo.Read(id);
            return stat ?? throw new ArgumentException("Stat with id does not exist");
        }
        public void Create(EndGameStat item)
        {
            if (item.Damage < 0 || item.KillPoints < 0)
                throw new ArgumentException("Damage and kill points must be non-negative");
            _repo.Create(item);
        }
        public void Update(EndGameStat item)
        {
            _repo.Update(item);
        }
        public void Delete(int id)
        {
            EndGameStat stat = _repo.Read(id);
            if (stat == null)
                throw new ArgumentException("Stat with id does not exist");

            _repo.Delete(id);
        }

        // NON-CRUD
        public int MostDamageAsPathfinder()
        {
            return 0;
        }

        private IRepository<EndGameStat> _repo;
    }
}
