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
    
    public partial class barrio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public barrio()
        {
            this.asistencia = new HashSet<asistencia>();
            this.medidor = new HashSet<medidor>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public Nullable<int> cci { get; set; }
        public string ccd { get; set; }
        public Nullable<int> cwi { get; set; }
        public string cwd { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<asistencia> asistencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<medidor> medidor { get; set; }
    }
}