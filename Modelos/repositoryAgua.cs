using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.DataSet;

namespace Modelos
{
    public class RepositoryAgua
    {
        //Tabla usuario
        public List<usuario> hacerLogin(string user, string clave)
        {
            using(var contexto = new juntas_aguaEntities())
            {
                var usuario =
                    from c in contexto.usuario
                    where c.nombre == user && c.clave == clave
                    select c;

                return usuario.ToList();
            }
        }

        //Tabla empresa
        public List<empresa> obtenerEmpresas()
        {
            using (var contexto = new juntas_aguaEntities())
            {
                var empresa =
                    from e in contexto.empresa
                    select e; 

                return empresa.ToList();
            }
        }

        //Tabla tarifa
        public List<tarifa>OtenerTarifas()
        {
            using (var contexto = new juntas_aguaEntities())
            {
                var tarifas =
                    from t in contexto.tarifa
                    select t;

                return tarifas.ToList();
            }
        }

        //Tabla cliente
        public List<cliente> OtenerClientes()
        {
            using (var contexto = new juntas_aguaEntities())
            {
                var clientes =
                    from c in contexto.cliente
                    select c;

                return clientes.ToList();
            }
        }
    }
}
