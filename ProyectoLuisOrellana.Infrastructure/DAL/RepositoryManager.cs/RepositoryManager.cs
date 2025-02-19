using DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using ProyectoLuisOrellana.Infrastructure.DAL.DbConecction;
using ProyectoLuisOrellana.Infrastructure.Repository.Base;
using ProyectoLuisOrellana.Infrastructure.Repository.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Infrastructure.DAL.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private DbContextPrueba dbContextPrueba;
        private IRepositoryBase<ErrorLog> errorLogRepository;
        private IRepositoryBase<User> userRepository;
        private IRepositoryBase<UserToken> userTokenRepository;
        private IIntentoRepository intentoRepository;


        public RepositoryManager(DbContextPrueba dbContextPrueba)
        {
            this.dbContextPrueba = dbContextPrueba;
        }
        public IRepositoryBase<ErrorLog> ErrorLog
        {
            get
            {
                if (errorLogRepository == null)
                {
                    errorLogRepository = new RepositoryBase<DbContextPrueba, ErrorLog>(dbContextPrueba);
                }
                return errorLogRepository;
            }
        }

        public IRepositoryBase<User> User
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new RepositoryBase<DbContextPrueba, User>(dbContextPrueba);
                }
                return userRepository;
            }
        }

        public IRepositoryBase<UserToken> UserToken
        {
            get
            {
                if (userTokenRepository == null)
                {
                    userTokenRepository = new RepositoryBase<DbContextPrueba, UserToken>(dbContextPrueba);
                }
                return userTokenRepository;
            }
        }

        public IIntentoRepository Intento
        {
            get
            {
                if (intentoRepository == null)
                {
                    intentoRepository = new IntentoRepository(dbContextPrueba);
                }
                return intentoRepository;
            }
        }

        public async Task Save()
        {
            await dbContextPrueba.SaveChangesAsync();
        }
    }
}
