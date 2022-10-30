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
    public class LegendLogicTester
    {
        [SetUp]
        public void Init()
        {
            _mockLegendRepo = new Mock<IRepository<Legend>>();
            _mockLegendRepo.Setup(lr => lr.ReadAll()).Returns(new List<Legend>()
            {
                new Legend("1@Wraith@Skin"),
                new Legend("2@Wraith@Skin"),
                new Legend("3@Octane@Skin"),
                new Legend("4@Valkyrie@Skin"),
                new Legend("5@Valkyrie@Skin"),
                new Legend("6@Valkyrie@Skin"),
                new Legend("7@Gibraltar@Skin"),
                new Legend("8@Loba@Skin")
            }.AsQueryable());

            _logic = new LegendLogic(_mockLegendRepo.Object);
        }

        // Create tests
        [Test]
        public void Create_LegendWithValidParams_Runs()
        {
            var legend = new Legend("45@Octane@Jeli");

            _logic.Create(legend);

            _mockLegendRepo.Verify(r => r.Create(legend), Times.Once);
        }

        [Test]
        public void Create_LegendWithInvalidName_NeverRuns()
        {
            var legend = new Legend() { Name = "Hab" };
            try
            {
                _logic.Create(legend);
            }
            catch
            {

            }

            _mockLegendRepo.Verify(r => r.Create(legend), Times.Never);
        }

        private LegendLogic _logic;
        private Mock<IRepository<Legend>> _mockLegendRepo;
    }
}
