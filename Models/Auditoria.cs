using System;

namespace DatingApp.API.Models
{
    public class Auditoria
    {
        public string Id { get; set; }
        public DateTime FechaRevision { get; set; }
        public string RemoteIpAddress { get; set; }
        public string NombreTabla { get; set; }
        public string UserName { get; set; }
        public string Accion { get; set; }
        public string DataOriginal { get; set; }
        public string DataNueva { get; set; }
        public string ColumnasMod { get; set; }
    }
}