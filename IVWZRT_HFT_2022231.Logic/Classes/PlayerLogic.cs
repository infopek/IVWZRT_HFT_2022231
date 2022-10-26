using System;
using System.Linq;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Logic
{
    public class PlayerLogic : IPlayerLogic
    {
        public PlayerLogic(IRepository<Player> repo)
        {
            _repo = repo;
        }

        // CRUD
        public IQueryable<Player> ReadAll()
        {
            return _repo.ReadAll();
        }
        public Player Read(int id)
        {
            Player player = _repo.Read(id);
            return player ?? throw new ArgumentException("Player with id does not exist");
        }
        public void Create(Player item)
        {
            _repo.Create(item);
        }
        public void Update(Player item)
        {
            _repo.Update(item);
        }
        public void Delete(int id)
        {
            Player player = _repo.Read(id);
            if (player == null)
                throw new ArgumentException("Player with id does not exist");

            _repo.Delete(id); 
        }

        // NON-CRUD
        public IEnumerable<Player> PlayersWithGreaterKD(string rank)
        {
            return null;
        }
        public int NumTimesTopThree(string username)
        {
            return 0;
        }

        private IRepository<Player> _repo;
    }
}
