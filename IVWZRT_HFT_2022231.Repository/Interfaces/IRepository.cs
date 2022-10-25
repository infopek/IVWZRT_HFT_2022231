using System.Linq;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
