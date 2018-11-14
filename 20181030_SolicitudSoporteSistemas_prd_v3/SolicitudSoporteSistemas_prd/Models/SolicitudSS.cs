using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class SolicitudSS
    {
        public CabeceraSSS Cabecera { get; set;}
        public List<LogSSS> Logs { get; set; }
        public List<DocumentoSSS> Documentos { get; set; }
        //public Solicitud Cabecera { get; set; }
    }
}