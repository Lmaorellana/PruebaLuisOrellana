using DatabaseFirst.Models;
using ProyectoLuisOrellana.Aplication.Utils;
using ProyectoLuisOrellana.Infrastructure.DAL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Aplication.General
{
    public class IntentoService : IIntentoService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IErrorLogService errorLog;
        private ResponseModel ResponseModel;

        public IntentoService(IRepositoryManager repositoryManager, IErrorLogService errorLog)
        {
            this.repositoryManager = repositoryManager;
            this.errorLog = errorLog;
            ResponseModel = new ResponseModel();
        }



        public async Task<ResponseModel> IntentoSave(Intento intento)
        {
            try
            {
                var cantidadIntentos = this.repositoryManager.Intento.List(p => p.DeportistaId == intento.DeportistaId && p.TipoIntento == intento.TipoIntento).Result.Count();

                if (cantidadIntentos == 3)
                {
                    this.ResponseModel.AddMessageError("Solo Puede Registrar 3 intentos por Deportista.");
                }
                if (ResponseModel.IsValid)
                {

                    cantidadIntentos++;
                    intento.NumeroIntento = cantidadIntentos;
                    await this.repositoryManager.Intento.Insert(intento);

                    this.ResponseModel.AddDefaultSuccess();
                }
            }
            catch (Exception ex)
            {
                this.errorLog.SaveErrorLog(ex);
                this.ResponseModel.AddDefaultError();
            }
            return ResponseModel;
        }

        public async Task<List<Dictionary<string, object>>> ObtenerRanking(int pageNumber, int pageSize)
        {
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            try
            {
                data = await this.repositoryManager.Intento.ObtenerRankingAsync(pageNumber, pageSize);
            }
            catch (Exception ex)
            {

                this.errorLog.SaveErrorLog(ex);
            }
            return data;
        }

        public async Task<List<Domain.Common.Intento>> GetIntentosAll()
        {
            List<Domain.Common.Intento> data = new List<Domain.Common.Intento>();
            try
            {
                var responsedata = await this.repositoryManager.Intento.List(new List<string> { "Deportista" });

                foreach (var intento in responsedata)
                {
                    data.Add(new Domain.Common.Intento
                    {
                        Nombre = intento.Deportista.Nombre,
                        Pais = intento.Deportista.Pais,
                        NumeroIntento = intento.NumeroIntento,
                        Peso = intento.Peso,
                        TipoIntento = intento.TipoIntento,
                    });
                }
            }
            catch (Exception ex)
            {

                this.errorLog.SaveErrorLog(ex);
            }
            return data;
        }


    }
}
