using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class DocumentoSSS
    {
        [Display(Name = "Id")]
        public int DocumentoId { get; set; }

        public int DocumentoSolicitudId { get; set; }

        [Display(Name = "Usuario")]
        public string DocumentoUsuario { get; set; }
        [Display(Name = "Tipo")]
        public string DocumentoTipo { get; set; }
        [Display(Name = "Fecha")]
        public DateTime DocumentoFecha { get; set; }
        [Display(Name = "Descripción")]
        public string DocumentoDescripcion { get; set; }

        [Display(Name = "Nombre")]
        public string DocumentoNombre { get; set; }
    }
}