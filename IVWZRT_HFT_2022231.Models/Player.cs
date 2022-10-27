﻿using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVWZRT_HFT_2022231.Models
{
    public class Player
    {
        public Player() { }
        public Player(string line)
        {
            string[] split = line.Split('@');

            PlayerId = int.Parse(split[0]);

            UserName = split[1];

            Age = int.Parse(split[2]);

            NumGames = int.Parse(split[3]);
            TotalKills = int.Parse(split[4]);
            TotalDeaths = int.Parse(split[5]);

            Rank = split[6];

            LegendId = int.Parse(split[7]);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Range(16, 90)]
        public int Age { get; set; }

        public int NumGames { get; set; }
        public int TotalKills { get; set; }
        public int TotalDeaths { get; set; }

        public string Rank { get; set; }

        [ForeignKey("Legend")]
        public int LegendId { get; set; }

        public virtual Legend Main { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<EndGameStat> Stats { get; set; }
    }
}
