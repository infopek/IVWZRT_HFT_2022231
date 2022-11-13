using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LegendController : ControllerBase
    {
        public LegendController(ILegendLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public IEnumerable<Legend> ReadAll()
        {
            return _logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Legend Read(int id)
        {
            return _logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Legend legend)
        {
            _logic.Create(legend);
        }

        [HttpPut]
        public void Update([FromBody] Legend legend)
        {
            _logic.Update(legend);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logic.Delete(id);
        }

        private readonly ILegendLogic _logic;
    }
}
