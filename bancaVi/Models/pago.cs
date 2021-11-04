namespace bancaVi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pago")]
    public partial class pago
    {
        [Key]
        public int id_pago { get; set; }

        public int id_servicio { get; set; }

        public int id_cuenta_cliente { get; set; }

        public virtual cuenta cuenta { get; set; }

        public virtual servicio servicio { get; set; }
    }
}
