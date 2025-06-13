using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.ViewModels;

namespace WebApplication.Models
{
    public partial class Usuario
    {
        public RolUsuario RolUsuario {
            get { return (RolUsuario)this.Rol; }
            set { this.Rol = (int)value; }
        }
    }
}