﻿using System;
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
        public string MapWhereRampartMostUsed()
        {
            return "";
        }
        public Match LongestMatchInDiamond()
        {
            return null;
        }

        private static readonly string[] _validModes = { "trios", "duos", "ranked leagues", "arenas", "ranked arenas" };

        private static readonly string[] _validBRMaps = { "kings canyon", "world's edge", "olympus", "storm point" };
        private static readonly string[] _validArenasMaps = { "drop off", "habitat 4", "encore", "overflow", "party crasher",
            "phase runner", "rotating map" };

        private IRepository<Match> _repo;
    }
}
