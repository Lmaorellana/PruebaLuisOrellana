using DatabaseFirst.Models;
using ProyectoLuisOrellana.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Infrastructure.Repository.General
{
    public interface IIntentoRepository : IRepositoryBase<Intento>
    {
        Task<List<Dictionary<string, object>>> ObtenerRankingAsync(int pageNumber, int pageSize);
    }
}
