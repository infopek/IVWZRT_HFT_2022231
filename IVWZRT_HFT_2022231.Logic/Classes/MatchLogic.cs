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
            string gameMode = item.GameMode.ToLower();
            string map = item.Map.ToLower();

            // Check game mode and map validity
            if (!_validModes.Contains(gameMode))
                throw new ArgumentException("Invalid game mode provided");
            else if (!(gameMode == "arenas" || gameMode == "ranked arenas") && !_validBRMaps.Contains(map))
                throw new ArgumentException("Invalid map provided for battle royale");
            else if ((gameMode == "arenas" || gameMode == "ranked arenas") && !_validArenasMaps.Contains(map) )
                throw new ArgumentException("Invalid map provided for arenas");

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
        /// <summary>
        /// Returns the average damage of players in <paramref name="gamemode"/>
        /// </summary>
        public float AvgLengthOfGame(string gameMode)
        {
            return (from m in _repo.ReadAll()
                   where m.GameMode == gameMode
                   group m by m.GameMode into gr
                   select gr.Average(m => m.Length)).First();
        }
        /// <summary>
        /// Returns the map(s) of match(es) where the most ramparts were played at the time.
        /// </summary>
        public IEnumerable<string> MapsWithMostRamparts()
        {
            int maxUse = _repo.ReadAll().Max(m => m.Players.Count(p => p.Legend.Name.ToLower() == "rampart"));

            return from m in _repo.ReadAll()
                   where m.Players.Count(p => p.Legend.Name.ToLower() == "rampart") == maxUse
                   select m.Map;
        }
        /// <summary>
        /// Returns the longest match(es) played in diamond ranked leagues.
        /// </summary>
        public IEnumerable<Match> LongestMatchesInDiamond()
        {
            // Note: I'll assume that a diamond player can only be in a diamond ranked lobby (realistically, this is NOT the case)
            float longest = _repo.ReadAll()
                .Where(m => m.GameMode.ToLower() == "ranked leagues" && m.Players.Any(p => p.Rank.ToLower() == "diamond"))
                .Max(m => m.Length);

            return from m in _repo.ReadAll()
                   where m.GameMode.ToLower() == "ranked leagues"
                   && m.Players.Any(p => p.Rank.ToLower() == "diamond")
                   && m.Length == longest
                   select m;
        }

        private static readonly string[] _validModes = { "trios", "duos", "ranked leagues", "arenas", "ranked arenas" };

        private static readonly string[] _validBRMaps = { "kings canyon", "world's edge", "olympus", "storm point" };
        private static readonly string[] _validArenasMaps = { "drop off", "habitat 4", "encore", "overflow", "party crasher",
            "phase runner", "rotating map" };

        private IRepository<Match> _repo;
    }
}
