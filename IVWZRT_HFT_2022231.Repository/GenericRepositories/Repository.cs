using System.Linq;

namespace IVWZRT_HFT_2022231.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public Repository(ApexDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> ReadAll()
        {
            return _context.Set<T>();
        }
        public void Create(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            _context.Set<T>().Remove(Read(id));
            _context.SaveChanges();
        }

        public abstract T Read(int id);
        public abstract void Update(T item);
        
        protected ApexDbContext _context;
    }
}
