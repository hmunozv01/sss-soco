using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class LogSSS
    {
        [Required]
        [Display(Name = "Id")]
        public int LogId { get; set; }

        public int LogSolicitudId { get; set; }
        public string LogSolicitudTipoEstadoIdNombre { get; set; }
        public string LogSolicitudDescripción { get; set; }
        public string LogSolicitudTipo { get; set; }
        
        [Display(Name = "Enviar Email")]
        public bool LogEmail { get; set; }

        //[Display(Name = "Acción")]
        //public string LogAccion { get; set; }

        [Display(Name = "Acción Id")]
        public int? LogAccionId { get; set; }

        [Display(Name = "Acción")]
        public string LogAccionNombre { get; set; }

        [Display(Name = "TipoId")]
        public int? LogTipoId { get; set; }

        [Display(Name = "Tipo")]
        public string LogTipoIdNombre { get; set; }

        [Display(Name = "Usuario")]
        public string LogUsuarioNombre { get; set; }
        
        public string LogUsuarioEmail { get; set; }

        [Display(Name = "Creador (Id Red)")]
        public string LogUsuarioIdRed { get; set; }

        [Display(Name = "Usuario Id")]
        public int? LogUsuarioId { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd\\/MM\\/yyyy HH\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime? LogFecha { get; set; }

        [Display(Name = "Descripción")]
        [AllowHtml]
        public string LogDescripcion { get; set; }

        [Display(Name = "Horas")]
        [Range(0, 50)]
        public decimal? LogHora { get; set; }
        
        [Display(Name = "Cerrar Solicitud")]
        public bool LogCerrarSolicitud { get; set; }

        //[Display(Name = "Minutos")]
        //[Range(0, 60)]
        public Int32? LogMinutos { get; set; }

        public IList<Minutos> LogListaMinutos { get; set; }

        public int? LogPersonalId { get; set; }

        [Display(Name = "Técnico")]
        public string LogTecnicoEmail { get; set; }
        public string LogTecnicoNombre { get; set; }

        public string LogResultadoEnvioEmail { get; set; }

        public string LogSolicitanteNombre { get; set; }
        public string LogSolicitanteEmail { get; set; }
        public string LogSolicitanteUbicacion { get; set; }
        public string LogSolicitanteTelefono { get; set; }
    }
}