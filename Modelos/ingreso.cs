//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Modelos
{
    using System;
    using System.Collections.Generic;
    
    public partial class ingreso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ingreso()
        {
            this.detalle_ingreso = new HashSet<detalle_ingreso>();
        }
    
        public int id { get; set; }
        public int ubicacion_id { get; set; }
        public int proveedor_id { get; set; }
        public int tipo_comprobante_id { get; set; }
        public string numero_comprobante { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<decimal> iva { get; set; }
        public Nullable<decimal> total { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_ingreso> detalle_ingreso { get; set; }
        public virtual tipo_comprobante tipo_comprobante { get; set; }
        public virtual ubicacion ubicacion { get; set; }
        public virtual proveedor proveedor { get; set; }
    }
}
