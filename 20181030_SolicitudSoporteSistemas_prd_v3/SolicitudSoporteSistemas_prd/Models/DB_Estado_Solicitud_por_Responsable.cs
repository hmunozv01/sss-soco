using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class DB_Estado_Solicitud_por_Responsable
    {
        public string NickName { get; set; }
        public int Resuelta { get; set; }
        public int ResueltaConConfirmacion { get; set; }
        public int NoCorrespondeATareaDeSistemas { get; set; }
        public int CierreHistorico { get; set; }
        public int EsperandoRespuestaDeUsuario { get; set; }
        public int Escalada { get; set; }
        public int Asignada { get; set; }
        public int Agendada { get; set; }
        public int EnProceso { get; set; }
        public int Nueva { get; set; }
    }
}