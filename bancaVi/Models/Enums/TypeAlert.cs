using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bancaVi.Models.Enums
{
    public class TypeAlert
    {
        public const string AlertExito = "success";

        public const string AlertError = "danger";

        public const string AlertPeligro = "warning";

        public const string AlertInfo = "info";

        public static string[] Alertas
        {
            get { return new[] { AlertExito, AlertError, AlertPeligro, AlertInfo }; }
        }
    }
}