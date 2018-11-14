using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class DB_Solicitudes_Pendientes_Atrasadas_por_Responsable
    {
        public string NickName { get; set; }
        public string NombreCompleto { get; set; }
        public int Pendientes { get; set; }
        public int Atrasadas { get; set; }
    }
}