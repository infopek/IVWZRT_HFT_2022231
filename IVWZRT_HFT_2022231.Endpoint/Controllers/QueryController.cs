using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        public QueryController(IPlayerLogic playerLogic, IMatchLogic matchLogic)
        {
            _playerLogic = playerLogic;
            _matchLogic = matchLogic;
        }

        // Player queries
        [HttpGet("{rank}")]
        public IEnumerable<PlayerLogic.PlayerRankInfo> PlayersWithGreaterKD(string rank)
        {
            return _playerLogic.PlayersWithGreaterKD(rank);
        }
        [HttpGet("{username}")]
        public int NumTimesTopThree(string username)
        {
            return _playerLogic.NumTimesTopThree(username);
        }

        // Match queries
        [HttpGet("{gameMode}")]
        public float AvgLengthOfGame(string gameMode)
        {
            return _matchLogic.AvgLengthOfGame(gameMode);
        }
        [HttpGet]
        public IEnumerable<string> MapsWithMostRamparts()
        {
            return _matchLogic.MapsWithMostRamparts();
        }
        [HttpGet]
        public IEnumerable<Match> LongestMatchesInDiamond()
        {
            return _matchLogic.LongestMatchesInDiamond();
        }

        private readonly IPlayerLogic _playerLogic;
        private readonly IMatchLogic _matchLogic;
    }
}