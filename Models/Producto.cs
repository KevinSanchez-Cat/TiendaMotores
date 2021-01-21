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
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.Inventario = new HashSet<Inventario>();
            this.Detalle_compra = new HashSet<Detalle_compra>();
            this.Producto1 = new HashSet<Producto>();
            this.ProductoTienda = new HashSet<ProductoTienda>();
        }
    
        public int Id_producto { get; set; }
        public string nombre_producto { get; set; }
        public string descripcion_producto { get; set; }
        public decimal precio { get; set; }
        public decimal coste { get; set; }
        public string imagen_producto { get; set; }
        public int id_categoria { get; set; }
        public Nullable<System.DateTime> ultima_actualizacion { get; set; }
        public int id_marca { get; set; }
        public Nullable<int> id_producto_relacionado { get; set; }
        public int clave_unica { get; set; }
        public Nullable<int> id_galeria { get; set; }
        public string email_usuario { get; set; }
        public string potencia { get; set; }
        public string Tipo { get; set; }
        public Nullable<decimal> ancho { get; set; }
        public Nullable<decimal> alto { get; set; }
        public Nullable<decimal> profundidad { get; set; }
        public Nullable<decimal> peso { get; set; }
        public string polos { get; set; }
        public string rpm { get; set; }
        public Nullable<decimal> voltaje { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        public virtual Galeria Galeria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual marca marca { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalle_compra> Detalle_compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Producto> Producto1 { get; set; }
        public virtual Producto Producto2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductoTienda> ProductoTienda { get; set; }
    }
}
