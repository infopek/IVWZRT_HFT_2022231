using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Models;
using Microsoft.AspNetCore.SignalR;
using IVWZRT_HFT_2022231.Endpoint.Services;
using System.Numerics;

namespace IVWZRT_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        public PlayerController(IPlayerLogic logic, IHubContext<SignalRHub> hub)
        {
            _logic = logic;
            _hub = hub;
        }

        [HttpGet]
        public IEnumerable<Player> ReadAll()
        {
            return _logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Player Read(int id)
        {
            return _logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Player player)
        {
            _logic.Create(player);
            _hub.Clients.All.SendAsync("PlayerCreated", player);
        }

        [HttpPut]
        public void Update([FromBody] Player player)
        {
            _logic.Update(player);
            _hub.Clients.All.SendAsync("PlayerUpdated", player);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var toDelete = _logic.Read(id);
            _logic.Delete(id);
            _hub.Clients.All.SendAsync("PlayerDeleted", toDelete);
        }

        private readonly IPlayerLogic _logic;

        private readonly IHubContext<SignalRHub> _hub;
    }
}
