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
    
    public partial class costo_tarifa
    {
        public int id { get; set; }
        public int tarifa_id { get; set; }
        public Nullable<int> orden { get; set; }
        public string nombre { get; set; }
        public decimal desde { get; set; }
        public decimal hasta { get; set; }
        public decimal valor { get; set; }
        public string es_base { get; set; }
        public string es_multa { get; set; }
        public Nullable<int> cci { get; set; }
        public Nullable<System.DateTime> ccd { get; set; }
        public Nullable<int> cwi { get; set; }
        public Nullable<System.DateTime> cwd { get; set; }
    
        public virtual tarifa tarifa { get; set; }
    }
}
