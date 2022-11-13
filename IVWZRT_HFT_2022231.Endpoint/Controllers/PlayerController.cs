using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        public PlayerController(IPlayerLogic logic)
        {
            _logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Player player)
        {
            _logic.Update(player);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logic.Delete(id);
        }

        private readonly IPlayerLogic _logic;
    }
}
