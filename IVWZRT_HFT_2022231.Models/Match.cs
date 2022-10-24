using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IVWZRT_HFT_2022231.Models
{
    public class Match
    {
        public Match() { }
        public Match(string line)
        {
            string[] split = line.Split('@');

            MatchId = int.Parse(split[0]);

            ChampionId = int.Parse(split[1]);

            Length = int.Parse(split[2]);

            GameMode = split[3];
            Map = split[4];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }

        public int ChampionId { get; set; }

        // In minutes
        [Range(1, 30)]
        public float Length { get; set; }

        public string GameMode { get; set; }
        public string Map { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual Champion Champion { get; set; }
    }
}
