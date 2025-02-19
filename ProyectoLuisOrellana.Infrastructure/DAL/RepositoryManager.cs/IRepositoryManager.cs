using DatabaseFirst.Models;
using ProyectoLuisOrellana.Infrastructure.Repository.Base;
using ProyectoLuisOrellana.Infrastructure.Repository.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Infrastructure.DAL.RepositoryManager
{
    public interface IRepositoryManager
    {
       // IErrorLogRepository ErrorLog { get; }

        IRepositoryBase<ErrorLog> ErrorLog { get; }
        IRepositoryBase<User> User { get; }
        IRepositoryBase<UserToken> UserToken { get; }
        IIntentoRepository Intento { get; }
    }
}
