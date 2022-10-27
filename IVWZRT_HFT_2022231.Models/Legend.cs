using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IVWZRT_HFT_2022231.Models
{
    public class Legend
    {
        /// <summary>
        /// Initializes the dictionary containing all 22 legends' info.
        /// </summary>
        static Legend()
        {
            IDictionary<string, LegendInfo> dict = new Dictionary<string, LegendInfo>()
            {
                { "Wraith", new LegendInfo("Into the Void", "Dimensinal Rift", 0) },
                { "Horizon", new LegendInfo("Gravity Lift", "Black Hole", 0) },
                { "Fuse", new LegendInfo("Knuckle Cluster", "Dimensinal Rift", 0) },
                { "Ash", new LegendInfo("Arc Snare", "Phase Breach", 0) },
                { "Mirage", new LegendInfo("Psyche Out", "Life of the Party", 0) },
                { "Octane", new LegendInfo("Stim", "Launch Pad", 0) },
                { "Revenant", new LegendInfo("Silence", "Death Totem", 0) },
                { "Bangalore", new LegendInfo("Smoke Launcher", "Rolling Thunder", 0) },
                { "Mad Maggie", new LegendInfo("Riot Drill", "Wrecking Ball", 0) },

                { "Rampart", new LegendInfo("Amped Cover", "Mobile Minigun \"Sheila\"", 1) },
                { "Gibraltar", new LegendInfo("Dome of Protection", "Defensive Bombardment", 1) },
                { "Newcastle", new LegendInfo("Mobile Shield", "Castle Wall", 1) },
                { "Caustic", new LegendInfo("Nox Gas Trap", "Nox Gas Grenade", 1) },
                { "Wattson", new LegendInfo("Perimeter Security", "Interception Pylon", 1) },

                { "Loba", new LegendInfo("Burglar's Best Friend", "Black Market Boutique", 2) },
                { "Lifeline", new LegendInfo("D.O.C Heal Drone", "Care Package", 2) },

                { "Vantage", new LegendInfo("Echo Relocation", "Sniper's Mark", 3) },
                { "Bloodhound", new LegendInfo("Eye of the Allfather", "Beast of the Hunt", 3) },
                { "Seer", new LegendInfo("Focus of Attention", "Exhibit", 3) },
                { "Pathfinder", new LegendInfo("Grappling Hook", "Zipline Gun", 3) },
                { "Valkyrie", new LegendInfo("Missile Swarm", "Skyward Dive", 3) },
                { "Crypto", new LegendInfo("Surveillance Drone", "Drone Emp", 3) },
            };
            _legends = new ReadOnlyDictionary<string, LegendInfo>(dict);
        }

        public Legend() { }
        public Legend(string line)
        {
            string[] split = line.Split('@');

            LegendId = int.Parse(split[0]);

            Info = _legends[split[1]];

            Skin = split[3];
            NumRevives = int.Parse(split[4]);

            Passive = split[5];
            Tactical = split[6];
            Ultimate = split[7];
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LegendId { get; set; }

        public LegendInfo Info { get; private set; }

        public string Skin { get; set; }
        public int NumRevives { get; set; }

        public string Passive { get; set; }
        public string Tactical { get; set; }
        public string Ultimate { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        private static readonly IReadOnlyDictionary<string, LegendInfo> _legends;
    }
}
