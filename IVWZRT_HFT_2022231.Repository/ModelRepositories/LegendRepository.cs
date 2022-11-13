using System.Linq;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Repository
{
    public class LegendRepository : Repository<Legend>, IRepository<Legend>
    {
        public LegendRepository(ApexDbContext context)
            : base(context) { }

        public override Legend Read(int id)
        {
            return _context.Legends.FirstOrDefault(p => p.LegendId == id);
        }
        public override void Update(Legend item)
        {
            var old = Read(item.LegendId);
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
