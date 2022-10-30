using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVWZRT_HFT_2022231.Models
{
    public class Match
    {
        public Match() { }
        public Match(string line)
        {
            string[] split = line.Split('@');

            MatchId = int.Parse(split[0]);

            Length = float.Parse(split[1]);

            GameMode = split[2];
            Map = split[3];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }

        // In minutes
        [Required]
        [Range(1, 30)]
        public float Length { get; set; }

        public string GameMode { get; set; }
        public string Map { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<EndGameStat> Stats { get; set; }
    }
}
