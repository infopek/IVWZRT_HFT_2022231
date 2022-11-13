using System.Linq;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Repository
{
    public class StatRepository : Repository<EndGameStat>, IRepository<EndGameStat>
    {
        public StatRepository(ApexDbContext context)
            : base(context) { }

        public override EndGameStat Read(int id)
        {
            return _context.Stats.FirstOrDefault(p => p.StatId == id);
        }
        public override void Update(EndGameStat item)
        {
            var old = Read(item.StatId);
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
