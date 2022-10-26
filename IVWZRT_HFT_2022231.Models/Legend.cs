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
            Role = split[2];

            Skin = split[3];
            NumRevives = int.Parse(split[4]);

            Passive = split[5];
            Tactical = split[6];
            Ultimate = split[7];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LegendId { get; set; }

        public string Name { get; set; }
        public string Role { get; set; }

        public string Skin { get; set; }
        public int NumRevives { get; set; }

        public string Passive { get; set; }
        public string Tactical { get; set; }
        public string Ultimate { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
