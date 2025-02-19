using DatabaseFirst.Models;
using ProyectoLuisOrellana.Aplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Aplication.General
{
    public interface IIntentoService
    {
        Task<ResponseModel> IntentoSave(Intento intento);
        Task<List<Dictionary<string, object>>> ObtenerRanking(int pageNumber, int pageSize);
        Task<List<ProyectoLuisOrellana.Domain.Common.Intento>> GetIntentosAll();
    }
}
