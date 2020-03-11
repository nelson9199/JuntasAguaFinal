using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                (string encryptedPassword, string salt) =
                    Protector.ObtenerEncryptedPassword(nuevoUsuario.clave, nuevoUsuario.usuario1);

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

        public async Task<tarifa> ObtenerTarifaPorNombre(string nombre)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var tarifa = await contexto.tarifa.FirstOrDefaultAsync(t => t.nombreT == nombre);

                return tarifa;
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
                    join mar in contexto.marca
                        on m.marca_id equals mar.id
                    where m.cliente_id == numCli
                    select new ConsultaMedidores
                    {
                        id = m.id,
                        nombreM = m.nombreM,
                        nombreB = b.nombreB,
                        NombreT = t.nombreT,
                        NombreMarca = mar.nombre,
                        serie = m.serie,
                        lectura_inicial = m.lectura_inicial,
                        estado = m.estado,
                        numB = m.barrio_id,
                        numM = m.marca_id,
                        numT = m.tarifa_id,
                        fecha_ingreso = m.fecha_ingreso,
                        fecha_retiro = m.fecha_retiro
                    };


                return usuario.ToList();
            }
        }

        public bool EliminarMedidor(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<medidor> medidor = contexto.medidor.Where(m => m.id == id);
                contexto.medidor.RemoveRange(medidor);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        public bool InsertarMedidor(medidor nuevoMedidor)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.medidor.Add(nuevoMedidor);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        public bool ActualizarMedidior(medidor nuevoMedidor)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var updatedMedidor = contexto.medidor.First(b => b.id == nuevoMedidor.id);

                updatedMedidor.barrio_id = nuevoMedidor.barrio_id;
                updatedMedidor.cliente_id = nuevoMedidor.cliente_id;
                updatedMedidor.estado = nuevoMedidor.estado;
                updatedMedidor.fecha_ingreso = nuevoMedidor.fecha_ingreso;
                updatedMedidor.fecha_retiro = nuevoMedidor.fecha_retiro;
                updatedMedidor.lectura_inicial = nuevoMedidor.lectura_inicial;
                updatedMedidor.marca_id = nuevoMedidor.marca_id;
                updatedMedidor.nombreM = nuevoMedidor.nombreM;
                updatedMedidor.serie = nuevoMedidor.serie;
                updatedMedidor.tarifa_id = nuevoMedidor.tarifa_id;

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

        public async Task<marca> ObtenerMarcaPorNombre(string nombre)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var marca = await contexto.marca.FirstOrDefaultAsync(x => x.nombre == nombre);

                return marca;
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

        public async Task<barrio> ObtenerBarrioPorNombre(string nombre)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var barrio = await contexto.barrio.FirstOrDefaultAsync(x => x.nombreB == nombre);

                return barrio;
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

        //tabla multa
        public async Task<List<multa_retraso>> ObtenerMultas()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var multas = await contexto.multa_retraso.Select(x => x).ToListAsync();

                return multas;
            }
        }

        public bool InsertarMultas(multa_retraso nuevaMulta)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.multa_retraso.Add(nuevaMulta);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        public bool ModificarMulta(multa_retraso nuevaMulta)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var updatedMulta = contexto.multa_retraso.First(b => b.id == nuevaMulta.id);

                updatedMulta.tiempo_espera = nuevaMulta.tiempo_espera;
                updatedMulta.porcentaje_pago = nuevaMulta.porcentaje_pago;

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        public bool EliminarMulta(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<multa_retraso> multa = contexto.multa_retraso.Where(b => b.id == id);
                contexto.multa_retraso.RemoveRange(multa);

                int affected = contexto.SaveChanges();

                return (affected == 1);
            }
        }

        //tabla grupo
        public async Task<List<grupo>> ObtenerGrupo()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var grupo = await contexto.grupo.Select(x => x).ToListAsync();

                return grupo;
            }
        }

        public async Task<grupo> ObtenerGrupoPorNombre(string nombre)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var grupo = await contexto.grupo.FirstAsync(x => x.nombre == nombre);

                return grupo;
            }
        }

        public async Task<bool> InsertarGrupo(grupo nuevoGrupo)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.grupo.Add(nuevoGrupo);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> ModificarGrupo(grupo nuevoGrupo)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var updatedGrupo = contexto.grupo.First(b => b.id == nuevoGrupo.id);

                updatedGrupo.codigo = nuevoGrupo.codigo;
                updatedGrupo.nombre = nuevoGrupo.nombre;

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> EliminarGrupo(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<grupo> grupo = contexto.grupo.Where(b => b.id == id);
                contexto.grupo.RemoveRange(grupo);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        //tabla impuesto
        public async Task<List<impuesto>> ObtenerImpuesto()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var impuesto = await contexto.impuesto.Select(x => x).ToListAsync();

                return impuesto;
            }
        }

        public async Task<impuesto> ObtenerImpuestoPorNombre(string nombre)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var impuesto = await contexto.impuesto.FirstAsync(x => x.nombre == nombre);

                return impuesto;
            }
        }

        public async Task<bool> InsertarImpuesto(impuesto nuevoImpuesto)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.impuesto.Add(nuevoImpuesto);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> ModificarImpuesto(impuesto nuevoImpuesto)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var updatedImpuesto = contexto.impuesto.First(b => b.id == nuevoImpuesto.id);

                updatedImpuesto.porcentaje = nuevoImpuesto.porcentaje;
                updatedImpuesto.nombre = nuevoImpuesto.nombre;

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> EliminarImpuesto(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<impuesto> impuesto = contexto.impuesto.Where(b => b.id == id);
                contexto.impuesto.RemoveRange(impuesto);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        //tabla unidad medida
        public async Task<List<unidad_medida>> ObtenerMedida()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var medida = await contexto.unidad_medida.Select(x => x).ToListAsync();

                return medida;
            }
        }

        public async Task<unidad_medida> ObtenerMedidaPorNombre(string nombre)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var medida = await contexto.unidad_medida.FirstAsync(x => x.nombre == nombre);

                return medida;
            }
        }

        public async Task<bool> InsertarMedida(unidad_medida nuevaMedida)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.unidad_medida.Add(nuevaMedida);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> ModificarMedida(unidad_medida nuevaMedida)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var updatedImpuesto = contexto.unidad_medida.First(b => b.id == nuevaMedida.id);

                updatedImpuesto.prefijo = nuevaMedida.prefijo;
                updatedImpuesto.nombre = nuevaMedida.nombre;

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> EliminarMedida(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<unidad_medida> medida = contexto.unidad_medida.Where(b => b.id == id);
                contexto.unidad_medida.RemoveRange(medida);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        //tabla articulo
        public async Task<bool> InsertarArticulo(articulo nuevoArticulo)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.articulo.Add(nuevoArticulo);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> ModificarArticulo(articulo nuevoArticulo)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var updatedArticulo = contexto.articulo.First(b => b.id == nuevoArticulo.id);

                updatedArticulo.grupo_id = nuevoArticulo.grupo_id;
                updatedArticulo.clase = nuevoArticulo.clase;
                updatedArticulo.control_stock = nuevoArticulo.control_stock;
                updatedArticulo.descripcion = nuevoArticulo.descripcion;
                updatedArticulo.fecha_creacion = nuevoArticulo.fecha_creacion;
                updatedArticulo.foto = nuevoArticulo.foto;
                updatedArticulo.impuesto_id = nuevoArticulo.impuesto_id;
                updatedArticulo.marca_id = nuevoArticulo.marca_id;
                updatedArticulo.nombre_completo = nuevoArticulo.nombre_completo;
                updatedArticulo.nombre_corto = nuevoArticulo.nombre_corto;
                updatedArticulo.nombre_foto = nuevoArticulo.nombre_foto;
                updatedArticulo.precio_compra = nuevoArticulo.precio_compra;
                updatedArticulo.precio_venta = nuevoArticulo.precio_venta;
                updatedArticulo.stock_actual = nuevoArticulo.stock_actual;
                updatedArticulo.stock_minimo = nuevoArticulo.stock_minimo;
                updatedArticulo.unidad_medida_id = nuevoArticulo.unidad_medida_id;


                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> EliminarArticulo(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<articulo> articulo = contexto.articulo.Where(b => b.id == id);
                contexto.articulo.RemoveRange(articulo);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<List<ConsultarArticulos>> ObtenerArticulo()
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var articulo =
                    // ReSharper disable once ComplexConditionExpression
                    from a in contexto.articulo
                    join g in contexto.grupo
                        on a.grupo_id equals g.id
                    join m in contexto.marca
                        on a.marca_id equals m.id
                    join u in contexto.unidad_medida
                        on a.unidad_medida_id equals u.id
                    join i in contexto.impuesto
                        on a.impuesto_id equals i.id
                    select new ConsultarArticulos
                    {
                        id = a.id,
                        grupo_id = g.id,
                        unidad_medida_id = u.id,
                        impuesto_id = i.id,
                        marca_id = m.id,
                        clase = a.clase,
                        nombre_completo = a.nombre_completo,
                        nombre_corto = a.nombre_corto,
                        descripcion = a.descripcion,
                        foto = a.foto,
                        nombre_foto = a.nombre_foto,
                        fecha_creacion = a.fecha_creacion,
                        control_stock = a.control_stock,
                        stock_minimo = a.stock_minimo,
                        stock_actual = a.stock_actual,
                        precio_compra = a.precio_compra,
                        precio_venta = a.precio_venta,
                        nombre_Grupo = g.nombre,
                        nombre_Medida = u.nombre,
                        nombre_marca = m.nombre,
                        nombre_impuesto = i.nombre + " " + i.porcentaje

                    };

                return await articulo.ToListAsync();
            }
        }

        //tabla Costos
        public async Task<List<costo_tarifa>> ObtenerCostos(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var costos = await contexto.costo_tarifa.Where(x => x.id == id).ToListAsync();

                return costos;
            }
        }

        public async Task<bool> InsertarCostoc(costo_tarifa costo)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                contexto.costo_tarifa.Add(costo);

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> ModificarCosto(costo_tarifa costo)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                var Ucosto = await contexto.costo_tarifa.FirstAsync(x => x.id == costo.id);

                Ucosto.nombre = costo.nombre;
                Ucosto.desde = costo.desde;
                Ucosto.es_base = costo.es_base;
                Ucosto.es_multa = costo.es_multa;
                Ucosto.hasta = costo.hasta;
                Ucosto.orden = costo.orden;
                Ucosto.tarifa_id = costo.tarifa_id;
                Ucosto.valor = costo.valor;

                int affected = await contexto.SaveChangesAsync();

                return (affected == 1);
            }
        }

        public async Task<bool> EliminarCosto(int id)
        {
            using (var contexto = new ipwebec_hydrosEntities())
            {
                IEnumerable<costo_tarifa> costo = contexto.costo_tarifa.Where(b => b.id == id);
                contexto.costo_tarifa.RemoveRange(costo);

                int affected = await contexto.SaveChangesAsync();

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
        public int numB { get; set; }
        public int numM { get; set; }
        public int numT { get; set; }
        public DateTime? fecha_ingreso { get; set; }
        public DateTime? fecha_retiro { get; set; }
        public string NombreMarca { get; set; }
    }

    public partial class ConsultarArticulos
    {
        public int id { get; set; }
        public int grupo_id { get; set; }
        public int unidad_medida_id { get; set; }
        public int impuesto_id { get; set; }
        public int marca_id { get; set; }
        public string clase { get; set; }
        public string nombre_completo { get; set; }
        public string nombre_corto { get; set; }
        public string descripcion { get; set; }
        public byte[] foto { get; set; }
        public string nombre_foto { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public string control_stock { get; set; }
        public Nullable<int> stock_minimo { get; set; }
        public Nullable<int> stock_actual { get; set; }
        public Nullable<decimal> precio_compra { get; set; }
        public Nullable<decimal> precio_venta { get; set; }
        public string nombre_Grupo { get; set; }
        public string nombre_Medida { get; set; }
        public string nombre_impuesto { get; set; }
        public string nombre_marca { get; set; }

    }
}
