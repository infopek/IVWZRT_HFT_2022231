using System;
using System.Collections.Generic;

using ConsoleTools;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Client
{
    public class Program
    {
        // CRUD
        static void Create(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter the player's name: ");
                string name = Console.ReadLine();
                Console.Write("Enter the player's age (between 16 and 90): ");
                int age = int.Parse(Console.ReadLine());

                _rest.Post(new Player() { UserName = name, Age = age }, "player");
            }
            else if (entity == "Legend")
            {
                Console.Write("Enter the legend's name: ");
                string name = Console.ReadLine();
                Console.Write("Enter the legend's skin: ");
                string skin = Console.ReadLine();

                _rest.Post(new Legend() { Name = name, Skin = skin }, "legend");
            }
            else if (entity == "Match")
            {
                Console.Write("Enter length (in minutes): ");
                float length = float.Parse(Console.ReadLine());
                Console.Write("Enter gamemode: ");
                string gameMode = Console.ReadLine();
                Console.Write("Enter map: ");
                string map = Console.ReadLine();

                _rest.Post(new Match() { Length = length, GameMode = gameMode, Map = map }, "match");
            }
            else if (entity == "Stat")
            {
                Console.Write("Enter the end game stat's placement: ");
                int placement = int.Parse(Console.ReadLine());

                _rest.Post(new EndGameStat() { Placement = placement }, "stat");
            }
        }
        static void List(string entity)
        {
            if (entity == "Player")
            {
                List<Player> players = _rest.Get<Player>("player");
                foreach (var p in players)
                {
                    Console.WriteLine(p.PlayerId + ": " + p.UserName + ", " + p.Age);
                }
            }
            else if (entity == "Legend")
            {
                List<Legend> legends = _rest.Get<Legend>("legend");
                foreach (var l in legends)
                {
                    Console.WriteLine(l.LegendId + ": " + l.Name + ", " + l.Skin);
                }
            }
            else if (entity == "Match")
            {
                List<Match> matches = _rest.Get<Match>("match");
                foreach (var m in matches)
                {
                    Console.WriteLine(m.MatchId + ": " + m.Length + ", " + m.GameMode + ", " + m.Map);
                }
            }
            else if (entity == "Stat")
            {
                List<EndGameStat> stats = _rest.Get<EndGameStat>("stat");
                foreach (var s in stats)
                {
                    Console.WriteLine(s.StatId + ": " + s.Placement + " (placement)");
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter player's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Player player = _rest.Get<Player>(id, "player");

                Console.Write($"New name [old: {player.UserName}]: ");
                string name = Console.ReadLine();
                player.UserName = name;

                Console.Write($"New age (between 16 and 90) [old: {player.Age}]: ");
                int age = int.Parse(Console.ReadLine());
                player.Age = age;

                _rest.Put(player, "player");
            }
            else if (entity == "Legend")
            {
                Console.Write("Enter the legend's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Legend legend = _rest.Get<Legend>(id, "legend");

                Console.Write($"New name [old: {legend.Name}]: ");
                string name = Console.ReadLine();
                legend.Name = name;

                Console.Write($"New skin [old: {legend.Skin}]: ");
                string skin = Console.ReadLine();
                legend.Skin = skin;

                _rest.Put(legend, "legend");
            }
            else if (entity == "Match")
            {
                Console.Write("Enter the match' id to update: ");
                int id = int.Parse(Console.ReadLine());
                Match match = _rest.Get<Match>(id, "match");

                Console.Write($"New length [old: {match.Length}]: ");
                float length = float.Parse(Console.ReadLine());
                match.Length = length;

                Console.Write($"New gamemode [old: {match.GameMode}]: ");
                string gameMode = Console.ReadLine();
                match.GameMode = gameMode;

                Console.Write($"New map [old: {match.Map}]: ");
                string map = Console.ReadLine();
                match.Map = map;

                _rest.Put(match, "match");
            }
            else if (entity == "Stat")
            {
                Console.Write("Enter the stat's id to update: ");
                int id = int.Parse(Console.ReadLine());
                EndGameStat stat = _rest.Get<EndGameStat>(id, "stat");

                Console.Write($"New placement [old: {stat.Placement}]: ");
                int placement = int.Parse(Console.ReadLine());
                stat.Placement = placement;

                _rest.Put(stat, "stat");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Player")
            {
                Console.Write("Enter Player's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                _rest.Delete(id, "player");
            }
            else if (entity == "Legend")
            {
                Console.Write("Enter the legend's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                _rest.Delete(id, "legend");
            }
            else if (entity == "Match")
            {
                Console.Write("Enter the match' id to delete: ");
                int id = int.Parse(Console.ReadLine());
                _rest.Delete(id, "match");
            }
            else if (entity == "Stat")
            {
                Console.Write("Enter the stat's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                _rest.Delete(id, "stat");
            }
        }

        // Player queries
        static void GetPlayersWithGreaterKD(string entity)
        {
            Console.Write("Enter rank: ");
            string rank = Console.ReadLine();

            IEnumerable<PlayerRankInfo> playerInfos = _rest.PlayersKD(rank, entity);
            foreach (var info in playerInfos)
            {
                Console.WriteLine($"{info.UserName}: KD Ratio -- {info.KDRatio}");
            }
            Console.ReadLine();
        }
        static void GetNumTimesTopThree(string entity)
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            int times = _rest.TopThree(username, entity);
            Console.WriteLine($"{username} has been in the top three {times} times");
            Console.ReadLine();
        }

        // Match queries
        static void GetAvgLengthOfGame(string entity)
        {
            Console.Write("Enter gamemode: ");
            string gameMode = Console.ReadLine();

            float avg = _rest.AvgLength(gameMode, entity);
            Console.WriteLine($"Average length of {gameMode}: {avg}");
            Console.ReadLine();

        }
        static void GetMapsWithMostRamparts(string entity)
        {
            IEnumerable<string> maps = _rest.MostRamparts(entity);
            foreach (string map in maps)
            {
                Console.WriteLine(map);
            }
            Console.ReadLine();
        }
        static void GetLongestMatchesInDiamond(string entity)
        {
            IEnumerable<Match> matches = _rest.LongestMatches(entity);
            foreach (var match in matches)
            {
                Console.WriteLine($"{match.MatchId}: map -- {match.Map}, length -- {match.Length}");
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            _rest = new RestService("http://localhost:64082/", "player");

            var querySubMenu = new ConsoleMenu(args, level: 1)
                .Add("Players with KD Greater than 2.0", () => GetPlayersWithGreaterKD("Query"))
                .Add("# of Times Player Top 3", () => GetNumTimesTopThree("Query"))
                .Add("Average Length of Game", () => GetAvgLengthOfGame("Query"))
                .Add("Maps with Most Ramparts", () => GetMapsWithMostRamparts("Query"))
                .Add("Longest Match in Diamond", () => GetLongestMatchesInDiamond("Query"))
                .Add("Exit", ConsoleMenu.Close);

            var playerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Player"))
                .Add("Create", () => Create("Player"))
                .Add("Delete", () => Delete("Player"))
                .Add("Update", () => Update("Player"))           
                .Add("Exit", ConsoleMenu.Close);

            var legendSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Legend"))
                .Add("Create", () => Create("Legend"))
                .Add("Delete", () => Delete("Legend"))
                .Add("Update", () => Update("Legend"))
                .Add("Exit", ConsoleMenu.Close);

            var matchSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Match"))
                .Add("Create", () => Create("Match"))
                .Add("Delete", () => Delete("Match"))
                .Add("Update", () => Update("Match"))
                
                .Add("Exit", ConsoleMenu.Close);

            var statSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Stat"))
                .Add("Create", () => Create("Stat"))
                .Add("Delete", () => Delete("Stat"))
                .Add("Update", () => Update("Stat"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Players", () => playerSubMenu.Show())
                .Add("Legends", () => legendSubMenu.Show())
                .Add("Matches", () => matchSubMenu.Show())
                .Add("Stats", () => statSubMenu.Show())
                .Add("Queries", () => querySubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }

        private static RestService _rest;
    }
}
