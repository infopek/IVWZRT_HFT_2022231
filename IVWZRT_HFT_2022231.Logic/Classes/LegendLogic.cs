using System;
using System.Linq;

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

        // Helper class
        public class HeadshotInfo
        {
            public override bool Equals(object obj)
            {
                HeadshotInfo other = obj as HeadshotInfo;
                if (other == null)
                    return false;
                else
                    return Name == other.Name && TotalHeadshots == other.TotalHeadshots;
            }
            public override int GetHashCode()
            {
                return Name.GetHashCode() ^ TotalHeadshots.GetHashCode();
            }

            public string Name { get; set; }
            public int TotalHeadshots { get; set; }
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
            string name = item.Name.ToLower();

            if (!_validLegends.Contains(name))
                throw new ArgumentException("Character does not exist");
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

        private static readonly string[] _validLegends = {
            "wraith", "bangalore", "mirage", "octane", "revenant", "horizon", "fuse", "ash", "mad maggie",
            "gibraltar", "caustic", "wattson", "rampart", "newcastle",
            "lifeline", "loba",
            "bloodhound", "pathfinder", "crypto", "valkyrie", "seer" 
        };

        private IRepository<Legend> _repo;
    }
}

