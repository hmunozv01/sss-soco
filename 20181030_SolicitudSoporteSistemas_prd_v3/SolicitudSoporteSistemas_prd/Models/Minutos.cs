using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class Minutos
    {
        [Display(Name = "Minutos")]
        public string MinutosNombre { get; set; }

        public int MinutosCantidad { get; set; }
    }
}