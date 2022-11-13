using System.Linq;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Repository
{
    public class PlayerRepository : Repository<Player>, IRepository<Player>
    {
        public PlayerRepository(ApexDbContext context)
            : base(context) { }

        public override Player Read(int id)
        {
            return _context.Players.FirstOrDefault(p => p.PlayerId == id);
        }
        public override void Update(Player item)
        {
            var old = Read(item.PlayerId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }

            _context.SaveChanges();
        }
    }
}
