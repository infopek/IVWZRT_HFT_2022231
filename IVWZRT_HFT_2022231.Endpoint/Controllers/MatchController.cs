using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Models;
using IVWZRT_HFT_2022231.Endpoint.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;
using Match = IVWZRT_HFT_2022231.Models.Match;

namespace IVWZRT_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        public MatchController(IMatchLogic logic, IHubContext<SignalRHub> hub)
        {
            _logic = logic;
            _hub = hub;
        }

        [HttpGet]
        public IEnumerable<Match> ReadAll()
        {
            return _logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Match Read(int id)
        {
            return _logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Match match)
        {
            _logic.Create(match);
            _hub.Clients.All.SendAsync("MatchCreated", match);
        }

        [HttpPut]
        public void Update([FromBody] Match match)
        {
            _logic.Update(match);
            _hub.Clients.All.SendAsync("MatchUpdated", match);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Match toDelete = _logic.Read(id);
            _logic.Delete(id);
            _hub.Clients.All.SendAsync("MatchDeleted", toDelete);
        }

        private readonly IMatchLogic _logic;

        private readonly IHubContext<SignalRHub> _hub;
    }
}
