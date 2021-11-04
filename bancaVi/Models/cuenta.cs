namespace bancaVi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cuenta")]
    public partial class cuenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cuenta()
        {
            pago = new HashSet<pago>();
            transferencia = new HashSet<transferencia>();
            transferencia1 = new HashSet<transferencia>();
        }

        [Key]
        public int id_cuenta_cliente { get; set; }

        [Display(Name = "Sado actual")]
        public int saldo_cuenta { get; set; }

        [Required]
        [StringLength(10)]
        public string id_cliente { get; set; }

        public virtual cliente cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pago> pago { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transferencia> transferencia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transferencia> transferencia1 { get; set; }
    }
}
