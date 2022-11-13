using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        public MatchController(IMatchLogic logic)
        {
            _logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Match match)
        {
            _logic.Update(match);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logic.Delete(id);
        }

        private readonly IMatchLogic _logic;
    }
}
