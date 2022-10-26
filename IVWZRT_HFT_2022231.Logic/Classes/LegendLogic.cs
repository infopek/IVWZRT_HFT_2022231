using System;
using System.Linq;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Logic
{
    public class LegendLogic : ILegendLogic
    {
        public LegendLogic(IRepository<Legend> repo)
        {
            _repo = repo;
        }

        // CRUD
        public IQueryable<Legend> ReadAll()
        {
            return _repo.ReadAll();
        }
        public Legend Read(int id)
        {
            Legend legend = _repo.Read(id);
            return legend ?? throw new ArgumentException("Legend with id does not exist");
        }
        public void Create(Legend item)
        {
            _repo.Create(item);
        }
        public void Update(Legend item)
        {
            _repo.Update(item);
        }
        public void Delete(int id)
        {
            Legend legend = _repo.Read(id);
            if (legend == null)
                throw new ArgumentException("Legend with id does not exist");

            _repo.Delete(id);
        }

        // NON-CRUD
        public IEnumerable<Legend> MostUsedLegendByRank()
        {
            return null;
        }
        public IEnumerable<int> NumGamesByDefensiveLegend()
        {
            return null;
        }

        private IRepository<Legend> _repo;
    }
}

