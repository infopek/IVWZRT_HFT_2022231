using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVWZRT_HFT_2022231.Models
{
    public class Legend
    {
        public Legend() { }
        public Legend(string line)
        {
            string[] split = line.Split('@');

            LegendId = int.Parse(split[0]);

            Name = split[1];
            Skin = split[2];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LegendId { get; set; }

        [Required]
        [StringLength(20)]
        public string Skin { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
