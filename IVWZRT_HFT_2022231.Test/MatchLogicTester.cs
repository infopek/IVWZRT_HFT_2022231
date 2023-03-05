using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;

using IVWZRT_HFT_2022231.Logic;
using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;

// Resolve ambiguity between Moq's Match and Models' Match
using Match = IVWZRT_HFT_2022231.Models.Match;

namespace IVWZRT_HFT_2022231.Test
{
    [TestFixture]
    public class MatchLogicTester
    {
        [SetUp]
        public void Init()
        {
            _mockRepo = new Mock<IRepository<Match>>();
            _mockRepo.Setup(r => r.ReadAll()).Returns(new List<Match>()
            {
                new Match
                {
                    MatchId = 1,
                    Length = 10.00f,
                    GameMode = "duos",
                    Map = "olympus",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 1,
                            UserName = "Player1",
                            Age = 20,
                            Rank = "platinum",
                            LegendId = 1,
                            Legend = new Legend
                            {
                                LegendId = 1,
                                Name = "Ash"
                            }
                        },
                        new Player
                        {
                            PlayerId = 2,
                            UserName = "Player2",
                            Age = 17,
                            Rank = "platinum",
                            LegendId = 2,
                            Legend = new Legend
                            {
                                LegendId = 2,
                                Name = "Horizon"
                            }
                        },
                        new Player
                        {
                            PlayerId = 3,
                            UserName = "Player3",
                            Age = 26,
                            Rank = "platinum",
                            LegendId = 3,
                            Legend = new Legend
                            {
                                LegendId = 3,
                                Name = "Gibraltar"
                            }
                        }
                    }
                },
                new Match
                {
                    MatchId = 2,
                    Length = 10.00f,
                    GameMode = "duos",
                    Map = "world's edge",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 4,
                            UserName = "Player4",
                            Age = 20,
                            Rank = "platinum",
                            LegendId = 4,
                            Legend = new Legend
                            {
                                LegendId = 4,
                                Name = "Rampart"
                            }
                        },
                        new Player
                        {
                            PlayerId = 5,
                            UserName = "Player5",
                            Age = 17,
                            Rank = "platinum",
                            LegendId = 5,
                            Legend = new Legend
                            {
                                LegendId = 5,
                                Name = "Caustic"
                            }
                        }
                    }
                },
                new Match
                {
                    MatchId = 3,
                    Length = 20.00f,
                    GameMode = "duos",
                    Map = "kings canyon",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 6,
                            UserName = "Player6",
                            Age = 20,
                            Rank = "platinum",
                            LegendId = 6,
                            Legend = new Legend
                            {
                                LegendId = 6,
                                Name = "Rampart"
                            }
                        },
                        new Player
                        {
                            PlayerId = 7,
                            UserName = "Player7",
                            Age = 17,
                            Rank = "platinum",
                            LegendId = 7,
                            Legend = new Legend
                            {
                                LegendId = 7,
                                Name = "Rampart"
                            }
                        },
                        new Player
                        {
                            PlayerId = 8,
                            UserName = "Player8",
                            Age = 26,
                            Rank = "platinum",
                            LegendId = 8,
                            Legend = new Legend
                            {
                                LegendId = 8,
                                Name = "Crypto"
                            }
                        }
                    }
                },
                new Match
                {
                    MatchId = 4,
                    Length = 30.00f,
                    GameMode = "duos",
                    Map = "storm point",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 9,
                            UserName = "Player9",
                            Age = 20,
                            Rank = "platinum",
                            LegendId = 9,
                            Legend = new Legend
                            {
                                LegendId = 9,
                                Name = "seer"
                            }
                        },
                        new Player
                        {
                            PlayerId = 10,
                            UserName = "Player10",
                            Age = 17,
                            Rank = "platinum",
                            LegendId = 10,
                            Legend = new Legend
                            {
                                LegendId = 10,
                                Name = "Bangalore"
                            }
                        },
                        new Player
                        {
                            PlayerId = 11,
                            UserName = "Player11",
                            Age = 26,
                            Rank = "platinum",
                            LegendId = 11,
                            Legend = new Legend
                            {
                                LegendId = 11,
                                Name = "Lifeline"
                            }
                        }
                    }
                },
                new Match
                {
                    MatchId = 5,
                    Length = 15.05f,
                    GameMode = "trios",
                    Map = "kings canyon",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 12,
                            UserName = "Player12",
                            Age = 20,
                            Rank = "platinum",
                            LegendId = 12,
                            Legend = new Legend
                            {
                                LegendId = 12,
                                Name = "Rampart"
                            }
                        },
                        new Player
                        {
                            PlayerId = 13,
                            UserName = "Player13",
                            Age = 20,
                            Rank = "platinum",
                            LegendId = 13,
                            Legend = new Legend
                            {
                                LegendId = 13,
                                Name = "Ash"
                            }
                        },
                        new Player
                        {
                            PlayerId = 14,
                            UserName = "Player14",
                            Age = 22,
                            Rank = "platinum",
                            LegendId = 14,
                            Legend = new Legend
                            {
                                LegendId = 14,
                                Name = "Vantage"
                            }
                        }
                    }
                },
                new Match
                {
                    MatchId = 6,
                    Length = 23.45f,
                    GameMode = "arenas",
                    Map = "olympus",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 15,
                            UserName = "Player15",
                            Age = 20,
                            Rank = "platinum",
                            LegendId = 15,
                            Legend = new Legend
                            {
                                LegendId = 15,
                                Name = "Bloodhound"
                            }
                        },
                        new Player
                        {
                            PlayerId = 16,
                            UserName = "Player16",
                            Age = 17,
                            Rank = "platinum",
                            LegendId = 16,
                            Legend = new Legend
                            {
                                LegendId = 16,
                                Name = "Bangalore"
                            }
                        },
                        new Player
                        {
                            PlayerId = 17,
                            UserName = "Player17",
                            Age = 26,
                            Rank = "platinum",
                            LegendId = 17,
                            Legend = new Legend
                            {
                                LegendId = 17,
                                Name = "Loba"
                            }
                        }
                    }
                },
                new Match
                {
                    MatchId = 7,
                    Length = 24.7f,
                    GameMode = "ranked leagues",
                    Map = "storm point",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 16,
                            UserName = "Player16",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 16,
                            Legend = new Legend
                            {
                                LegendId = 16,
                                Name = "Bloodhound"
                            }
                        },
                        new Player
                        {
                            PlayerId = 17,
                            UserName = "Player17",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 17,
                            Legend = new Legend
                            {
                                LegendId = 17,
                                Name = "Bangalore"
                            }
                        },
                        new Player
                        {
                            PlayerId = 18,
                            UserName = "Player18",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 18,
                            Legend = new Legend
                            {
                                LegendId = 18,
                                Name = "Loba"
                            }
                        }
                    }
                },
                new Match
                {
                    MatchId = 8,
                    Length = 21.87f,
                    GameMode = "ranked leagues",
                    Map = "storm point",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 19,
                            UserName = "Player19",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 19,
                            Legend = new Legend
                            {
                                LegendId = 19,
                                Name = "Bloodhound"
                            }
                        },
                        new Player
                        {
                            PlayerId = 20,
                            UserName = "Player20",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 20,
                            Legend = new Legend
                            {
                                LegendId = 20,
                                Name = "Bangalore"
                            }
                        },
                        new Player
                        {
                            PlayerId = 21,
                            UserName = "Player21",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 21,
                            Legend = new Legend
                            {
                                LegendId = 21,
                                Name = "Loba"
                            }
                        }
                    }
                },
                new Match
                {
                    MatchId = 9,
                    Length = 27.43f,
                    GameMode = "ranked leagues",
                    Map = "storm point",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 22,
                            UserName = "Player22",
                            Age = 20,
                            Rank = "gold",
                            LegendId = 22,
                            Legend = new Legend
                            {
                                LegendId = 22,
                                Name = "Bloodhound"
                            }
                        },
                        new Player
                        {
                            PlayerId = 23,
                            UserName = "Player23",
                            Age = 20,
                            Rank = "gold",
                            LegendId = 23,
                            Legend = new Legend
                            {
                                LegendId = 23,
                                Name = "Bangalore"
                            }
                        },
                        new Player
                        {
                            PlayerId = 24,
                            UserName = "Player24",
                            Age = 20,
                            Rank = "gold",
                            LegendId = 24,
                            Legend = new Legend
                            {
                                LegendId = 24,
                                Name = "Loba"
                            }
                        }
                    }
                }
            }.AsQueryable());

            _logic = new MatchLogic(_mockRepo.Object);
        }

        // Query tests
        [Test]
        public void AvgLengthOfGame_Test()
        {
            string gameMode = "duos";
            float expected = (10 + 10 + 20 + 30) / 4.0f;

            float actual = _logic.AvgLengthOfGame(gameMode);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void MapsWithMostRamparts_Test()
        {
            IEnumerable<string> expected = new List<string>() { "kings canyon" };

            IEnumerable<string> actual = _logic.MapsWithMostRamparts();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void LongestMatchesInDiamond_Test()
        {
            IEnumerable<Match> expected = new List<Match>()
            {
                new Match
                {
                   MatchId = 7,
                    Length = 24.7f,
                    GameMode = "ranked leagues",
                    Map = "storm point",
                    Players = new List<Player>
                    {
                        new Player
                        {
                            PlayerId = 16,
                            UserName = "Player16",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 16,
                            Legend = new Legend
                            {
                                LegendId = 16,
                                Name = "Bloodhound"
                            }
                        },
                        new Player
                        {
                            PlayerId = 17,
                            UserName = "Player17",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 17,
                            Legend = new Legend
                            {
                                LegendId = 17,
                                Name = "Bangalore"
                            }
                        },
                        new Player
                        {
                            PlayerId = 18,
                            UserName = "Player18",
                            Age = 20,
                            Rank = "diamond",
                            LegendId = 18,
                            Legend = new Legend
                            {
                                LegendId = 18,
                                Name = "Loba"
                            }
                        }
                    }
                }
            };

            IEnumerable<Match> actual = _logic.LongestMatchesInDiamond();

            Assert.AreEqual(expected, actual);
        }

        // Create tests
        [Test]
        public void Create_ArenasMatchWithValidParams_Runs()
        {
            var match = new Match("50@25.06@Ranked Arenas@Drop Off");

            _logic.Create(match);

            _mockRepo.Verify(r => r.Create(match), Times.Once);
        }
        [Test]
        public void Create_RegularMatchWithValidParams_Runs()
        {
            var match = new Match("50@25.06@Trios@Olympus");

            _logic.Create(match);

            _mockRepo.Verify(r => r.Create(match), Times.Once);
        }

        [Test]
        public void Create_MatchWithInvalidGameMode_NeverRuns()
        {
            var match = new Match("50@25.06@Solos@Kings Canyon");

            try
            {
                _logic.Create(match);
            }
            catch
            {

            }

            _mockRepo.Verify(r => r.Create(match), Times.Never);
        }
        [Test]
        public void Create_ArenasMatchWithInvalidMap_NeverRuns()
        {
            var match = new Match("50@25.06@Arenas@Kings Canyon");

            try
            {
                _logic.Create(match);
            }
            catch
            {

            }

            _mockRepo.Verify(r => r.Create(match), Times.Never);
        }
        [Test]
        public void Create_RegularMatchWithInvalidMap_NeverRuns()
        {
            var match = new Match("50@25.06@Duos@Habitat 4");

            try
            {
                _logic.Create(match);
            }
            catch
            {

            }

            _mockRepo.Verify(r => r.Create(match), Times.Never);
        }

        private MatchLogic _logic;
        private Mock<IRepository<Match>> _mockRepo;
    }
}
