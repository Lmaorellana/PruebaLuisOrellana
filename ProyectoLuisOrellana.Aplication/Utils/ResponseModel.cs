using ProyectoLuisOrellana.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Aplication.Utils
{
    public class ResponseModel
    {
        public bool IsValid { get; set; } = true;
        public string Mensaje { get; set; }
        public Object Data { get; set; }
        public MessageType TipoMensaje { get; set; }



        public void AddMessageError(string Mensaje)
        {
            this.IsValid = false;
            this.Mensaje = Mensaje;
            this.Data = string.Empty;
            this.TipoMensaje = MessageType.Error;
        }

        public void AddMessageInformation(string Mensaje)
        {
            this.IsValid = false;
            this.Mensaje = Mensaje;
            this.Data = string.Empty;
            this.TipoMensaje = MessageType.Information;
        }
        public void AddMessageWarning(string Mensaje)
        {
            this.IsValid = false;
            this.Mensaje = Mensaje;
            this.Data = string.Empty;
            this.TipoMensaje = MessageType.Warning;
        }
        public void AddDefaultError()
        {
            this.IsValid = false;
            this.Mensaje = "Ocurrió un error durante la transacción.";
            this.Data = string.Empty;
            this.TipoMensaje = MessageType.Error;
        }

        // Método para establecer una respuesta exitosa con datos opcionales
        public void AddDefaultSuccess(object data = null)
        {
            this.IsValid = true;
            this.Mensaje = "Operación realizada con éxito.";
            this.Data = data;
            this.TipoMensaje = MessageType.Success;
        }
    }
}
