using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bancaVi.ViewModels.Access
{
    public class AccessViewModel : MError
    {
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}