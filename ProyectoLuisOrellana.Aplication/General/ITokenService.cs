using DatabaseFirst.Models;
using ProyectoLuisOrellana.Aplication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Aplication.General
{
    public interface ITokenService
    {
        Task<ResponseModel> GenerateTokenAsync(User user);
        Task<bool> IsTokenValidAsync(string token);
    }
}
