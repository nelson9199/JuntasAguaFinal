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
    
    public partial class detalle_asistencia
    {
        public int id { get; set; }
        public int asistencia_id { get; set; }
        public int medidor_id { get; set; }
        public Nullable<int> detalle_venta_id { get; set; }
        public string asiste { get; set; }
        public string estado { get; set; }
        public Nullable<int> cci { get; set; }
        public Nullable<System.DateTime> ccd { get; set; }
        public Nullable<int> cwi { get; set; }
        public Nullable<System.DateTime> cwd { get; set; }
    
        public virtual asistencia asistencia { get; set; }
        public virtual detalle_venta detalle_venta { get; set; }
        public virtual medidor medidor { get; set; }
    }
}
