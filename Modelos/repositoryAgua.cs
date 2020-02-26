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
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var usuario =
                    from c in contexto.usuario
                    where c.usuario1 == user && c.clave == clave
                    select c;

                return usuario.ToList();
            }
        }

        //Tabla empresa
        public List<empresa> obtenerEmpresas()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var empresa =
                    from e in contexto.empresa
                    select e;

                return empresa.ToList();
            }
        }

        public bool ModificarEmpresa(empresa nuevaEmpresa)
        {
            using (var contexto = new ipwebec_hydrosEntities())
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
                updateEmpresa.facturacion = nuevaEmpresa.facturacion;
                updateEmpresa.cwi = nuevaEmpresa.cwi;
                updateEmpresa.cwd = nuevaEmpresa.cwd;
                updateEmpresa.cci = nuevaEmpresa.cci;
                updateEmpresa.ccd = nuevaEmpresa.ccd;

                int affected = contexto.SaveChanges();
                return (affected == 1);
            }
        }

        //Tabla tarifa
        public List<tarifa> OtenerTarifas()
        {
            using (var contexto = new ipwebec_hydrosEntities())
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
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var clientes =
                    from c in contexto.cliente
                    select c;

                return clientes.ToList();
            }
        }

        public bool ActualizarCliente(cliente nuevoCliente)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                cliente updateCliente = contexto.cliente.First(c => c.id == nuevoCliente.id);

                updateCliente.nombre = nuevoCliente.nombre;
                updateCliente.medidor = nuevoCliente.medidor;
                updateCliente.numero_identificacion = nuevoCliente.numero_identificacion;
                updateCliente.sexo = nuevoCliente.sexo;
                updateCliente.telefono_celular = nuevoCliente.telefono_celular;
                updateCliente.telefono_fijo = nuevoCliente.telefono_fijo;
                updateCliente.apellido = nuevoCliente.apellido;
                updateCliente.ccd = nuevoCliente.ccd;
                updateCliente.cci = nuevoCliente.cci;
                updateCliente.cwd = nuevoCliente.cwd;
                updateCliente.cwi = nuevoCliente.cwi;
                updateCliente.direccion = nuevoCliente.direccion;
                updateCliente.email = nuevoCliente.email;
                updateCliente.estado = nuevoCliente.estado;
                updateCliente.id = nuevoCliente.id;
                updateCliente.tipo_identificacion = nuevoCliente.tipo_identificacion;
                updateCliente.venta = nuevoCliente.venta;

                int affected = contexto.SaveChanges();
                return (affected == 1);
            }
        }

        public bool InsertarCliente(cliente nuevoCliente)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.cliente.Add(nuevoCliente);

                int affected = contexto.SaveChanges();
                return (affected == 1);
            }
        }

        //tabla medidores
        public List<ConsultaMedidores> ObtenerMedidioresPorId(int numCli)
        {

            using (var contexto = new ipwebec_hydrosEntities())
            {

                var usuario =
                    from m in contexto.medidor
                    join b in contexto.barrio
                    on m.barrio_id equals b.id
                    join t in contexto.tarifa
                    on m.tarifa_id equals t.id
                    where m.cliente_id == numCli
                    select new ConsultaMedidores
                    {
                        id = m.id,
                        nombreM = m.nombreM,
                        nombreB = b.nombreB,
                        NombreT = t.nombreT,
                        serie = m.serie,
                        lectura_inicial = m.lectura_inicial,
                        estado = m.estado
                    };


                return usuario.ToList();
            }
        }

    }

    public partial class ConsultaMedidores
    {
        public int id { get; set; }
        public string nombreM { get; set; }
        public string nombreB { get; set; }
        public string NombreT { get; set; }
        public string serie { get; set; }
        public string lectura_inicial { get; set; }
        public string estado { get; set; }
    }
}
