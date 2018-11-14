using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class Email
    {
        [Display(Name = "Usuario:")]
        public string nombrecompleto { get; set; }
        public string cargo { get; set; }

        public string solicitantenombre { get; set; }
        public string solicitantecargo { get; set; }
        public string solicitanteubicacion { get; set; }
        public string solicitantetelefono { get; set; }
        public string solicitanteemail { get; set; }

        [Display(Name = "De:")]
        public string emailfuente { get; set; }
        [Display(Name = "Para:")]
        public string emaildestinatario { get; set; }

        [Display(Name = "CC:")]
        public string emailcc { get; set; }

        [Display(Name = "Asunto:")]
        public string asunto { get; set; }

        [Display(Name = "Mensaje:")]
        public string mensaje { get; set; }

        public string tecniconombre { get; set; }
        public string tecnicoemail { get; set; }

        public string solicitudtipo { get; set; }
        public string solicituddescripcion { get; set; }
        public string solicitudestado { get; set; }
        public int solicitudid { get; set; }

        public string error { get; set; }
    }
}