using System.Collections.Generic;

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
            LegendId = int.Parse(split[1]);

            Age = int.Parse(split[2]);

            UserName = split[3];

            NumGames = int.Parse(split[4]);
            Kills = int.Parse(split[5]);
            Deaths = int.Parse(split[6]);

            Rank = split[7];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        public int LegendId { get; set; }

        [Range(16, 90)]
        public int Age { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        public int NumGames { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }

        public string Rank { get; set; }

        public virtual Legend Main { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<Champion> ChampionTitles { get; set; }
    }
}
