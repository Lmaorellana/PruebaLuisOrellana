using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Aplication.General
{
    public interface IErrorLogService
    {
        void SaveErrorLog(Exception exception);
    }
}
