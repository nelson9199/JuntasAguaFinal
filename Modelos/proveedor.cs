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
    
    public partial class proveedor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proveedor()
        {
            this.ingreso = new HashSet<ingreso>();
        }
    
        public int id { get; set; }
        public string ruc { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono_fijo { get; set; }
        public string telefono_celular { get; set; }
        public string email { get; set; }
        public string estado { get; set; }
        public Nullable<int> cci { get; set; }
        public Nullable<System.DateTime> ccd { get; set; }
        public Nullable<int> cwi { get; set; }
        public Nullable<System.DateTime> cwd { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ingreso> ingreso { get; set; }
    }
}
