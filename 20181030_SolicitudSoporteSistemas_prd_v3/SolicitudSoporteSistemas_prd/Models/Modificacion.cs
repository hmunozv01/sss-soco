using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class Modificacion
    {
        public int SolicitudId { get; set; }

        public string entidad { get; set; }
        public string campoId { get; set; }
        public string campoNombre { get; set; }

        public int codigoOriginalId { get; set; }
        public string codigoOriginalNombre { get; set; }

        public int codigoNuevoId { get; set; }
        public string codigoNuevoNombre { get; set; }

        public int? tipoModificacionId { get; set; }

        public string comentario { get; set; }

        public string error { get; set; }

    }
}