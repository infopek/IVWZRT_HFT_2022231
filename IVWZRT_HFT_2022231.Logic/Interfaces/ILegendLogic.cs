﻿using IVWZRT_HFT_2022231.Repository;
using IVWZRT_HFT_2022231.Models;
using System.Linq;
using System.Collections.Generic;

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

        // NON-CRUD
        IEnumerable<Legend> MostUsedLegendByRank();
        IEnumerable<int> NumGamesByDefensiveLegend();
    }
}