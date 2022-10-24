using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IVWZRT_HFT_2022231.Models
{
    public class Champion
    {
        public Champion() { }
        public Champion(string line)
        {
            string[] split = line.Split('@');

            ChampionId = int.Parse(split[0]);

            PlayerId = int.Parse(split[1]);
            MatchId = int.Parse(split[2]);

            Damage = int.Parse(split[3]);
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChampionId { get; set; }

        public int PlayerId { get; set; }
        public int MatchId { get; set; }

        [Range(0, 15000)]
        public int Damage { get; set; }

        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
    }
}
