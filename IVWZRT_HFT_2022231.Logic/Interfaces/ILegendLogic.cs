using System.Linq;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Logic
{
    public interface ILegendLogic
    {
        // CRUD
        IQueryable<Legend> ReadAll();
        Legend Read(int id);
        void Create(Legend item);
        void Update(Legend item);
        void Delete(int id);
    }
}
