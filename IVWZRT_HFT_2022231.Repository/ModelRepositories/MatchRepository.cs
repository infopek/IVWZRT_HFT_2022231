using System.Linq;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Repository
{
    public class MatchRepository : Repository<Match>, IRepository<Match>
    {
        public MatchRepository(ApexDbContext context)
            : base(context) { }

        public override Match Read(int id)
        {
            return _context.Matches.FirstOrDefault(p => p.MatchId == id);
        }
        public override void Update(Match item)
        {
            var old = Read(item.MatchId);
            foreach (var prop in old.GetType().GetProperties())
                prop.SetValue(old, prop.GetValue(item));

            _context.SaveChanges();
        }
    }
}