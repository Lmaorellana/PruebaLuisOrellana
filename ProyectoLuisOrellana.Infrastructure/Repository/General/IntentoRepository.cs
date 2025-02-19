using DatabaseFirst.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoLuisOrellana.Infrastructure.DAL.DbConecction;
using ProyectoLuisOrellana.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Infrastructure.Repository.General
{
    public class IntentoRepository : RepositoryBase<DbContextPrueba, Intento>, IIntentoRepository
    {
        public IntentoRepository(DbContextPrueba dbContext) : base(dbContext)
        {
        }

        public async Task<List<Dictionary<string, object>>> ObtenerRankingAsync(int pageNumber, int pageSize)
        {
            var result = new List<Dictionary<string, object>>();

            using (var connection = Context.Database.GetDbConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.ObtenerRanking";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddRange(new[]
                    {
                CreateParameter(command, "@pageNumber", pageNumber),
                CreateParameter(command, "@pageSize", pageSize),

                    });
                    using (var reader = await command.ExecuteReaderAsync())
                    {

                        while (await reader.ReadAsync())
                        {
                            var deportista = new Dictionary<string, object>();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                deportista[reader.GetName(i)] = reader[i] is DBNull ? null : reader[i];
                            }

                            result.Add(deportista);
                        }


                    }
                }
            }

            return result;
        }

    }
}
