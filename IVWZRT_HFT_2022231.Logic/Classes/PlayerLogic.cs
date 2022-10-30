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

        public class PlayerRankInfo
        {
            public override bool Equals(object obj)
            {
                PlayerRankInfo other = obj as PlayerRankInfo;
                if (other == null)
                    return false;
                else
                    return other.UserName == UserName && other.Rank == Rank && other.KDRatio == KDRatio;
            }
            public override int GetHashCode()
            {
                return UserName.GetHashCode() ^ Rank.GetHashCode() ^ KDRatio.GetHashCode();
            }

            public string UserName { get; set; }
            public string Rank { get; set; }
            public float KDRatio { get; set; }
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
            string rank = item.Rank.ToLower();

            if (item.UserName == null || item.UserName == "")
                throw new ArgumentException("A player must have a username");
            if (!_validRanks.Contains(rank))
                throw new ArgumentException("Provided rank does not exist");
            if (item.NumGames < 0 || item.TotalDeaths < 0 || item.TotalKills < 0)
                throw new ArgumentException("Number of games, total deaths and total kills must be non-negative");

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
        
        /// <summary>
        /// Returns all the players whose kill/death ratio is (strictly) greater than 2.0 in a given <paramref name="rank"/>
        /// </summary>
        public IEnumerable<PlayerRankInfo> PlayersWithGreaterKD(string rank)
        {
            return from p in _repo.ReadAll()
                   where p.Rank == rank
                   && ((float)p.TotalKills / p.TotalDeaths) > 2.0f
                   select new PlayerRankInfo
                   {
                       UserName = p.UserName,
                       Rank = rank,
                       KDRatio = (float)p.TotalKills / p.TotalDeaths
                   };
        }
        /// <summary>
        /// Returns the number of times a player with username <paramref name="username"/> has finished in the top 3.
        /// </summary>
        public int NumTimesTopThree(string username)
        {
            return (from p in _repo.ReadAll()
                   where p.UserName == username
                   select p.Stats.Count(s => s.Placement <= 3)).First();
        }

        private static readonly string[] _validRanks = { "unranked", "rookie", "bronze", "silver", "gold",
            "platinum", "diamond", "master", "predator" };

        private IRepository<Player> _repo;
    }
}
