﻿using System.Text.Json.Serialization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVWZRT_HFT_2022231.Models
{
    public class EndGameStat
    {
        public EndGameStat() { }
        public EndGameStat(string line)
        {
            string[] split = line.Split('@');

            StatId = int.Parse(split[0]);

            Placement = int.Parse(split[1]);
            KillPoints = float.Parse(split[2]);
            Damage = int.Parse(split[3]);

            PlayerId = int.Parse(split[4]);
            MatchId = int.Parse(split[5]);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatId { get; set; }

        [Required]
        [Range(1, 30)]
        public int Placement { get; set; }
        public float KillPoints { get; set; }
        public int Damage { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        [ForeignKey("Match")]
        public int MatchId { get; set; }

        [NotMapped]
        public virtual Player Player { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Match Match { get; set; }
    }
}
