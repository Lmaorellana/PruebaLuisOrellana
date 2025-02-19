using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Domain.Common
{
    public enum MessageType
    {
        Information = 0,
        Error = 1,
        Warning = 2,
        Confirmation = 3,
        Success = 4
    }
    public class ErrorLogModel
    {
        public string ApplicationSource { get; set; }
        public Exception Exception { get; set; }
        public int OrganizationId { get; set; }
        public MessageType Type { get; set; }

        public string Message { get; set; }

        public string AdditionalInformation { get; set; }

        public string LogonName { get; set; }
    }
}
