namespace bancaVi.Models
{
    using bancaVi.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("transferencia")]
    public partial class transferencia
    {
        [Key]
        public int id_transferencia { get; set; }

        [Display(Name = "Cuenta Origen")]
        public int id_cuenta_cliente { get; set; }

        [Display(Name = "Monto de la transaccion")]
        public int monto_d { get; set; }

        [Display(Name = "Cuenta Destino")]
        public int id_cuenta_user { get; set; }

        [Display(Name = "Cuenta Origen")]
        public virtual cuenta cuenta { get; set; }

        [Display(Name = "Cuenta Destino")]
        public virtual cuenta cuenta1 { get; set; }
    }
}
