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
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");

        //Tabla usuario
        public bool ComprobarLogin(string username, string clave)
        {

            using (var contexto = new ipwebec_hydrosEntities())
            {
                var usuario =
                    from c in contexto.usuario
                    where c.usuario1 == username
                    select c;

                var usuariosList = usuario.ToList();

                if (usuariosList.Count > 0)
                {
                    var salt = usuariosList[0].salt;

                    // re-generate the salted and hashed password 
                    var saltedhashedPassword = Protector.SaltAndHashPassword(
                      clave, salt);

                    var usuarioVerificar =
                        from c in contexto.usuario
                        where c.usuario1 == username && c.clave == saltedhashedPassword
                        select c;

                    var usuarioVerificarList = usuarioVerificar.ToList();

                    if (usuarioVerificarList.Count > 0)
                    {
                        if (!(usuarioVerificarList[0].usuario1 == username))
                        {
                            return false;
                        }

                        return (saltedhashedPassword == usuarioVerificarList[0].clave);
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }

            }
        }

        public List<usuario> hacerLogin(string user, string clave)
        {

            using (var contexto = new ipwebec_hydrosEntities())
            {
                var usuario =
                    from c in contexto.usuario
                    where c.usuario1 == user
                    select c;

                var usuariosList = usuario.ToList();

                var salt = usuariosList[0].salt;

                // re-generate the salted and hashed password 
                var saltedhashedPassword = Protector.SaltAndHashPassword(
                  clave, salt);

                var usuario2 =
                from c in contexto.usuario
                where c.usuario1 == user && c.clave == saltedhashedPassword
                select c;

                return usuario.ToList();

            }
        }

        public List<usuario> ObtenerUsuarios()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var usuario =
                    from c in contexto.usuario
                    select c;

                return usuario.ToList();
            }
        }

        public bool InsertUsuario(usuario nuevoUsuario)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                (string encryptedPassword, string salt) = Protector.ObtenerEncryptedPassword(nuevoUsuario.clave, nuevoUsuario.usuario1);

                nuevoUsuario.clave = encryptedPassword;
                nuevoUsuario.salt = salt;

                contexto.usuario.Add(nuevoUsuario);

                int affected = contexto.SaveChanges();
                return (affected == 1);
            }
        }

        public bool EliminarUsuarios(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<usuario> usuarios = contexto.usuario.Where(u => u.id == id);
                contexto.usuario.RemoveRange(usuarios);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }

        }

        public bool ModificarUsuarios(usuario nuevoUsuario)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {

                var usuarioUpdate = contexto.usuario.First(x => x.id == nuevoUsuario.id);

                usuarioUpdate.id = nuevoUsuario.id;
                usuarioUpdate.foto = nuevoUsuario.foto;
                usuarioUpdate.apellido = nuevoUsuario.apellido;
                usuarioUpdate.cci = nuevoUsuario.cci;
                usuarioUpdate.ccd = nuevoUsuario.ccd;
                usuarioUpdate.cwd = nuevoUsuario.cwd;
                usuarioUpdate.cwi = nuevoUsuario.cwi;
                usuarioUpdate.direccion = nuevoUsuario.direccion;
                usuarioUpdate.email = nuevoUsuario.email;
                usuarioUpdate.estado = nuevoUsuario.estado;
                usuarioUpdate.es_superadmin = nuevoUsuario.es_superadmin;
                usuarioUpdate.fecha_nacimiento = nuevoUsuario.fecha_nacimiento;
                usuarioUpdate.salt = nuevoUsuario.salt;
                usuarioUpdate.nombre = nuevoUsuario.nombre;
                usuarioUpdate.numero_identificacion = nuevoUsuario.numero_identificacion;
                usuarioUpdate.perfil = nuevoUsuario.perfil;
                usuarioUpdate.telefono_fijo = nuevoUsuario.telefono_fijo;
                usuarioUpdate.telefono_movil = nuevoUsuario.telefono_movil;
                usuarioUpdate.tipo_identificacion = nuevoUsuario.tipo_identificacion;
                usuarioUpdate.usuario1 = nuevoUsuario.usuario1;

                var usuario =
                   from c in contexto.usuario
                   where c.usuario1 == nuevoUsuario.usuario1
                   select c;

                var usuariosList = usuario.ToList();

                var salt = usuariosList[0].salt;

                // re-generate the salted and hashed password 
                var saltedhashedPassword = Protector.SaltAndHashPassword(
                  nuevoUsuario.clave, salt);

                usuarioUpdate.clave = saltedhashedPassword;

                int affected = contexto.SaveChanges();
                return (affected == 1);

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
                //Eliminar el campo facturacion
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
        public List<tarifa> ObtenerTarifas()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var tarifas =
                    from t in contexto.tarifa
                    select t;

                return tarifas.ToList();
            }
        }

        public bool InsertarTarifa(tarifa nuevaTarifa)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.tarifa.Add(nuevaTarifa);

                int affected = contexto.SaveChanges();
                return (affected == 1);
            }
        }

        public bool ModificarTarifa(tarifa nuevaTarifa)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var tarifaUpdate = contexto.tarifa.First(t => t.id == nuevaTarifa.id);

                tarifaUpdate.nombreT = nuevaTarifa.nombreT;
                tarifaUpdate.acumulativa = nuevaTarifa.acumulativa;
                tarifaUpdate.descripcion = nuevaTarifa.descripcion;
                tarifaUpdate.orden = nuevaTarifa.orden;

                int affected = contexto.SaveChanges();
                return (affected == 1);
            }
        }

        public bool EliminarTarifa(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<tarifa> tarifaUpdate = contexto.tarifa.Where(t => t.id == id);

                contexto.tarifa.RemoveRange(tarifaUpdate);

                int affected = contexto.SaveChanges();
                return (affected == 1);
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
                try
                {
                    int affected = contexto.SaveChanges();
                    return (affected == 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

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

        public bool EliminarCliente(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<cliente> clientes = contexto.cliente.Where(c => c.id == id);
                contexto.cliente.RemoveRange(clientes);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        public List<cliente> BuscarClientePorNombre(string nombre)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var clientes = contexto.cliente.Where(c => c.nombre.StartsWith(nombre));

                return clientes.ToList();
            }
        }
        public List<cliente> BuscarClientePorCedula(string cedula)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var clientes = contexto.cliente.Where(c => c.numero_identificacion.StartsWith(cedula));

                return clientes.ToList();
            }
        }
        public List<cliente> BuscarClientePorDireccion(string direccion)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var clientes = contexto.cliente.Where(c => c.direccion.StartsWith(direccion));

                return clientes.ToList();
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

        public bool EliminarMedidor(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<medidor> medidor = contexto.medidor.Where(m => m.cliente_id == id);
                contexto.medidor.RemoveRange(medidor);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        //Tabla Marca
        public List<marca> ObtenerMarcas()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var marcas =
                    from m in contexto.marca
                    select m;

                return marcas.ToList();
            }
        }

        public bool InsertarMarca(marca nuevaMarca)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.marca.Add(nuevaMarca);

                int affected = contexto.SaveChanges();
                return (affected == 1);
            }
        }
        public bool ModificarMarca(marca nuevaMarca)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var marcaUpdate = contexto.marca.First(m => m.id == nuevaMarca.id);

                marcaUpdate.nombre = nuevaMarca.nombre;

                int affected = contexto.SaveChanges();
                return (affected == 1);

            }
        }

        public bool EliminarMarca(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<marca> marca = contexto.marca.Where(m => m.id == id);
                contexto.marca.RemoveRange(marca);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        //tabla Barrio
        public List<barrio> ObtenerBarrios()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var barrios = contexto.barrio.Select(b => b);

                return barrios.ToList();
            }
        }

        public bool InsertarBarrio(barrio barrio)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.barrio.Add(barrio);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        public bool ModificarBarrio(barrio nuevoBarrio)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var updatedBarrio = contexto.barrio.First(b => b.id == nuevoBarrio.id);

                updatedBarrio.nombreB = nuevoBarrio.nombreB;

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        public bool EliminarBarrio(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<barrio> barrio = contexto.barrio.Where(b => b.id == id);
                contexto.barrio.RemoveRange(barrio);

                int affected = contexto.SaveChanges();

                return (affected == 1);
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
