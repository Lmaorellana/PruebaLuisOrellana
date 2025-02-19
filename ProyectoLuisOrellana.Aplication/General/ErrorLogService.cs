using DatabaseFirst.Models;
using ProyectoLuisOrellana.Domain.Common;
using ProyectoLuisOrellana.Infrastructure.DAL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Aplication.General
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly ICurrentUserService currentUser;

        public ErrorLogService(IRepositoryManager repositoryManager, ICurrentUserService currentUser)
        {
            this.repositoryManager = repositoryManager;
            this.currentUser = currentUser;
        }
        public void SaveErrorLog(Exception exception)
        {
            SaveErrorLog(new ErrorLogModel
            {
                ApplicationSource = "ProyectoPrueba",
                Exception = exception,
                LogonName = this.currentUser.LogonName,
                Type = MessageType.Error              
            }).Wait();
        }


        private async Task SaveErrorLog(ErrorLogModel model)
        {
            
            try
            {
                string message = GetExceptionMessage(model.Exception);

                if (!string.IsNullOrWhiteSpace(message) && !string.IsNullOrWhiteSpace(model.Message))
                {
                    message = message + ": " + model.Message;
                }

                string additionalInfo = model.AdditionalInformation;
                if (string.IsNullOrEmpty(message))
                    message = model.Message;
                else
                    additionalInfo = model.Exception.StackTrace;
                if (string.IsNullOrEmpty(model.ApplicationSource))
                    model.ApplicationSource = "ProyectoPrueba";
                if (!string.IsNullOrEmpty(message))
                {

                    await repositoryManager.ErrorLog.ExecuteSerializableInsert(new ErrorLog
                    {
                        Date = DateTime.Now,
                        AppSource = model.ApplicationSource,
                        Type = (short)model.Type,
                        Message = message,
                        AdditionalInformation = additionalInfo,
                        LogonName = model.LogonName,
                        
                    });


                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public static string GetExceptionMessage(Exception ex)
        {
            string value = string.Empty;
            if (ex != null)
            {
                value = string.Format("{0} ({1})", ex.Message, ex.GetType().FullName);
                if (ex.InnerException != null) value += string.Format("\r\n{0}", GetExceptionMessage(ex.InnerException));
            }
            return value;
        }
    }
}
