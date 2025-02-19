using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Aplication.General
{
    public interface ICurrentUserService
    {
        string LogonName { get; }
    }
}
