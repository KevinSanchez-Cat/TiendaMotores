//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TiendaMotores.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            this.Compra = new HashSet<Compra>();
        }
    
        public int id_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string apellido_p { get; set; }
        public string apellido_m { get; set; }
        public Nullable<int> id_direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public Nullable<int> id_tarjeta { get; set; }
        public string nombre_usuario { get; set; }
        public string contrasenia { get; set; }
        public Nullable<System.DateTime> fecha_nacimiento { get; set; }
    
        public virtual Direccion Direccion { get; set; }
        public virtual Tarjeta Tarjeta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compra> Compra { get; set; }
    }
}
