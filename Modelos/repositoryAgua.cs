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

        public bool ModificarEmpresa(empresa nuevaEmpresa)
        {
            using (var contexto = new juntas_aguaEntities())
            {
                empresa updateEmpresa = contexto.empresa.First(p => p.id == nuevaEmpresa.id);

                updateEmpresa.nombre = nuevaEmpresa.nombre;
                updateEmpresa.logo = nuevaEmpresa.logo;
                updateEmpresa.recaudador = nuevaEmpresa.recaudador;
                updateEmpresa.ruc = nuevaEmpresa.ruc;
                updateEmpresa.telefono_fijo = nuevaEmpresa.telefono_fijo;
                updateEmpresa.telefono_movil = nuevaEmpresa.telefono_movil;
                updateEmpresa.email = nuevaEmpresa.email;
                updateEmpresa.direccion = nuevaEmpresa.direccion;
                updateEmpresa.cwi = nuevaEmpresa.cwi;
                updateEmpresa.cwd = nuevaEmpresa.cwd;
                updateEmpresa.cci = nuevaEmpresa.cci;
                updateEmpresa.ccd = nuevaEmpresa.ccd;

                int affected = contexto.SaveChanges();
                return (affected == 1);
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
