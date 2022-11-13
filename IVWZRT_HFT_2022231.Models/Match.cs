using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        public override bool Equals(object obj)
        {
            Match other = obj as Match;
            if (other == null)
                return false;
            else
                return MatchId == other.MatchId
                    && Length == other.Length
                    && GameMode == other.GameMode
                    && Map == other.Map;
        }
        public override int GetHashCode()
        {
            return MatchId.GetHashCode() ^ Length.GetHashCode() ^ GameMode.GetHashCode() ^ Map.GetHashCode();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MatchId { get; set; }

        // In minutes
        [Required]
        [Range(1, 30)]
        public float Length { get; set; }
        [Required]
        public string GameMode { get; set; }
        [Required]
        public string Map { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Player> Players { get; set; }
        [NotMapped]
        public virtual ICollection<EndGameStat> Stats { get; set; }
    }
}
