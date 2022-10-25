using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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

        public int PlayerId { get; set; }
        public int MatchId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
    }
}
