namespace bancaVi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cliente")]
    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            cuenta = new HashSet<cuenta>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "Cedula")]
        public string id_cliente { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Nombre")]
        public string nombre_cliente { get; set; }

        [Display(Name = "Telefono fijo")]    
        public int telefono_cliente { get; set; }

        [Required]
        [StringLength(30)]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string correo_cliente { get; set; }

        [Required]
        [StringLength(8)]
        [Display(Name = "Contraseña")]
        public string contraseña_cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cuenta> cuenta { get; set; }
    }
}
