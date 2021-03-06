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
    
    public partial class medidor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public medidor()
        {
            this.detalle_asistencia = new HashSet<detalle_asistencia>();
            this.lectura = new HashSet<lectura>();
        }
    
        public int id { get; set; }
        public int cliente_id { get; set; }
        public int barrio_id { get; set; }
        public int tarifa_id { get; set; }
        public int marca_id { get; set; }
        public string nombreM { get; set; }
        public string serie { get; set; }
        public string lectura_inicial { get; set; }
        public string estado { get; set; }
        public Nullable<System.DateTime> fecha_ingreso { get; set; }
        public Nullable<System.DateTime> fecha_retiro { get; set; }
        public Nullable<int> cci { get; set; }
        public Nullable<System.DateTime> ccd { get; set; }
        public Nullable<int> cwi { get; set; }
        public Nullable<System.DateTime> cwd { get; set; }
    
        public virtual barrio barrio { get; set; }
        public virtual cliente cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detalle_asistencia> detalle_asistencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lectura> lectura { get; set; }
        public virtual marca marca { get; set; }
        public virtual tarifa tarifa { get; set; }
    }
}
