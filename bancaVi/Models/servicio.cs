namespace bancaVi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("servicio")]
    public partial class servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public servicio()
        {
            pago = new HashSet<pago>();
        }

        [Key]
        [Display(Name = "Codigo de servicio")]
        public int id_servicio { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Servicio")]
        public string nombre_servicio { get; set; }

        [Display(Name = "Costo Servicio")]
        public int valor_servicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pago> pago { get; set; }
    }
}
