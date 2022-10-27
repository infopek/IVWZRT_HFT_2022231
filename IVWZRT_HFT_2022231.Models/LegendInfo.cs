namespace IVWZRT_HFT_2022231.Models
{
    public enum Role
    {
        Offensive,
        Defensive,
        Support,
        Recon
    }

    public class LegendInfo
    {
        public LegendInfo() { }
        public LegendInfo(string tactical, string ultimate, int role)
        {
            Tactical = tactical;
            Ultimate = ultimate;
            Role = (Role)role;
        }

        public string Tactical { get; private set; }
        public string Ultimate { get; private set; }
        public Role Role { get; private set; }
    }
}
