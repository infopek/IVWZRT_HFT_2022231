using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;

using IVWZRT_HFT_2022231.Logic;
using static IVWZRT_HFT_2022231.Logic.PlayerLogic;
using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Test
{
    [TestFixture]
    public class PlayerLogicTester
    {
        [SetUp]
        public void Init()
        {
            _mockRepo = new Mock<IRepository<Player>>();
            _mockRepo.Setup(r => r.ReadAll()).Returns(new List<Player>()
            {
                new Player("1@Player1@20@1000@13000@12000@Platinum@3"),
                new Player("2@Player2@20@2000@13000@12000@Gold@2"),
                new Player
                {
                    PlayerId = 3,
                    UserName = "Player3",
                    Age = 20,
                    LegendId = 7,
                    Stats = new List<EndGameStat>
                    {
                        new EndGameStat
                        {
                            StatId = 1,
                            Placement = 7,
                            PlayerId = 3,
                            MatchId = 1
                        },
                        new EndGameStat
                        {
                            StatId = 2,
                            Placement = 15,
                            PlayerId = 3,
                            MatchId = 2
                        },
                        new EndGameStat
                        {
                            StatId = 3,
                            Placement = 3,
                            PlayerId = 3,
                            MatchId = 3
                        },
                        new EndGameStat
                        {
                            StatId = 4,
                            Placement = 4,
                            PlayerId = 3,
                            MatchId = 4
                        },
                        new EndGameStat
                        {
                            StatId = 5,
                            Placement = 1,
                            PlayerId = 3,
                            MatchId = 5
                        },
                    }
            },
                new Player
                {
                    PlayerId = 4,
                    UserName = "Player4",
                    Age = 21,
                    LegendId = 3,
                    Stats = new List<EndGameStat>
                    {
                        new EndGameStat
                        {
                            StatId = 6,
                            Placement = 1,
                            PlayerId = 4,
                            MatchId = 1
                        },
                        new EndGameStat
                        {
                            StatId = 7,
                            Placement = 20,
                            PlayerId = 4,
                            MatchId = 2
                        },
                        new EndGameStat
                        {
                            StatId = 8,
                            Placement = 2,
                            PlayerId = 4,
                            MatchId = 3
                        }
                    }
                },
                new Player("5@Player5@20@4000@13000@12000@Silver@7"),
                new Player("6@Player6@20@6000@48000@23000@Master@2"),
                new Player("7@Player7@20@2000@30000@12000@Platinum@8"),
                new Player("8@Player8@20@7500@24000@11000@Platinum@3")
            }.AsQueryable());

            _logic = new PlayerLogic(_mockRepo.Object);
        }

        // Query tests
        [Test]
        public void PlayersWithGreaterKD_Test()
        {
            string rank = "Platinum";
            var expected = new List<PlayerRankInfo>()
            {
                new PlayerRankInfo
                {
                    UserName = "Player7",
                    Rank = rank,
                    KDRatio = 30000.0f / 12000.0f
                },
                new PlayerRankInfo
                {
                    UserName = "Player8",
                    Rank = rank,
                    KDRatio = 24000.0f / 11000.0f
                }
            };

            var actual = _logic.PlayersWithGreaterKD(rank);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void NumTimesTopThree_Test()
        {
            string username = "Player4";
            int expected = 2;

            var actual = _logic.NumTimesTopThree(username);

            Assert.AreEqual(expected, actual);
        }

        // Create tests
        [Test]
        public void Create_PlayerWithValidParams_Runs()
        {
            var player = new Player("45@DogWithTwoTails@36@1000@16000@15000@Diamond@6");

            _logic.Create(player);

            _mockRepo.Verify(r => r.Create(player), Times.Once);
        }

        [Test]
        public void Create_PlayerWithInvalidUserName_NeverRuns()
        {
            var player = new Player() { UserName = "" };
            try
            {
                _logic.Create(player);
            }
            catch
            {

            }

            _mockRepo.Verify(r => r.Create(player), Times.Never);
        }
        [Test]
        public void Create_PlayerWithInvalidRank_NeverRuns()
        {
            var player = new Player() { Rank = "Wood" };
            try
            {
                _logic.Create(player);
            }
            catch
            {

            }

            _mockRepo.Verify(r => r.Create(player), Times.Never);
        }
        [Test]
        public void Create_PlayerWithNegativeGames_NeverRuns()
        {
            var player = new Player() { NumGames = -1 };
            try
            {
                _logic.Create(player);
            }
            catch
            {

            }

            _mockRepo.Verify(r => r.Create(player), Times.Never);
        }

        private PlayerLogic _logic;
        private Mock<IRepository<Player>> _mockRepo;
    }
}
