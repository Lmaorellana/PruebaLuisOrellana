using Microsoft.EntityFrameworkCore;
using SuperNova.Erp.Base.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Infrastructure.Repository.Base
{
    public class RepositoryBase<TContext, TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase where TContext : DbContext
    {
        protected TContext Context;
        public RepositoryBase(TContext dbContext)
        {
            Context = dbContext;
        }
        protected virtual string DefaultSchema
        {
            get
            {
                return "dbo";
            }
        }
        public DbParameter CreateParameter(DbCommand command, string name, object value)
        {
            var param = command.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            return param;
        }
        public async Task Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }
        public async Task<TEntity?> GetById(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();


            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }


        public async Task Insert(TEntity entity)
        {

            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();

        }
        public async Task Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> List(IEnumerable<string> includes = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();


            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includes = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().Where(predicate);


            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }
        public async Task ExecuteSerializableInsert(TEntity entity)
        {
            // AssignInsertAuditableData(entity);
            string infoXml = entity.ToXml();
            infoXml = infoXml.Replace("'", "''");
            string entityName = entity.GetType().Name;
            var sql = string.Format("EXEC {3}.sp{0}{1} '{2}'", entityName, "Insert", infoXml, DefaultSchema);
            await Context.Database.ExecuteSqlRawAsync(sql);
            // await Context.SaveChangesAsync();   
        }

    }
}
