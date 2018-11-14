using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SolicitudSoporteSistemas_prd.Models
{
    public class RolUsuario
    {
        [Display(Name = "UserId")]          // 5: tecnico. 6: Supervisor
        public int UserId { get; set; }

        [Display(Name = "UserIdName")]
        public string UserIdName { get; set; }

        [Display(Name = "UserNickName")]
        public string UserNickName { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "UserEmail")]
        public string UserEmail { get; set; }
    }
}
