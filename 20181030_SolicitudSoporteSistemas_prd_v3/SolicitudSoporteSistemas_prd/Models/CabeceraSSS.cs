using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class CabeceraSSS
    {
        [Display(Name = "Id")]
        public int SolicitudId { get; set; }

        [Display(Name = "Tipo Solicitud")]
        public string TipoSolicitudNombre { get; set; }

        [Display(Name = "Tipo Solicitud Id")]
        public int TipoSolicitudId { get; set; }

        [Display(Name = "Empresa")]
        public string EmpresaNombre { get; set; }
        public int? EmpresaId { get; set; }

        [Display(Name = "Ubicación")]
        public string UbicacionNombre { get; set; }
        public int UbicacionId { get; set; }

        public string Proyecto { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "¿Es incidente?")]
        public bool Incidente { get; set; }

        //[Required(ErrorMessage = "Campo Obligatorio")]
        //[Display(Name = "Responsable")]
        public int? AsignadoAId { get; set; }
        [Display(Name = "Técnico (email)")]
        public string AsignadoAEmail { get; set; }
        [Display(Name = "Técnico")]
        public string AsignadoANombre { get; set; }

        public int? SolicitanteId { get; set; }
        [Display(Name = "Solicitante (email)")]
        public string SolicitanteEmail { get; set; }
        [Display(Name = "Solicitante (nombre)")]
        public string SolicitanteNombre { get; set; }


        [Display(Name = "Creador (Id Red)")]
        public string SolicitanteIdRed { get; set; }

        [Display(Name = "Categoría")]
        public string CategoriaNombre { get; set; }
        public int? CategoriaId { get; set; }

        [Display(Name = "Sub-categoría")]
        public string SubCategoriaNombre { get; set; }
        public int? SubCategoriaId { get; set; }

        [Display(Name = "Horas")]
        [Range(0, 50)]
        public decimal? HorasTotal { get; set; }

        public TimeSpan? Intervalo { get; set; }

        [Display(Name = "Duración")]
        public string Duracion { get; set; }

        [Display(Name = "Descripción")]
        [AllowHtml]
        public string Descripcion { get; set; }

        [Display(Name = "Estado")]
        public string EstadoNombre { get; set; }
        public int EstadoId { get; set; }
        public string EstadoTipo { get; set; }

        public string Area { get; set; }

        public string Usuario { get; set; }

        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:dd\\/MM\\/yyyy HH\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime? FechaCreacion { get; set; }

        [Display(Name = "Fecha Solicitada")]
        [DisplayFormat(DataFormatString = "{0:dd\\/MM\\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaSolicitada { get; set; }

        [Display(Name = "Fecha Comprometida")]
        [DisplayFormat(DataFormatString = "{0:dd\\/MM\\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaComprometida { get; set; }

        [Display(Name = "Fecha Resolución")]
        [DisplayFormat(DataFormatString = "{0:dd\\/MM\\/yyyy HH\\:mm}", ApplyFormatInEditMode = true)]
        public DateTime? FechaResolucion { get; set; }

        [Display(Name = "Sociedad")]
        public string SociedadNombre { get; set; }
        [Display(Name = "Sociedad (Rut")]
        public string SociedadRut { get; set; }

        [Display(Name = "Centro de Costo")]
        public string CentroCostoNombre { get; set; }
        [Display(Name = "Centro Costo/Proyecto")]
        public string CentroCostoCodigo { get; set; }

        public string Creacion { get; set; }

        public string Semaforo { get; set; }

        public string mensaje_error { get; set; }

        [Range(0, 10000000)]
        [Display(Name = "Referencia")]
        public int? Referencia { get; set; }

    }
}