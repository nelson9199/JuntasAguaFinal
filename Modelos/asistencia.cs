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
    
    public partial class asistencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public asistencia()
        {
            this.detalle_asistencia = new HashSet<detalle_asistencia>();
        }
    
        public int id { get; set; }
        public int actividad_id { get; set; }
        public int barrio_id { get; set; }
        public int articulo_id { get; set; }
        public string multa { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> cci { get; set; }
        public Nullable<System.DateTime> ccd { get; set; }
        public Nullable<int> cwi { get; set; }
        public Nullable<System.DateTime> cwd { get; set; }
    
        public virtual actividad actividad { get; set; }
        public virtual articulo articulo { get; set; }
        public virtual barrio barrio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_asistencia> detalle_asistencia { get; set; }
    }
}
