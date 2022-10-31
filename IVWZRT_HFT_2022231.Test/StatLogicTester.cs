using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Test
{
    [TestFixture]
    public class StatLogicTester
    {
        [SetUp]
        public void Init()
        {
            _mockLegendRepo = new Mock<IRepository<EndGameStat>>();
            _mockLegendRepo.Setup(lr => lr.ReadAll()).Returns(new List<EndGameStat>()
            {
                new EndGameStat("14@10@5@1000@5@7"),
                new EndGameStat("15@11@6@1100@6@6"),
                new EndGameStat("16@12@7@1200@7@5")
            }.AsQueryable());

            _logic = new StatLogic(_mockLegendRepo.Object);
        }

        // Create tests
        [Test]
        public void Create_StatWithValidParams_Runs()
        {
            var stat = new EndGameStat("1@13@4.5@2400@5@7");

            _logic.Create(stat);

            _mockLegendRepo.Verify(r => r.Create(stat), Times.Once);
        }
        [Test]
        public void Create_StatWith0KP0Dmg_Runs()
        {
            var stat = new EndGameStat("1@13@0@0@5@7");

            _logic.Create(stat);

            _mockLegendRepo.Verify(r => r.Create(stat), Times.Once);
        }

        [Test]
        public void Create_StatWithNegativeKp_NeverRuns()
        {
            var stat = new EndGameStat("1@13@-4.5@2400@5@7");

            try
            {
                _logic.Create(stat);
            }
            catch
            {

            }

            _mockLegendRepo.Verify(r => r.Create(stat), Times.Never);
        }
        [Test]
        public void Create_StatWithNegativeDmg_NeverRuns()
        {
            var stat = new EndGameStat("1@13@4.5@-2400@5@7");

            try
            {
                _logic.Create(stat);
            }
            catch
            {

            }

            _mockLegendRepo.Verify(r => r.Create(stat), Times.Never);
        }

        private StatLogic _logic;
        private Mock<IRepository<EndGameStat>> _mockLegendRepo;
    }
}