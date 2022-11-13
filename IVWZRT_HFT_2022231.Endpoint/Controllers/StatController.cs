using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        public StatController(IStatLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public IEnumerable<EndGameStat> ReadAll()
        {
            return _logic.ReadAll();
        }

        [HttpGet("{id}")]
        public EndGameStat Read(int id)
        {
            return _logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] EndGameStat stat)
        {
            _logic.Create(stat);
        }

        [HttpPut]
        public void Update([FromBody] EndGameStat stat)
        {
            _logic.Update(stat);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logic.Delete(id);
        }

        private readonly IStatLogic _logic;
    }
}
