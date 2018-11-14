using SolicitudSoporteSistemas_prd.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Web.Security;
using System.Security.Principal;
using System.Net.Mime;
using System.Text.RegularExpressions;

namespace SolicitudSoporteSistemas_prd.Controllers
{
    public class SSController : Controller
    {
        IList<CabeceraSSS> listaCabeceras = new List<CabeceraSSS>{
                            new CabeceraSSS() { EstadoNombre = "Nueva", SolicitudId = 1, TipoSolicitudId = 7, EmpresaId = 4, EmpresaNombre = "Pilares", UbicacionNombre = "Avenida", Proyecto = "1528", Direccion = "Por ahi", AsignadoANombre = "Técnico", SolicitanteNombre = "John",  SolicitanteEmail = "vsandoval@socovesa.cl", CategoriaNombre = "Algo", SubCategoriaNombre = "Detalle", Incidente = true, HorasTotal = 3, TipoSolicitudNombre = "Configuración", Descripcion = "Prueba de nuevo sistema de tickets",  } ,
                            new CabeceraSSS() { EstadoNombre = "En Tratamiento", SolicitudId = 2, TipoSolicitudId = 8, EmpresaId = 4, EmpresaNombre = "Socovesa Stgo", UbicacionNombre = "Calle", Proyecto = "1111", Direccion = "No sé", AsignadoANombre = "Desarrollador", SolicitanteNombre = "Pedro",  SolicitanteEmail = "vsandoval@socovesa.cl", CategoriaNombre = "Otro", SubCategoriaNombre = "Detalle", Incidente = true, HorasTotal = 1, TipoSolicitudNombre = "Desarrollo", Descripcion = "Extender sistema de tickets para atención de proveedores" }
                        };

        // *****************************************************************************************************************
        // ******************************************************************************************************* FUNCIONES
        // *****************************************************************************************************************




        public CabeceraSSS IniciarCabecera()
        {
            string aux_cab = string.Empty, idred = string.Empty;

            if (!string.IsNullOrEmpty(@System.Environment.UserName))
                idred = @System.Environment.UserName;

            CabeceraSSS iniciar_cabecera = new CabeceraSSS()
            {
                SolicitudId = 0,
                //EmpresaId = 14,
                Usuario = aux_cab,
                FechaCreacion = DateTime.Now,
                FechaSolicitada = DateTime.Now,
                FechaComprometida = DateTime.Now.AddDays(1),
                // fecha_resolucion
                // fecha_comprometida
                EstadoId = 1,
                EstadoTipo = aux_cab,
                Descripcion = aux_cab,
                SolicitanteEmail = aux_cab,
                //TipoSolicitudId = 12,
                SolicitanteIdRed = @System.Environment.UserName,
                UbicacionNombre = aux_cab,
                //UbicacionId = 104,
                Direccion = aux_cab,

                AsignadoAEmail = ObtenerEmailPersonal(idred + "@socovesa.cl"),
                AsignadoAId = 1,
                HorasTotal = 0,
                //CategoriaId = 12,
                //Incidente = false,  
                //SubCategoriaId = 12,
                Area = "1",
                SolicitanteId = 0
            };

            return iniciar_cabecera;
        }


        public void IniciarViewbag()
        {
            using (SoporteICSA_prdEntities ctx = new SoporteICSA_prdEntities())
            {
                TipoSolicitud ts = ctx.TipoSolicitud.Where(a => a.TipoSolicitudId == 1).FirstOrDefault();
                List<TipoSolicitud> lts = ctx.TipoSolicitud.OrderBy(x => x.NombreSolicitud).ToList();

                EstadoSolicitud es = ctx.EstadoSolicitud.Where(e => e.EstadoSolicitudId == 1).FirstOrDefault();
                List<EstadoSolicitud> les = ctx.EstadoSolicitud.OrderBy(y => y.NombreEstado).ToList();

                Personal pers = ctx.Personal.Where(u => u.PersonalId == 1).FirstOrDefault();
                List<Personal> lpers = ctx.Personal.ToList();


                //ObraSalaVenta osv = ctx.ObraSalaVentas.Where(ou => ou.ObraSalaVenta1 == 1).FirstOrDefault();
                List<ObraSalaVenta> losv = ctx.ObraSalaVenta.OrderBy(z => z.NombreObraSalaVenta).ToList();
                string crearObraSalaVenta = string.Empty;
                foreach (ObraSalaVenta osv in losv)
                {
                    crearObraSalaVenta = crearObraSalaVenta + "'" + osv.NombreObraSalaVenta + "', ";
                }
                crearObraSalaVenta = crearObraSalaVenta.TrimEnd(' ');
                crearObraSalaVenta = crearObraSalaVenta.TrimEnd(',');

                Empresa em = ctx.Empresa.Where(i => i.EmpresaId == 1).FirstOrDefault();
                List<Empresa> ems = ctx.Empresa.OrderBy(w => w.Nombre).ToList();

                CategoriaSolicitud ca = ctx.CategoriaSolicitud.Where(o => o.CategoriaSolicitudId == 1).FirstOrDefault();
                List<CategoriaSolicitud> lca = ctx.CategoriaSolicitud.OrderBy(f => f.Descr).ToList();

                SubCategoria sc = ctx.SubCategoria.Where(u => u.SubCategoriaId == 1).FirstOrDefault();
                List<SubCategoria> lsc = ctx.SubCategoria.OrderBy(g => g.Descr).ToList();

                //CentroCostoyProyecto ccyp = ctx.CentroCostoyProyectos.Where(t => t.Id == 1).FirstOrDefault();
                List<CentroCostoyProyectos> ccyps = ctx.CentroCostoyProyectos.OrderBy(h => h.Descripcion).ToList();
                string crearCCyP = string.Empty;
                foreach (CentroCostoyProyectos ccyp in ccyps)
                {
                    crearCCyP = crearCCyP + "'" + ccyp.Descripcion + "', ";
                }
                crearCCyP = crearCCyP.TrimEnd(' ');
                crearCCyP = crearCCyP.TrimEnd(',');

                //Sociedade soc = ctx.Sociedades.Where(s => s.Id == 1).FirstOrDefault();
                List<Sociedades> socs = ctx.Sociedades.OrderBy(j => j.Descripcion).ToList();
                string crearSoc = string.Empty;
                foreach (Sociedades soc in socs)
                {
                    crearSoc = crearSoc + "'" + soc.Descripcion + "', ";
                }
                crearSoc = crearSoc.TrimEnd(' ');
                crearSoc = crearSoc.TrimEnd(',');

                ViewBag.EstadosSolicitud = les;
                ViewBag.TiposSolicitud = lts;
                ViewBag.ListaPersonal = lpers;
                //ViewBag.ListaObraSalaVenta = losv;
                ViewBag.StringObraSalaVenta = crearObraSalaVenta;

                ViewBag.Empresas = ems;
                ViewBag.Categorias = lca;
                ViewBag.SubCategorias = lsc;
                //ViewBag.CentroCostoyProyecto = ccyps;
                ViewBag.StringCostoyProyecto = crearCCyP;
                //ViewBag.Sociedades = socs;
                ViewBag.StringSociedades = crearSoc;

                ViewBag.StringCuentas = ObtenerStringDeCUENTASDesdeActiveDirectory();
                //ViewBag.ListaCuentas = ObtenerListaDeCUENTASDesdeActiveDirectory();

                ViewBag.StringSolicitantes = DB_Obtener_Solicitantes();
            }
        }


        public void IniciarViewbagLog()
        {
            using (SoporteICSA_prdEntities ctx = new SoporteICSA_prdEntities())
            {
                List<TipoLog> ltl = ctx.TipoLog.OrderBy(t => t.NombreTL).ToList();
                ViewBag.ListaTipoLog = ltl;
            }
        }


        public UsuarioSSS ConstruirNombreCompleto(string nombrecompleto)
        {
            UsuarioSSS us_aux = new UsuarioSSS();
            string[] nc;
            string aux_nombre = string.Empty, aux_apellido = string.Empty;

            if (!string.IsNullOrEmpty(nombrecompleto))
            {
                if (nombrecompleto.Contains(","))
                {
                    nc = nombrecompleto.Split(',');
                    aux_nombre = nc[1].Trim();
                    aux_apellido = nc[0].Trim();
                }
                else if (nombrecompleto.Contains(" "))
                {
                    nc = nombrecompleto.Split(' ');
                    aux_nombre = nc[0].Trim();
                    aux_apellido = nc[1].Trim();
                }
                else
                {
                    aux_nombre = nombrecompleto;
                    aux_apellido = " ";
                }

                us_aux.nombre = aux_nombre;
                us_aux.apellido = aux_apellido;
                us_aux.nombrecompleto = aux_nombre + " " + aux_apellido;
            }
            else
            {
                us_aux.nombre = null;
                us_aux.apellido = null;
                us_aux.nombrecompleto = null;
            }

            return us_aux;
        }


        public UsuarioSSS ObtenerUsuarioDesdeActiveDirectoryConNickname(string nickname)
        {
            UsuarioSSS us_asociado = new UsuarioSSS();
            string error = string.Empty;
            string[] nc;

            string aux_nombre = string.Empty, aux_apellido = string.Empty;

            try
            {
                string constring = System.Configuration.ConfigurationManager.ConnectionStrings["usuarios_socovesa"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(constring);
                DataSet objSet = new DataSet();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                //string Query = "select PersonalId,Nombre,Apellidopaterno,Cargo,NombreCompleto,email,nickname,Rol from USUARIOS_MAILS where nickname = '" + oportunidadid.ToString().ToUpper() + "'";
                string Query = "select FullName,Description,Dominio,EMAIL from USUARIOS_MAILS where nickname = '" + nickname.ToString().ToUpper() + "'";

                sqlCommand.CommandText = Query;

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(objSet);

                if (objSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in objSet.Tables[0].Rows)
                    {
                        //if (dr["FullName"].ToString().Contains(","))
                        //{
                        //    nc = dr["FullName"].ToString().Split(',');
                        //    aux_nombre = nc[1].Trim();
                        //    aux_apellido = nc[0].Trim();
                        //}
                        //else if (dr["FullName"].ToString().Contains(" "))
                        //{
                        //    nc = dr["FullName"].ToString().Split(' ');
                        //    aux_nombre = nc[0].Trim();
                        //    aux_apellido = nc[1].Trim();
                        //}
                        //else
                        //{
                        //    aux_nombre = dr["FullName"].ToString();
                        //    aux_apellido = " ";
                        //}

                        us_asociado = ConstruirNombreCompleto(dr["FullName"].ToString());

                        us_asociado.id = 1;
                        //us_asociado.nombre = aux_nombre;
                        //us_asociado.apellido = aux_apellido;
                        //us_asociado.nombrecompleto = aux_nombre + " " + aux_apellido;
                        us_asociado.cargo = dr["Description"].ToString();
                        us_asociado.email = dr["EMAIL"].ToString();
                        us_asociado.nickname = nickname.ToString();
                        us_asociado.rol = "tecnico";
                        us_asociado.error = "ok";
                    }
                }
                else
                {
                    us_asociado.id = 0;
                    us_asociado.error = "No EXISTE el nickname " + nickname.ToString().ToUpper() + " en el dominio Socovesa. ";
                }
            }
            catch (Exception er)
            {
                us_asociado.id = 0;
                us_asociado.error = "ERROR al recuperar el nickname " + nickname.ToString().ToUpper() + " del dominio Socovesa: .- " + er.Message + "-. ";
            }

            return us_asociado;
        }


        public UsuarioSSS ObtenerUsuarioDesdeActiveDirectory(string email)
        {
            UsuarioSSS us_asociado = new UsuarioSSS();
            string error = string.Empty;
            string[] nn = email.ToString().Split('@');
            string[] nc;
            string aux_nombre = string.Empty, aux_apellido = string.Empty;

            try
            {
                string constring = System.Configuration.ConfigurationManager.ConnectionStrings["usuarios_socovesa"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(constring);
                DataSet objSet = new DataSet();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                //string Query = "select PersonalId,Nombre,Apellidopaterno,Cargo,NombreCompleto,email,nickname,Rol from USUARIOS_MAILS where nickname = '" + oportunidadid.ToString().ToUpper() + "'";
                string Query = "select FullName,Description,Dominio,EMAIL from USUARIOS_MAILS where upper(email) = ltrim(rtrim('" + email.ToString().ToUpper() + "')) and upper(nickname) = ltrim(rtrim('" + nn[0].Trim().ToString().ToUpper() + "'))";

                sqlCommand.CommandText = Query;

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(objSet);

                if (objSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in objSet.Tables[0].Rows)
                    {
                        //if (dr["FullName"].ToString().Contains(","))
                        //{
                        //    nc = dr["FullName"].ToString().Split(',');
                        //    aux_nombre = nc[1].Trim();
                        //    aux_apellido = nc[0].Trim();
                        //}
                        //else if (dr["FullName"].ToString().Contains(" "))
                        //{
                        //    nc = dr["FullName"].ToString().Split(' ');
                        //    aux_nombre = nc[0].Trim();
                        //    aux_apellido = nc[1].Trim();
                        //}
                        //else
                        //{
                        //    aux_nombre = dr["FullName"].ToString();
                        //    aux_apellido = " ";
                        //}

                        us_asociado = ConstruirNombreCompleto(dr["FullName"].ToString());

                        us_asociado.id = 1;
                        //us_asociado.nombre = aux_nombre;
                        //us_asociado.apellido = aux_apellido;
                        //us_asociado.nombrecompleto = aux_nombre + " " + aux_apellido;
                        us_asociado.cargo = "VIGENTE";
                        us_asociado.email = email.ToString();         // dr["EMAIL"].ToString();
                        us_asociado.nickname = nn[0].Trim();
                        us_asociado.rol = "tecnico";
                        us_asociado.error = "ok";
                    }
                }
                else
                {
                    us_asociado.id = 0;
                    us_asociado.error = "No EXISTE el email " + email.ToString().ToUpper() + " en el dominio Socovesa";
                }
            }
            catch (Exception er)
            {
                us_asociado.id = 0;
                us_asociado.error = "ERROR al recuperar el email " + email.ToString().ToUpper() + " del dominio Socovesa: .- " + er.Message + "-. ";
            }

            return us_asociado;
        }


        public List<UsuarioSSS> ObtenerListaDeCUENTASDesdeActiveDirectory()
        {
            List<UsuarioSSS> Cuentas = new List<UsuarioSSS>();

            string error = string.Empty;
            string[] nc;

            try
            {
                string constring = System.Configuration.ConfigurationManager.ConnectionStrings["usuarios_socovesa"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(constring);
                DataSet objSet = new DataSet();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                //string Query = "select PersonalId,Nombre,Apellidopaterno,Cargo,NombreCompleto,email,nickname,Rol from USUARIOS_MAILS where nickname = '" + oportunidadid.ToString().ToUpper() + "'";
                string Query = "select FullName,Description,Dominio,EMAIL,NickName from USUARIOS_MAILS";

                sqlCommand.CommandText = Query;

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(objSet);

                if (objSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in objSet.Tables[0].Rows)
                    {
                        nc = dr["FullName"].ToString().Split(',');

                        Cuentas.Add(new UsuarioSSS()
                        {
                            id = 1,
                            nombre = nc[1].Trim(),
                            apellido = nc[0].Trim(),
                            nombrecompleto = nc[1].Trim() + " " + nc[0].Trim(),
                            cargo = "VIGENTE",
                            email = dr["EMAIL"].ToString(),
                            nickname = dr["NickName"].ToString(),
                            rol = "tecnico",
                            error = "ok"
                        });
                    }
                }
                else
                {
                    Cuentas.Add(new UsuarioSSS()
                    {
                        id = 0,
                        nombre = "",
                        apellido = "",
                        nombrecompleto = "",
                        cargo = "",
                        email = "",
                        nickname = "",
                        rol = "",
                        error = "No se generó listado de cuentas del dominio Socovesa"
                    });
                }
            }
            catch (Exception er)
            {
                Cuentas.Add(new UsuarioSSS()
                {
                    id = 0,
                    nombre = "",
                    apellido = "",
                    nombrecompleto = "",
                    cargo = "",
                    email = "",
                    nickname = "",
                    rol = "",
                    error = "ERROR al recuperar cuentas del dominio Socovesa: .- " + er.Message + "-. "
                });
            }

            return Cuentas;
        }


        public string ObtenerStringDeCUENTASDesdeActiveDirectory()
        {
            string Cuentas = string.Empty;

            try
            {
                string constring = System.Configuration.ConfigurationManager.ConnectionStrings["usuarios_socovesa"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(constring);
                DataSet objSet = new DataSet();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                //string Query = "select PersonalId,Nombre,Apellidopaterno,Cargo,NombreCompleto,email,nickname,Rol from USUARIOS_MAILS where nickname = '" + oportunidadid.ToString().ToUpper() + "'";
                string Query = "select FullName,Description,Dominio,EMAIL,NickName from USUARIOS_MAILS";

                sqlCommand.CommandText = Query;

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(objSet);

                if (objSet.Tables.Count > 0)
                {
                    foreach (DataRow dr in objSet.Tables[0].Rows)
                    {
                        //Cuentas = Cuentas + "'" + dr["EMAIL"].ToString() + "', ";
                        Cuentas = Cuentas + "{" + "'nickname':'" + dr["NickName"].ToString() + "'" + "," + "'email':'" + dr["EMAIL"].ToString() + "'" + "," + "'nombre':'" + dr["FullName"].ToString() + "'" + "}, ";
                    }

                    Cuentas = Cuentas.TrimEnd(' ');
                    Cuentas = Cuentas.TrimEnd(',');
                }
                else
                {
                    Cuentas = "No se generó listado de cuentas del dominio Socovesa";
                }
            }
            catch (Exception er)
            {
                Cuentas = "ERROR al recuperar cuentas del dominio Socovesa: .- " + er.Message + "-. ";
            }

            return Cuentas;
        }


        public Personal ConstruirPersonal(UsuarioSSS usuario)
        {
            Personal per = new Personal()
            {
                Nombre = usuario.nombre,
                Apellidopaterno = usuario.apellido,
                Cargo = usuario.cargo,
                NombreCompleto = usuario.nombrecompleto,
                email = usuario.email,
                nickname = usuario.nickname,
                Rol = usuario.rol
            };

            return per;
        }


        public UsuarioSSS CrearPersonal(UsuarioSSS us_ss)
        {
            string messages_errors = string.Empty;

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                Personal personal_socovesa = new Personal();
                personal_socovesa = ConstruirPersonal(us_ss);

                using (SoporteICSA_prdEntities bd_us = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        bd_us.Personal.Add(personal_socovesa);
                        int resultado = bd_us.SaveChanges();

                        if (resultado > 0)
                        {
                            messages_errors = "Personal " + personal_socovesa.PersonalId.ToString() + " creado";
                            us_ss.id = personal_socovesa.PersonalId; ;
                            us_ss.error = messages_errors;
                        }
                        else
                        {
                            messages_errors = "No fue creado el Personal";
                            us_ss.id = 0;
                            us_ss.error = messages_errors;
                        }

                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                                //Console.WriteLine(message);
                            }
                        }

                        us_ss.id = 0;
                        us_ss.error = messages_errors;
                    }
                }
            }
            else
            {
                messages_errors = string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));

                us_ss.id = 0;
                us_ss.error = messages_errors;
            }

            return us_ss;
        }


        public int IdentificarPersonal(string email)                    // obtener Id del tecnico desde email                                                
        {
            ////string s = "xyz@yahoo.com";

            //string[] email_as = cab.AsignadoAEmail.Split('@');
            int personalid = 1;                                                             // Valor "Sin Asignar" por defecto
            UsuarioSSS us_email = new UsuarioSSS();
            string[] email_as = email.Split('@');
            UsuarioSSS usuario_nuevo_creado = new UsuarioSSS();

            using (SoporteICSA_prdEntities bd_personal = new SoporteICSA_prdEntities())
            {
                // en Personal se busca por email y nickname 
                string nm_cs = email_as[0].ToUpper();
                Personal Per = bd_personal.Personal.Where(e => e.nickname.ToUpper().Equals(nm_cs.ToUpper()) && e.email.ToUpper().Equals(email.ToUpper())).FirstOrDefault();

                if ((Per != null) && !Per.PersonalId.Equals(null))
                {
                    //sol.PersonalId = Per.PersonalId;
                    personalid = Per.PersonalId;
                }
                else
                {
                    // buscar usuario por nickname en dominio socovesa (tabla USUARIOS_MAILS en CERBERO)
                    // us_email = ObtenerUsuarioDesdeActiveDirectory(email_as[0]);

                    // buscar usuario por email en dominio socovesa(tabla USUARIOS_MAILS en CERBERO)
                    // se busca la info. en el Active Directory ya que no se ingresa en un formulario
                    us_email = ObtenerUsuarioDesdeActiveDirectory(email);

                    if (us_email.id == 0)                                  // no fue posible obtener el usuario (error o no existe)
                    {
                        // Quedará "sin asignar"
                        personalid = 1;
                    }
                    else
                    {
                        // crear usuario en tabla PERSONAL (que fue encontrado en tabla USUARIOS_MAILS) y asignar ID de nuevo usuario
                        usuario_nuevo_creado = CrearPersonal(us_email);

                        //sol.PersonalId = usuario_nuevo_creado.id;
                        personalid = usuario_nuevo_creado.id;
                    }
                }
            }

            return personalid;
        }


        public Solicitante ConstruirSolicitante(UsuarioSSS usuario)
        {
            Solicitante sol = new Solicitante()
            {
                Email = usuario.email.ToLower(),
                NombreCompleto = usuario.nombrecompleto,
                Nombres = usuario.nombre,
                Apellidos = usuario.apellido,
                Ubicacion = usuario.ubicacion,
                Telefono = usuario.telefono,
                Dirección = usuario.direccion,
                EmpresaId = usuario.empresaid,
                Nickname = usuario.nickname.ToLower()
            };

            return sol;
        }


        public UsuarioSSS CrearSolicitante(UsuarioSSS us_ss)
        {
            string messages_errors = string.Empty;

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                Solicitante solicitante = new Solicitante();
                solicitante = ConstruirSolicitante(us_ss);

                using (SoporteICSA_prdEntities bd_us = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        bd_us.Solicitante.Add(solicitante);
                        int resultado = bd_us.SaveChanges();

                        if (resultado > 0)
                        {
                            messages_errors = ".- Solicitante" + solicitante.Id.ToString() + " creado -.";
                            us_ss.id = solicitante.Id;
                            us_ss.error = messages_errors;
                        }
                        else
                        {
                            messages_errors = ".- No fue creado el Solicitante " + us_ss.email + "-.";
                            us_ss.id = 0;
                            us_ss.error = messages_errors;
                        }

                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                                //Console.WriteLine(message);
                            }
                        }

                        us_ss.id = 0;
                        us_ss.error = messages_errors;
                    }
                }
            }
            else
            {
                messages_errors = string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));

                us_ss.id = 0;
                us_ss.error = messages_errors;
            }

            return us_ss;
        }


        public UsuarioSSS ActualizarSolicitante(UsuarioSSS us_ss)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string messages_errors = ".- Error al actualizar Solicitante: ";

            if (ModelState.IsValid)
            {
                using (SoporteICSA_prdEntities bd_as = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        if (us_ss.id > 0)
                        {
                            Solicitante sol = bd_as.Solicitante.Where(s => s.Id == us_ss.id).FirstOrDefault();

                            if ((sol != null) && !sol.Id.Equals(null))
                            {
                                if (!string.IsNullOrEmpty(us_ss.email))
                                    sol.Email = us_ss.email;

                                if (!string.IsNullOrEmpty(us_ss.nombrecompleto))
                                    sol.NombreCompleto = us_ss.nombrecompleto;

                                if (!string.IsNullOrEmpty(us_ss.nombre))
                                    sol.Nombres = us_ss.nombre;

                                if (!string.IsNullOrEmpty(us_ss.apellido))
                                    sol.Apellidos = us_ss.apellido;

                                if (!string.IsNullOrEmpty(us_ss.ubicacion))
                                    sol.Ubicacion = us_ss.ubicacion;

                                if (!string.IsNullOrEmpty(us_ss.telefono))
                                    sol.Telefono = us_ss.telefono;

                                if (!string.IsNullOrEmpty(us_ss.direccion))
                                    sol.Dirección = us_ss.direccion;

                                if (us_ss.empresaid.HasValue)
                                    sol.EmpresaId = us_ss.empresaid;

                                //bd_as.Solicitante.Attach(sol);
                                bd_as.Entry(sol).State = System.Data.Entity.EntityState.Modified;

                                int resultado = bd_as.SaveChanges();

                                if (resultado > 0)
                                {
                                    us_ss.error = "ok";
                                }
                                else
                                {
                                    us_ss.error = messages_errors + "No fue posible actualizar el estado del solicitante " + us_ss.email + " (" + us_ss.id + ") -.";
                                }
                            }
                            else
                            {
                                us_ss.error = "No se encontró solicitante " + us_ss.email + " en BD -.";
                            }
                        }
                        else
                        {
                            us_ss.error = "No existe solicitante " + us_ss.email + " en BD para actualizar -.";
                        }

                        return us_ss;
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            }
                        }

                        us_ss.error = messages_errors;
                        return us_ss;
                    }
                }
            }
            else
            {
                us_ss.error = messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
                return us_ss;
            }
        }


        public int IdentificarSolicitante(UsuarioSSS us_email)                    // obtener Id del solicitante desde email                                                
        {
            int solicitanteid = 1;                                                             // Valor "Sin Asignar" por defecto
            UsuarioSSS us_aux = ConstruirNombreCompleto(us_email.nombrecompleto);

            //string[] email_as = email.Split('@');
            string[] email_as = us_email.email.Split('@');

            us_email.nickname = email_as[0];
            us_email.nombrecompleto = us_aux.nombrecompleto;
            us_email.nombre = us_aux.nombre;
            us_email.apellido = us_aux.apellido;

            UsuarioSSS usuario_nuevo_creado = new UsuarioSSS();
            UsuarioSSS usuario_actualizado = new UsuarioSSS();

            using (SoporteICSA_prdEntities bd_sol = new SoporteICSA_prdEntities())
            {
                // en solicitante se busca por nickname (que es unico)
                string nm_cs = email_as[0].ToUpper();
                Solicitante Sol = bd_sol.Solicitante.Where(s => s.Nickname.ToUpper().Equals(nm_cs.ToUpper())).FirstOrDefault();

                if ((Sol != null) && !Sol.Id.Equals(null))
                {
                    // Actualizar Solicitante
                    // Pendiente: manejo de error al actualizar un solicitante
                    us_email.id = Sol.Id;
                    usuario_actualizado = ActualizarSolicitante(us_email);

                    solicitanteid = Sol.Id;
                }
                else
                {
                    // buscar usuario por nickname en dominio socovesa (tabla USUARIOS_MAILS en CERBERO)
                    // us_email = ObtenerUsuarioDesdeActiveDirectory(email_as[0]);

                    // buscar usuario por email en dominio socovesa(tabla USUARIOS_MAILS en CERBERO)
                    // No se busca en el Active Directory ya que la info. se ingresa en el formulario
                    //us_email = ObtenerUsuarioDesdeActiveDirectory(email);

                    // crear usuario en tabla SOLICITANTE y asignar ID de nuevo usuario
                    usuario_nuevo_creado = CrearSolicitante(us_email);

                    if (us_email.id == 0)                                  // no fue posible obtener el usuario (error o no existe)
                    {
                        // Quedará "sin asignar"
                        solicitanteid = 1;
                    }
                    else
                    {
                        solicitanteid = usuario_nuevo_creado.id;
                    }
                }
            }

            return solicitanteid;
        }



        //public Solicitud ConstruirSolicitudRapida(CabeceraSSS cab)
        //{
        //    string aux = string.Empty;
        //    UsuarioSSS us_as = new UsuarioSSS();
        //    Solicitud sol = new Solicitud()
        //    {
        //        //SolicitudId = idsolicitud,
        //        //EmpresaId = 14,
        //        //usuario = aux,
        //        fecha_creacion = DateTime.Now,
        //        //fecha_solicitud = DateTime.Now,
        //        //fecha_resolucion = DateTime.Now,
        //        //fecha_comprometida = DateTime.Now,
        //        //EstadoSolicitudId = 1,
        //        //Descr = aux,
        //        //SolicitanteEmail = aux,
        //        //TipoSolicitudId = 12,
        //        //SolicitanteIdRed = aux,
        //        //Direccion = aux,
        //        //Obra_Oficina_SVenta = aux,
        //        //Obra = aux,
        //        PersonalId = 1,                             // 'Sin asignar' por defecto
        //        //Telefono = aux,
        //        Horas = 0,
        //        //CategoriaSolicitudId = 12,
        //        EsIncidente = 0,
        //        //SubCategoriaId = 12,
        //        Area = 1
        //    };

        //    // campos obligatorios
        //    sol.EstadoSolicitudId = cab.EstadoId;
        //    sol.Descr = cab.Descripcion;
        //    sol.SolicitanteEmail = cab.SolicitanteEmail;
        //    sol.TipoSolicitudId = cab.TipoSolicitudId;
        //    sol.Obra_Oficina_SVenta = cab.UbicacionNombre;

        //    if (!string.IsNullOrEmpty(cab.AsignadoAEmail))
        //    {
        //        //string s = "xyz@yahoo.com";
        //        string[] email_as = cab.AsignadoAEmail.Split('@');
        //        UsuarioSSS usuario_nuevo_creado = new UsuarioSSS();

        //        using (SoporteICSA_prdEntities bd_personal = new SoporteICSA_prdEntities())
        //        {
        //            string nm_cs = email_as[0].ToUpper();
        //            Personal Per = bd_personal.Personals.Where(e => e.nickname.ToUpper().Equals(nm_cs)).FirstOrDefault();

        //            if((Per != null) && !Per.PersonalId.Equals(null))
        //            {
        //                sol.PersonalId = Per.PersonalId;
        //            }
        //            else
        //            {
        //                us_as = ObtenerUsuarioAsignado(email_as[0]);        // buscar usuario por nickname en dominio socovesa (tabla USUARIOS_MAILS en CERBERO)

        //                if (us_as.id == 0)                                  // no fue posible crear el usuario
        //                {
        //                    // Quedará "sin asignar"
        //                }
        //                else
        //                {
        //                    // crear usuario en tabla PERSONAL (que fue encontrado en tabla USUARIOS_MAILS) y asignar ID de nuevo usuario
        //                    usuario_nuevo_creado = CrearPersonal(us_as);

        //                    sol.PersonalId = usuario_nuevo_creado.id;
        //                }
        //            }
        //        }
        //    }

        //    //Otros

        //    if (!string.IsNullOrEmpty(cab.Direccion))
        //        sol.Direccion = cab.Direccion;

        //    if (cab.EmpresaId.HasValue)
        //        sol.EmpresaId = cab.EmpresaId;

        //    if (!string.IsNullOrEmpty(cab.SolicitanteIdRed))
        //        sol.SolicitanteIdRed = cab.SolicitanteIdRed;

        //    if (cab.CategoriaId.HasValue)
        //        sol.CategoriaSolicitudId = cab.CategoriaId;

        //    if (cab.SubCategoriaId.HasValue)
        //        sol.SubCategoriaId = cab.SubCategoriaId;

        //    if (cab.Incidente)
        //    {
        //        sol.EsIncidente = Convert.ToInt32(cab.Incidente);
        //    }

        //    return sol;
        //}


        private IList<SubCategoria> ObtenerSubcategoria(int id)
        {
            using (SoporteICSA_prdEntities ctx = new SoporteICSA_prdEntities())
            {
                return ctx.SubCategoria.Where(g => g.CategoriaSolicitudId == id).ToList();
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CargarSubByCat(string id)
        {
            var modelList = this.ObtenerSubcategoria(Convert.ToInt32(id));

            var modelData = modelList.Select(m => new SelectListItem()
            {
                Text = m.Descr,
                Value = m.SubCategoriaId.ToString()
            });

            return Json(modelData, JsonRequestBehavior.AllowGet);
        }


        public Solicitud ConstruirSolicitud(CabeceraSSS cab)            // de Cabecera a Solicitud
        {
            string aux = string.Empty;
            UsuarioSSS us_sol = new UsuarioSSS();

            Solicitud sol = new Solicitud()
            {
                SolicitudId = 0,
                //EmpresaId = 14,
                //usuario = aux,
                fecha_creacion = DateTime.Now,
                //fecha_solicitud = DateTime.Now,
                //fecha_resolucion = DateTime.Now,
                //fecha_comprometida = DateTime.Now,
                //EstadoSolicitudId = 1,
                //Descr = aux,
                //SolicitanteEmail = aux,
                //TipoSolicitudId = 12,
                //SolicitanteIdRed = aux,
                //Direccion = aux,
                //Obra_Oficina_SVenta = aux,
                //Obra = aux,
                PersonalId = 1,                                         // 'Sin asignar' por defecto
                //Telefono = aux,
                Horas = 0,
                //CategoriaSolicitudId = 12,
                EsIncidente = 0,
                //SubCategoriaId = 12,
                Area = 1
            };

            // campos obligatorios
            sol.Descr = cab.Descripcion;

            if (!string.IsNullOrEmpty(cab.UbicacionNombre))
            {
                sol.Obra_Oficina_SVenta = cab.UbicacionNombre;
                us_sol.ubicacion = cab.UbicacionNombre;
            }

            // para la creación, se asigna SolicitudId igual a 0 (cero)
            sol.SolicitudId = cab.SolicitudId;




            // Otros

            if (!string.IsNullOrEmpty(cab.SolicitanteNombre))
            {
                sol.SolicitanteNombre = cab.SolicitanteNombre;
                us_sol.nombrecompleto = cab.SolicitanteNombre;
            }

            if (!string.IsNullOrEmpty(cab.SolicitanteIdRed))
            {
                sol.SolicitanteIdRed = cab.SolicitanteIdRed;
            }


            if (!string.IsNullOrEmpty(cab.Direccion))
            {
                sol.Direccion = cab.Direccion;
                us_sol.direccion = cab.Direccion;
            }


            if (!string.IsNullOrEmpty(cab.Telefono))
            {
                sol.Telefono = cab.Telefono;
                us_sol.telefono = cab.Telefono;
            }


            if (cab.Incidente)
            {
                sol.EsIncidente = Convert.ToInt32(cab.Incidente);
            }

            if (cab.HorasTotal.HasValue)
                sol.Horas = cab.HorasTotal;

            if (cab.Referencia.HasValue)
                sol.Referencia = cab.Referencia;

            if (cab.FechaCreacion.HasValue)
                sol.fecha_creacion = cab.FechaCreacion;

            if (cab.FechaSolicitada.HasValue)
                sol.fecha_solicitud = cab.FechaSolicitada;

            if (cab.FechaComprometida.HasValue)
                sol.fecha_comprometida = cab.FechaComprometida;

            // ***********************************************************************************

            // se actualiza automaticamente la fecha de resolución en solicitudes creadas en la app. nueva
            // no se consideran solicitudes antiguas con estado final pero sin fecha de resolucion

            //if (cab.FechaResolucion.HasValue)
            //    sol.fecha_resolucion = cab.FechaResolucion;

            using (SoporteICSA_prdEntities bd_cs = new SoporteICSA_prdEntities())
            {
                //EstadoSolicitud EstSol = bd_cs.EstadoSolicituds.Where(es => es.EstadoSolicitudId == sol.EstadoSolicitudId.Value).FirstOrDefault();
                //sol.EstadoSolicitud = EstSol;
                //sol.EstadoSolicitud.NombreEstado = EstSol.NombreEstado;
                //sol.EstadoSolicitud.tipo = EstSol.tipo;

                if (cab.EmpresaId.HasValue)
                {
                    sol.EmpresaId = cab.EmpresaId;
                    //sol.Empresa = bd_cs.Empresas.Where(em => em.EmpresaId == cab.EmpresaId.Value).FirstOrDefault();
                    sol.Empresa = bd_cs.Empresa.Where(em => em.EmpresaId == cab.EmpresaId.Value).Single();
                    us_sol.empresaid = cab.EmpresaId;
                }

                if (cab.CategoriaId.HasValue)
                {
                    sol.CategoriaSolicitudId = cab.CategoriaId;
                    //sol.CategoriaSolicitud = bd_cs.CategoriaSolicituds.Where(ca => ca.CategoriaSolicitudId == cab.CategoriaId).FirstOrDefault();
                    //sol.CategoriaSolicitud = bd_cs.CategoriaSolicituds.Single(ca => ca.CategoriaSolicitudId == cab.CategoriaId);
                    sol.CategoriaSolicitud = bd_cs.CategoriaSolicitud.Where(ca => ca.CategoriaSolicitudId == cab.CategoriaId).Single();
                }

                if (cab.SubCategoriaId.HasValue)
                    if (cab.SubCategoriaId > 0)
                        sol.SubCategoriaId = cab.SubCategoriaId;

                sol.EstadoSolicitudId = cab.EstadoId;
                //sol.EstadoSolicitud = bd_cs.EstadoSolicituds.Where(est => est.EstadoSolicitudId == cab.EstadoId).FirstOrDefault();
                //sol.EstadoSolicitud = bd_cs.EstadoSolicituds.Single(est => est.EstadoSolicitudId == cab.EstadoId);
                //sol.EstadoSolicitud = bd_cs.EstadoSolicituds.First(est => est.EstadoSolicitudId == cab.EstadoId);
                sol.EstadoSolicitud = bd_cs.EstadoSolicitud.Where(est => est.EstadoSolicitudId == cab.EstadoId).Single();

                sol.TipoSolicitudId = cab.TipoSolicitudId;
                //sol.TipoSolicitud = bd_cs.TipoSolicituds.Where(ts => ts.TipoSolicitudId == cab.TipoSolicitudId).FirstOrDefault();
                //sol.TipoSolicitud = bd_cs.TipoSolicituds.Single(ts => ts.TipoSolicitudId == cab.TipoSolicitudId);
                sol.TipoSolicitud = bd_cs.TipoSolicitud.Where(ts => ts.TipoSolicitudId == cab.TipoSolicitudId).Single();

                if (!string.IsNullOrEmpty(cab.AsignadoAEmail))
                {
                    int aux_personalid = IdentificarPersonal(cab.AsignadoAEmail);
                    sol.PersonalId = aux_personalid;
                    sol.Personal = bd_cs.Personal.Where(per => per.PersonalId == aux_personalid).Single();
                }

                if (!string.IsNullOrEmpty(cab.SolicitanteEmail))
                {
                    sol.SolicitanteEmail = cab.SolicitanteEmail;
                    us_sol.email = cab.SolicitanteEmail;

                    int aux_solicitanteid = IdentificarSolicitante(us_sol);
                    sol.SolicitanteId = aux_solicitanteid;
                    sol.Solicitante = bd_cs.Solicitante.Where(s => s.Id == aux_solicitanteid).Single();
                }
            }

            if (sol.EstadoSolicitud.tipo.Equals("final"))
            {
                if (cab.FechaResolucion.HasValue)
                {
                    // nuevo estado final, pero sin actualizar la fecha de resolución original
                }
                else
                    sol.fecha_resolucion = DateTime.Now;
            }
            else
            {
                if (cab.FechaResolucion.HasValue)
                    sol.fecha_resolucion = null;
            }


            //sol.EstadoSolicitud = null;

            // ***********************************************************************************

            if (!string.IsNullOrEmpty(cab.SociedadNombre))
                sol.Sociedad = cab.SociedadNombre;

            if (!string.IsNullOrEmpty(cab.CentroCostoNombre))
                sol.CCyP = cab.CentroCostoNombre;

            if (!string.IsNullOrEmpty(cab.Creacion))
                sol.Creacion = cab.Creacion;

            return sol;
        }


        public ActionResult CrearSolicitudSoporteSistemas(CabeceraSSS objetounicoeneluniverso, string tipo)
        {
            string messages_errors = "Error al Crear Solicitud: ";

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                Solicitud cSSS = new Solicitud();

                cSSS = ConstruirSolicitud(objetounicoeneluniverso);
                cSSS.Creacion = tipo;

                using (SoporteICSA_prdEntities bd = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        bd.Solicitud.Add(cSSS);
                        int resultado = bd.SaveChanges();

                        if (resultado > 0)
                        {
                            messages_errors = "Solicitud " + cSSS.Creacion + " " + cSSS.SolicitudId.ToString() + " creada";
                            TempData["exito"] = messages_errors;
                            return RedirectToAction("ModificarSSS", "SS", new { @id = cSSS.SolicitudId });
                        }
                        else
                        {
                            messages_errors = messages_errors + "No fue creado el registro";
                            TempData["error"] = messages_errors;
                            //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                            return RedirectToAction("ErrorSSS", "SS");
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                                //Console.WriteLine(message);
                            }
                        }

                        TempData["error"] = messages_errors;
                        //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                        return RedirectToAction("ErrorSSS", "SS");
                    }
                }
            }
            else
            {
                messages_errors = messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));

                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                return RedirectToAction("ErrorSSS", "SS");
            }
        }


        [ValidateInput(false)]
        public ActionResult CrearModificarSolicitudSoporteSistemas(CabeceraSSS objetounicoeneluniverso, int accion, string tipo, List<Modificacion> lista_mod)
        {
            // Accion ======> 1: crear; 2: modificar
            // Tipo ======> rápida o completa

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string messages_errors = "Error al Crear/Modificar Solicitud: ";
            string rla = string.Empty;
            string rem = string.Empty;
            string desc_inicial = string.Empty;
            string nombreEmpresa = string.Empty, nombreEstado = string.Empty, nombreTipoSolicitud = string.Empty, nombreCategoria = string.Empty;
            string nombreDescripcion = string.Empty;
            string nombreSolicitante = string.Empty, emailSolicitante = string.Empty;
            string nombreTecnico = string.Empty, emailTecnico = string.Empty;
            string nicknametecnico = string.Empty;
            string descripcion_auxiliar = string.Empty;
            int contador_auxiliar = 0;
            bool estadofinal = false, enviaremail = false;

            string lamod_desc = string.Empty;
            LogSSS lamod = new LogSSS();

            if (ModelState.IsValid)
            {
                Solicitud auxiliarSSS = new Solicitud();
                auxiliarSSS = ConstruirSolicitud(objetounicoeneluniverso);

                if (auxiliarSSS.Empresa != null)
                    nombreEmpresa = auxiliarSSS.Empresa.Nombre;
                if (auxiliarSSS.EstadoSolicitud != null)
                    nombreEstado = auxiliarSSS.EstadoSolicitud.NombreEstado;
                if (auxiliarSSS.EstadoSolicitud.tipo.Equals("final"))
                    estadofinal = true;
                if (auxiliarSSS.TipoSolicitud != null)
                    nombreTipoSolicitud = auxiliarSSS.TipoSolicitud.NombreSolicitud;
                if (auxiliarSSS.CategoriaSolicitud != null)
                    nombreCategoria = auxiliarSSS.CategoriaSolicitud.Descr;
                if (auxiliarSSS.Personal != null)
                {
                    nombreTecnico = auxiliarSSS.Personal.NombreCompleto;
                    emailTecnico = auxiliarSSS.Personal.email;
                }

                if (auxiliarSSS.Descr != null)
                    nombreDescripcion = auxiliarSSS.Descr;
                if (auxiliarSSS.SolicitanteEmail != null)
                    emailSolicitante = auxiliarSSS.SolicitanteEmail;

                auxiliarSSS.Empresa = null;
                auxiliarSSS.EstadoSolicitud = null;
                auxiliarSSS.TipoSolicitud = null;
                auxiliarSSS.CategoriaSolicitud = null;
                auxiliarSSS.Personal = null;
                //auxiliarSSS.Descr = null;
                //auxiliarSSS.SolicitanteEmail = null;

                auxiliarSSS.DocAdjuntos = null;
                auxiliarSSS.Log = null;
                auxiliarSSS.Status = null;
                auxiliarSSS.TareaSolicitud = null;
                auxiliarSSS.Solicitante = null;

                objetounicoeneluniverso.SolicitudId = auxiliarSSS.SolicitudId;
                objetounicoeneluniverso.AsignadoANombre = nombreTecnico;
                objetounicoeneluniverso.AsignadoAEmail = emailTecnico;
                objetounicoeneluniverso.SolicitanteNombre = auxiliarSSS.SolicitanteNombre;
                objetounicoeneluniverso.SolicitanteEmail = auxiliarSSS.SolicitanteEmail;
                objetounicoeneluniverso.Telefono = auxiliarSSS.Telefono;
                objetounicoeneluniverso.EstadoNombre = nombreEstado;


                using (SoporteICSA_prdEntities bd = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        if (accion == 1)                                 // CREAR... o ( auxiliarSSS.SolicitudId == 0)
                        {
                            auxiliarSSS.Creacion = tipo;
                            bd.Solicitud.Add(auxiliarSSS);
                        }
                        else if (accion == 2)                            // MODIFICAR... o ( auxiliarSSS.SolicitudId != 0)
                        {
                            bd.Entry(auxiliarSSS).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            // otra opcion no considerada por el momento
                        }

                        int resultado = bd.SaveChanges();


                        if (resultado > 0)
                        {
                            if (!string.IsNullOrEmpty(objetounicoeneluniverso.AsignadoAEmail))
                            {
                                string[] e_tec = objetounicoeneluniverso.AsignadoAEmail.ToString().Split('@');
                                nicknametecnico = e_tec[0].Trim();
                            }

                            objetounicoeneluniverso.SolicitudId = auxiliarSSS.SolicitudId;
                            objetounicoeneluniverso.AsignadoAId = auxiliarSSS.PersonalId;

                            if (accion == 1)
                            {
                                // 1.- Enviar EMAIL
                                // 2.- Crear LOG AUTOMÁTICO


                                // 1.- Enviar EMAIL

                                rem = EnviarEmailSSSAutomatico(objetounicoeneluniverso);

                                // PENDIENTE: ¿que hacer si NO envia Email?


                                // 2.- Crear LOG AUTOMÁTICO

                                //string exito = "Solicitud nro. " + auxiliarSSS.SolicitudId.ToString() + " creada (en forma " + auxiliarSSS.Creacion + ")";
                                string exito = auxiliarSSS.Descr;

                                LogSSS lacre = new LogSSS();
                                lacre = IniciarLogSSS();

                                lacre.LogSolicitudId = objetounicoeneluniverso.SolicitudId;
                                lacre.LogEmail = true;                                     // enviar email para informar que se creo la solicitud
                                lacre.LogAccionId = 1;                                     // Automático
                                lacre.LogTipoId = 1;                                       // Creación
                                lacre.LogUsuarioIdRed = @System.Environment.UserName;
                                //la.LogUsuarioId = auxiliarSSS.PersonalId;
                                lacre.LogDescripcion = exito;
                                lacre.LogTecnicoEmail = nicknametecnico;
                                lacre.LogPersonalId = objetounicoeneluniverso.AsignadoAId;
                                lacre.LogHora = objetounicoeneluniverso.HorasTotal;

                                if (!string.IsNullOrEmpty(rem))
                                {
                                    if (rem.Length > 450)
                                        lacre.LogResultadoEnvioEmail = rem.Substring(0, 450);
                                    else
                                        lacre.LogResultadoEnvioEmail = rem;
                                }

                                rla = CrearLogSolicitudSoporteSistemas_Automatico(lacre);

                                // PENDIENTE: ¿que hacer si NO crea el log automático?


                                if (estadofinal)
                                {
                                    TempData["exito"] = exito;
                                    return RedirectToAction("CrearSSSComp", "SS");
                                }
                                else
                                {
                                    TempData["exito"] = exito;
                                    return RedirectToAction("ModificarSSS", "SS", new { @id = auxiliarSSS.SolicitudId });
                                }

                            }
                            else if (accion == 2)
                            {
                                // 1.- Enviar EMAIL
                                // 2.- Crear LOG AUTOMÁTICO


                                if (lista_mod.Count() > 0)
                                {
                                    descripcion_auxiliar = objetounicoeneluniverso.Descripcion;

                                    foreach (var mod in lista_mod)
                                    {
                                        lamod_desc = string.Empty;
                                        rem = string.Empty;
                                        lamod = new LogSSS();

                                        lamod = IniciarLogSSS();
                                        lamod.LogSolicitudId = objetounicoeneluniverso.SolicitudId;
                                        lamod.LogAccionId = 1;                                  // Automático
                                        lamod.LogUsuarioIdRed = @System.Environment.UserName;
                                        //la.LogUsuarioId = auxiliarSSS.PersonalId;

                                        if (mod.tipoModificacionId.HasValue)
                                            lamod.LogTipoId = mod.tipoModificacionId.Value;
                                        else
                                        {
                                            lamod.LogTipoId = 7;
                                            mod.tipoModificacionId = 7;
                                        }

                                        // 1.- Enviar EMAIL si cambia de TECNICO o nuevo ESTADO es tipo "Final"
                                        if (mod.campoId.Equals("tecnico"))
                                        {
                                            if (mod.tipoModificacionId == 2)
                                            {
                                                lamod_desc = mod.comentario + objetounicoeneluniverso.AsignadoAEmail + "'";
                                            }
                                            else if (mod.tipoModificacionId == 5)
                                            {
                                                lamod_desc = mod.comentario + objetounicoeneluniverso.AsignadoAEmail + "'";
                                            }
                                            else if (mod.tipoModificacionId == 6)
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                            else
                                            {
                                                lamod_desc = mod.comentario;
                                            }

                                            lamod.LogEmail = true;
                                            enviaremail = true;

                                            //objetounicoeneluniverso.Descripcion = lamod_desc;
                                            //objetounicoeneluniverso.Descripcion = lamod_desc + " <br />" + "Solicitud: " + objetounicoeneluniverso.Descripcion;
                                            //objetounicoeneluniverso.Descripcion = objetounicoeneluniverso.Descripcion + " <br />" + "Evento: " + lamod_desc;
                                            descripcion_auxiliar = descripcion_auxiliar + " <br />" + "Evento: " + lamod_desc;

                                            //rem = EnviarEmailSSSAutomatico(objetounicoeneluniverso);
                                        }
                                        else if (mod.campoId.Equals("empresaid"))
                                        {
                                            if (mod.tipoModificacionId == 2)
                                            {
                                                lamod_desc = mod.comentario + nombreEmpresa + "'";
                                            }
                                            else if (mod.tipoModificacionId == 5)
                                            {
                                                lamod_desc = mod.comentario + nombreEmpresa + "'";
                                            }
                                            else if (mod.tipoModificacionId == 6)
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                            else
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                        }
                                        else if (mod.campoId.Equals("tiposolicitudid"))
                                        {
                                            lamod_desc = mod.comentario + nombreTipoSolicitud + "'";
                                        }
                                        else if (mod.campoId.Equals("categoriaid"))
                                        {
                                            if (mod.tipoModificacionId == 2)
                                            {
                                                lamod_desc = mod.comentario + nombreCategoria + "'";
                                            }
                                            else if (mod.tipoModificacionId == 5)
                                            {
                                                lamod_desc = mod.comentario + nombreCategoria + "'";
                                            }
                                            else if (mod.tipoModificacionId == 6)
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                            else
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                        }
                                        else if (mod.campoId.Equals("solicitante"))
                                        {
                                            if (mod.tipoModificacionId == 2)
                                            {
                                                lamod_desc = mod.comentario + objetounicoeneluniverso.SolicitanteEmail + "'";
                                            }
                                            else if (mod.tipoModificacionId == 5)
                                            {
                                                lamod_desc = mod.comentario + objetounicoeneluniverso.SolicitanteEmail + "'";
                                            }
                                            else if (mod.tipoModificacionId == 6)
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                            else
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                        }
                                        else if (mod.campoId.Equals("descripcion"))
                                        {
                                            if (mod.tipoModificacionId == 2)
                                            {
                                                lamod_desc = mod.comentario + objetounicoeneluniverso.Descripcion + "'";
                                            }
                                            else if (mod.tipoModificacionId == 5)
                                            {
                                                lamod_desc = mod.comentario + objetounicoeneluniverso.Descripcion + "'";
                                            }
                                            else if (mod.tipoModificacionId == 6)
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                            else
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                        }
                                        else if (mod.campoId.Equals("estadoid"))
                                        {
                                            lamod_desc = mod.comentario + nombreEstado + "'";

                                            //if (auxiliarSSS.EstadoSolicitud.tipo.Equals("final"))
                                            if (estadofinal)
                                            {
                                                lamod.LogEmail = true;
                                                enviaremail = true;
                                                lamod.LogTipoId = 8;

                                                //objetounicoeneluniverso.Descripcion = lamod_desc;
                                                //objetounicoeneluniverso.Descripcion = objetounicoeneluniverso.Descripcion + " <br />" + "Evento: " + lamod_desc;
                                                descripcion_auxiliar = descripcion_auxiliar + " <br />" + "Evento: " + lamod_desc;

                                                //rem = EnviarEmailSSSAutomatico(objetounicoeneluniverso);
                                            }
                                        }
                                        else if (mod.campoId.Equals("fechacomprometida"))
                                        {
                                            if (mod.tipoModificacionId == 2)
                                            {
                                                lamod_desc = mod.comentario + objetounicoeneluniverso.FechaComprometida.ToString().Substring(0, 10) + "'";
                                            }
                                            else if (mod.tipoModificacionId == 5)
                                            {
                                                lamod_desc = mod.comentario + objetounicoeneluniverso.FechaComprometida.ToString().Substring(0, 10) + "'";
                                            }
                                            else if (mod.tipoModificacionId == 6)
                                            {
                                                lamod_desc = mod.comentario;
                                            }
                                            else
                                            {
                                                lamod_desc = mod.comentario;
                                            }

                                            lamod.LogEmail = true;
                                            enviaremail = true;

                                            //objetounicoeneluniverso.Descripcion = lamod_desc;
                                            //objetounicoeneluniverso.Descripcion = objetounicoeneluniverso.Descripcion + " <br />" + "Evento: " + lamod_desc;
                                            descripcion_auxiliar = descripcion_auxiliar + " <br />" + "Evento: " + lamod_desc;

                                            //rem = EnviarEmailSSSAutomatico(objetounicoeneluniverso);                                            
                                        }
                                        else
                                        {

                                        }


                                        if ((contador_auxiliar == (lista_mod.Count() - 1)) && enviaremail == true)
                                        {
                                            objetounicoeneluniverso.Descripcion = descripcion_auxiliar;
                                            rem = EnviarEmailSSSAutomatico(objetounicoeneluniverso);
                                        }


                                        if (!string.IsNullOrEmpty(rem))
                                        {
                                            if (rem.Length > 450)
                                                lamod.LogResultadoEnvioEmail = rem.Substring(0, 450);
                                            else
                                                lamod.LogResultadoEnvioEmail = rem;
                                        }

                                        lamod.LogDescripcion = lamod_desc;
                                        lamod.LogTecnicoEmail = nicknametecnico;
                                        lamod.LogPersonalId = objetounicoeneluniverso.AsignadoAId;


                                        // 2.- Crear LOG AUTOMÁTICO

                                        rla = CrearLogSolicitudSoporteSistemas_Automatico(lamod);

                                        contador_auxiliar = contador_auxiliar + 1;

                                        // PENDIENTE: ¿que hacer si NO crea el log automático?
                                    }


                                    //objetounicoeneluniverso.Descripcion = descripcion_auxiliar;
                                    //rem = EnviarEmailSSSAutomatico(objetounicoeneluniverso);


                                }


                                // 2.- Enviar EMAIL si estado es tipo "Final"

                                //rem = EnviarEmailSSSAutomatico(objetounicoeneluniverso);

                                // PENDIENTE: ¿que hacer si NO envia Email?


                                if (estadofinal)
                                {
                                    TempData["exito"] = "Solicitud " + auxiliarSSS.SolicitudId.ToString() + " modificada";
                                    //return RedirectToAction("ModificarSSS", "SS", new { @id = auxiliarSSS.SolicitudId });
                                    return RedirectToAction("MisSSSPendientes", "SS", new { @idred = System.Security.Principal.WindowsIdentity.GetCurrent().Name });

                                }
                                else
                                {
                                    TempData["exito"] = "Solicitud " + auxiliarSSS.SolicitudId.ToString() + " modificada";
                                    return RedirectToAction("ModificarSSS", "SS", new { @id = auxiliarSSS.SolicitudId });
                                    //return RedirectToAction("MisSSSPendientes", "SS", new { @idred = System.Security.Principal.WindowsIdentity.GetCurrent().Name });

                                }

                            }
                            else
                            {
                                TempData["error"] = messages_errors + "Registro creado/modificado, sin embargo NO fue identificado como Creación o Modificación";
                                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                                return RedirectToAction("ErrorSSS", "SS");
                            }
                        }
                        else
                        {
                            TempData["error"] = messages_errors + "No fue creado/modificado el registro";
                            //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                            return RedirectToAction("ErrorSSS", "SS");
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            }
                        }

                        TempData["error"] = messages_errors;
                        //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                        return RedirectToAction("ErrorSSS", "SS");
                    }
                }
            }
            else
            {
                TempData["error"] = messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
                //TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                return RedirectToAction("ErrorSSS", "SS");
            }
        }


        public CabeceraSSS ObtenerCabecera(int idsolicitud)             // de Solicitud (bd) a Cabecera
        {
            string aux = string.Empty;

            CabeceraSSS cabecera = new CabeceraSSS()
            {
                AsignadoAId = 1,
                HorasTotal = 0,
                Incidente = false,
                Area = "1"
            };

            using (SoporteICSA_prdEntities bd = new SoporteICSA_prdEntities())
            {
                Solicitud Sol = bd.Solicitud.Where(e => e.SolicitudId == idsolicitud).FirstOrDefault();

                if ((Sol != null) && !Sol.SolicitudId.Equals(null))
                {
                    cabecera.SolicitudId = Sol.SolicitudId;
                    cabecera.FechaCreacion = Sol.fecha_creacion.Value;
                    cabecera.Descripcion = Sol.Descr;

                    if (Sol.EstadoSolicitudId.HasValue)
                    {
                        cabecera.EstadoId = Sol.EstadoSolicitudId.Value;
                        cabecera.EstadoNombre = Sol.EstadoSolicitud.NombreEstado.ToString();

                        //EstadoSolicitud EstSol = bd.EstadoSolicituds.Where(es => es.EstadoSolicitudId == Sol.EstadoSolicitudId.Value).FirstOrDefault();
                        //cabecera.EstadoTipo = EstSol.tipo;
                        cabecera.EstadoTipo = Sol.EstadoSolicitud.tipo;
                    }

                    if (Sol.TipoSolicitudId.HasValue)
                    {
                        cabecera.TipoSolicitudId = Sol.TipoSolicitudId.Value;
                        cabecera.TipoSolicitudNombre = Sol.TipoSolicitud.NombreSolicitud.ToString();
                    }

                    if (Sol.PersonalId.HasValue)
                    {
                        cabecera.AsignadoAId = Sol.PersonalId.Value;
                        cabecera.AsignadoANombre = Sol.Personal.NombreCompleto.ToString();
                        if (Sol.PersonalId.Value != 1)
                        {
                            //cabecera.AsignadoAEmail = ObtenerEmailPersonalDesdeId(Sol.PersonalId.Value);
                            cabecera.AsignadoAEmail = Sol.Personal.email;
                        }
                    }


                    if (Sol.SolicitanteId.HasValue)
                    {
                        if (Sol.SolicitanteId.Value != 1)
                        {
                            cabecera.SolicitanteId = Sol.SolicitanteId.Value;

                            cabecera.SolicitanteEmail = Sol.Solicitante.Email;

                            cabecera.UbicacionNombre = Sol.Solicitante.Ubicacion;
                            cabecera.SolicitanteNombre = Sol.Solicitante.NombreCompleto;
                            cabecera.Direccion = Sol.Solicitante.Dirección;
                            cabecera.Telefono = Sol.Solicitante.Telefono;

                            cabecera.EmpresaId = Sol.Solicitante.EmpresaId;
                            //cabecera.EmpresaNombre = Sol.Empresa.Nombre.ToString();
                        }
                    }



                    //if (!string.IsNullOrEmpty(Sol.Obra_Oficina_SVenta))
                    //    cabecera.UbicacionNombre = Sol.Obra_Oficina_SVenta;

                    //if (!string.IsNullOrEmpty(Sol.SolicitanteNombre))
                    //{
                    //    cabecera.SolicitanteNombre = Sol.SolicitanteNombre;
                    //    // PENDIENTE: REVISAR
                    //}

                    //if (!string.IsNullOrEmpty(Sol.Direccion))
                    //    cabecera.Direccion = Sol.Direccion;

                    //if (!string.IsNullOrEmpty(Sol.Telefono))
                    //    cabecera.Telefono = Sol.Telefono;

                    //if (Sol.EmpresaId.HasValue)
                    //{
                    //    cabecera.EmpresaId = Sol.EmpresaId.Value;
                    //    cabecera.EmpresaNombre = Sol.Empresa.Nombre.ToString();
                    //}






                    if (!string.IsNullOrEmpty(Sol.SolicitanteIdRed))
                        cabecera.SolicitanteIdRed = Sol.SolicitanteIdRed;

                    if (Sol.CategoriaSolicitudId.HasValue)
                    {
                        cabecera.CategoriaId = Sol.CategoriaSolicitudId.Value;
                        cabecera.CategoriaNombre = Sol.CategoriaSolicitud.Descr.ToString();
                    }

                    if (Sol.SubCategoriaId.HasValue)
                        if (Sol.SubCategoriaId > 0)
                        {
                            cabecera.SubCategoriaId = Sol.SubCategoriaId.Value;
                            cabecera.SubCategoriaNombre = Sol.SubCategoria.Descr.ToString();
                        }

                    if (Sol.EsIncidente.HasValue)
                        cabecera.Incidente = Convert.ToBoolean(Sol.EsIncidente.Value);

                    if (Sol.Horas.HasValue)
                        cabecera.HorasTotal = Sol.Horas.Value;

                    if (Sol.Referencia.HasValue)
                        cabecera.Referencia = Sol.Referencia.Value;

                    if (Sol.fecha_solicitud.HasValue)
                        cabecera.FechaSolicitada = Sol.fecha_solicitud.Value;

                    if (Sol.fecha_comprometida.HasValue)
                        cabecera.FechaComprometida = Sol.fecha_comprometida.Value;

                    if (Sol.fecha_resolucion.HasValue)
                    {
                        cabecera.FechaResolucion = Sol.fecha_resolucion.Value;
                        cabecera.Intervalo = cabecera.FechaResolucion - cabecera.FechaCreacion;
                        cabecera.Duracion = cabecera.Intervalo.Value.Days.ToString() + " d. " + cabecera.Intervalo.Value.Hours.ToString() + " h.";
                    }
                    else
                    {
                        cabecera.Intervalo = DateTime.Now - cabecera.FechaCreacion;
                        cabecera.Duracion = cabecera.Intervalo.Value.Days.ToString() + " d. " + cabecera.Intervalo.Value.Hours.ToString() + " h.";
                    }

                    if (!string.IsNullOrEmpty(Sol.Sociedad))
                        cabecera.SociedadNombre = Sol.Sociedad;

                    if (!string.IsNullOrEmpty(Sol.CCyP))
                        cabecera.CentroCostoNombre = Sol.CCyP;

                    if (!string.IsNullOrEmpty(Sol.Creacion))
                        cabecera.Creacion = Sol.Creacion;

                    // semaforo
                    string estilo = "";
                    cabecera.Semaforo = estilo + "black";


                    if (!string.IsNullOrEmpty(cabecera.EstadoTipo))
                    {
                        if (cabecera.EstadoTipo.Equals("inicial") && (cabecera.FechaComprometida != null))
                        {
                            if (DateTime.Now > cabecera.FechaComprometida.Value)
                            {
                                cabecera.Semaforo = estilo + "red";
                            }
                            else if ((DateTime.Now < cabecera.FechaComprometida.Value) && (DateTime.Now > cabecera.FechaComprometida.Value.AddDays(-2)))
                            {
                                cabecera.Semaforo = estilo + "yellow";
                            }
                            else if (DateTime.Now < cabecera.FechaComprometida.Value)
                            {
                                cabecera.Semaforo = estilo + "green";
                            }
                        }
                    }

                }
                else
                {
                    cabecera.SolicitudId = 0;
                }

            }

            return cabecera;
        }


        public string ObtenerEmailPersonalDesdeId(int id)                      // obtener email del tecnico desde Id
        {
            string emailtecnico = string.Empty;

            using (SoporteICSA_prdEntities bd_personal = new SoporteICSA_prdEntities())
            {
                // en Personal se busca por email y nickname 
                Personal Per = bd_personal.Personal.Where(e => e.PersonalId == id).FirstOrDefault();

                if ((Per != null) && !Per.PersonalId.Equals(null))
                {
                    emailtecnico = Per.email.ToString();
                }
                else
                {
                }
            }

            return emailtecnico;
        }


        public string ObtenerEmailPersonal(string email_original)        // obtener Email del colaborador desde el nickname
        {
            string[] nn = email_original.ToString().Split('@');
            string nickname = nn[0].Trim();
            string email_definitivo = string.Empty;

            using (SoporteICSA_prdEntities bd_ap = new SoporteICSA_prdEntities())
            {
                // en tabla Colaborador se busca por nickname 
                Personal personal = bd_ap.Personal.Where(e => e.nickname.Equals(nickname)).FirstOrDefault();

                if ((personal != null) && !personal.PersonalId.Equals(null))
                {
                    email_definitivo = personal.email;
                }
                else
                {
                }
            }

            return email_definitivo;
        }



        public int ObtenerIdPersonal(string nickname)                   // obtener Id del tecnico desde el nickname
        {
            int idtecnico = 0;

            using (SoporteICSA_prdEntities bd_personal = new SoporteICSA_prdEntities())
            {
                // en Personal se busca por email y nickname 
                Personal Per = bd_personal.Personal.Where(e => e.nickname.Equals(nickname)).FirstOrDefault();

                if ((Per != null) && !Per.PersonalId.Equals(null))
                {
                    idtecnico = Per.PersonalId;
                }
                else
                {
                }
            }

            return idtecnico;
        }


        public IList<DB_Adm_Tipo_Solicitud> FormarCadenaParaGraficoTorta(string sp)
        {
            using (SoporteICSA_prdEntities bd_db_adm = new SoporteICSA_prdEntities())
            {
                return bd_db_adm.Database.SqlQuery<DB_Adm_Tipo_Solicitud>(sp).ToList();

                //if (result.Count() > 0)
                //{
                //    resultado = "[";
                //    foreach (var ts in result)
                //    {
                //        resultado = resultado + '{' + '"' + titulo + '"' + ':' + '"' + ts.TipoSolicitudNombre + '"' + ',' + '"' + valor + '"' + ':' + ts.Cantidad + "},";
                //    }
                //    resultado = resultado.TrimEnd(',') + "]";

                //    var string1 = "&quot; ";

                //    resultado = resultado.Replace("\\", "");

                //    resultado = resultado + " " ;
                //}
                //else
                //{
                //}
            }

            // falta manejo de error

            //return resultado;
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CargarDBAdmTipoSolicitud()
        {
            //string aux_octs = FormarCadenaParaGraficoTorta("SP_DB_Obtener_Tipo_Solicitud", "Tipo", "Cant.");

            //return Json(aux_octs, JsonRequestBehavior.AllowGet);

            var modelList = FormarCadenaParaGraficoTorta("SP_DB_Obtener_Tipo_Solicitud");

            var modelData = modelList.Select(m => new SelectListItem()
            {
                Text = m.TipoSolicitudNombre,
                Value = m.Cantidad.ToString()
            });

            return Json(modelData, JsonRequestBehavior.AllowGet);

        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CargarDBCategoria()
        {
            using (SoporteICSA_prdEntities bd_sss = new SoporteICSA_prdEntities())
            {
                var modelList = bd_sss.Database.SqlQuery<DB_Categoria>("SP_DB_Obtener_Categoria").ToList();

                var modelData = modelList.Select(m => new SelectListItem()
                {
                    Text = m.CategoriaNombre,
                    Value = m.Cantidad.ToString()
                });

                return Json(modelData, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CargarDBEstadoSolicitudPorResponsable()
        {
            using (SoporteICSA_prdEntities bd_sss = new SoporteICSA_prdEntities())
            {
                var modelList = bd_sss.Database.SqlQuery<DB_Estado_Solicitud_por_Responsable>("SP_DB_Obtener_Estado_Solicitud_por_Responsable").ToList();

                var modelData = modelList.Select(m => new
                {
                    NickName = m.NickName,
                    Resuelta = m.Resuelta,
                    ResueltaConConfirmacion = m.ResueltaConConfirmacion,
                    NoCorrespondeATareaDeSistemas = m.NoCorrespondeATareaDeSistemas,
                    CierreHistorico = m.CierreHistorico,
                    EsperandoRespuestaDeUsuario = m.EsperandoRespuestaDeUsuario,
                    Escalada = m.Escalada,
                    Asignada = m.Asignada,
                    EnProceso = m.EnProceso,
                    Nueva = m.Nueva,
                    Agendada = m.Agendada
                });
                return Json(modelData, JsonRequestBehavior.AllowGet);
            }
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult CargarDBSolicitudesPendientesAtrasadasPorResponsable()
        {
            using (SoporteICSA_prdEntities bd_sss = new SoporteICSA_prdEntities())
            {
                var modelList = bd_sss.Database.SqlQuery<DB_Solicitudes_Pendientes_Atrasadas_por_Responsable>("SP_DB_Obtener_Solicitudes_Pendientes_Atrasadas_por_Responsable").ToList();

                var modelData = modelList.Select(m => new
                {
                    NickName = m.NickName,
                    NombreCompleto = m.NombreCompleto,
                    Pendientes = m.Pendientes,
                    Atrasadas = m.Atrasadas
                });
                return Json(modelData, JsonRequestBehavior.AllowGet);
            }
        }


        public Email ConstruirEmailFormularioContacto(UsuarioSSS usuario)
        {
            Email efc = new Email()
            {
                emailfuente = usuario.email,
                emaildestinatario = "operacionesti@socovesa.cl",
                asunto = "Solicitud de Ayuda App. Soporte Sistemas",
                mensaje = "",
                nombrecompleto = usuario.nombrecompleto,
                cargo = usuario.cargo
            };

            return efc;
        }


        public LogSSS IniciarLogSSS()
        {
            string aux_cab = string.Empty;

            IList<Minutos> construirListaMinutos = new List<Minutos>{
                            new Minutos() {  MinutosNombre = "5 minutos", MinutosCantidad = 5 } ,
                            new Minutos() {  MinutosNombre = "10 minutos", MinutosCantidad = 10 } ,
                            new Minutos() {  MinutosNombre = "15 minutos", MinutosCantidad = 15 } ,
                            new Minutos() {  MinutosNombre = "30 minutos", MinutosCantidad = 30 } ,
                            new Minutos() {  MinutosNombre = "45 minutos", MinutosCantidad = 45 }
                        };

            LogSSS iniciar_log = new LogSSS()
            {
                LogId = 0,
                LogSolicitudId = 0,
                LogEmail = false,
                LogCerrarSolicitud = false,
                LogAccionId = 0,
                LogTipoId = 1,
                LogUsuarioIdRed = aux_cab,
                //LogUsuarioId = 1,
                LogFecha = DateTime.Now,
                LogDescripcion = aux_cab,
                LogHora = 0,
                LogListaMinutos = construirListaMinutos,
                LogPersonalId = 1
            };

            return iniciar_log;
        }


        public Log ConstruirLog(LogSSS clog)                            // del modelo a la BD
        {
            string aux = string.Empty;

            Log log_inicial = new Log()
            {
                Id = 0,
                SolicitudId = 0,
                Email = false,
                //Accion = aux,
                AccionId = 0,
                TipoId = 1,
                Usuario = aux,
                //UsuarioId = 1,
                Fecha = DateTime.Now,
                Descripcion = aux,
                Horas = 0,
                PersonalId = 1
            };

            // campos automáticos

            log_inicial.SolicitudId = clog.LogSolicitudId;

            if (clog.LogEmail)
                log_inicial.Email = clog.LogEmail;

            if (clog.LogFecha.HasValue)
                log_inicial.Fecha = clog.LogFecha.Value;

            using (SoporteICSA_prdEntities bd_log = new SoporteICSA_prdEntities())
            {
                if (clog.LogTipoId.HasValue)
                {
                    log_inicial.TipoId = clog.LogTipoId;

                    log_inicial.TipoLog = bd_log.TipoLog.Where(tl => tl.Id == clog.LogTipoId).Single();
                }

                //sol.EstadoSolicitudId = cab.EstadoId;
                //sol.EstadoSolicitud = bd_cs.EstadoSolicituds.Where(est => est.EstadoSolicitudId == cab.EstadoId).Single();

                //if (cab.EmpresaId.HasValue)
                //{
                //    sol.EmpresaId = cab.EmpresaId;
                //    //sol.Empresa = bd_cs.Empresas.Where(em => em.EmpresaId == cab.EmpresaId.Value).FirstOrDefault();
                //    sol.Empresa = bd_cs.Empresas.Where(em => em.EmpresaId == cab.EmpresaId.Value).Single();
                //}
            }


            // campos del formulario
            //log_inicial.Descripcion = clog.LogDescripcion;

            if (!string.IsNullOrEmpty(clog.LogDescripcion))
            {
                if (clog.LogDescripcion.Length > 495)
                {
                    log_inicial.Descripcion = clog.LogDescripcion.Substring(0, 495) + "...";
                }
                else
                {
                    log_inicial.Descripcion = clog.LogDescripcion;
                }
            }



            if (clog.LogHora.HasValue)
                log_inicial.Horas = clog.LogHora.Value;

            if (clog.LogAccionId.HasValue)
                log_inicial.AccionId = clog.LogAccionId.Value;

            if (!string.IsNullOrEmpty(clog.LogUsuarioIdRed))
                log_inicial.Usuario = clog.LogUsuarioIdRed;

            //if (clog.LogUsuarioId.HasValue)
            //    log_inicial.UsuarioId = clog.LogUsuarioId.Value;

            if (clog.LogPersonalId.HasValue)
                log_inicial.PersonalId = clog.LogPersonalId.Value;

            if (!string.IsNullOrEmpty(clog.LogTecnicoEmail))
                log_inicial.Tecnico = clog.LogTecnicoEmail;

            if (!string.IsNullOrEmpty(clog.LogResultadoEnvioEmail))
            {
                int largo_mensaje_email = clog.LogResultadoEnvioEmail.Length - 2;
                log_inicial.EnvioEmail = clog.LogResultadoEnvioEmail.Substring(0, largo_mensaje_email);
            }

            return log_inicial;
        }


        public ActionResult CrearLogSolicitudSoporteSistemas_Manual(LogSSS logunicoeneluniverso)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string messages_errors = "Error al Crear Log 'Manual': ";
            string sh = string.Empty, rem = string.Empty, NombreTipoLog = string.Empty;

            if (!logunicoeneluniverso.LogAccionId.HasValue)
                logunicoeneluniverso.LogAccionId = 2;

            if (ModelState.IsValid)
            {

                using (SoporteICSA_prdEntities bd_log = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        Log AuxiliarLog = new Log();
                        AuxiliarLog = ConstruirLog(logunicoeneluniverso);

                        if (AuxiliarLog.TipoLog != null)
                            NombreTipoLog = AuxiliarLog.TipoLog.NombreTL;

                        AuxiliarLog.TipoLog = null;


                        // 1.- Enviar EMAIL
                        // 2.- Crear LOG


                        // 1.- Enviar EMAIL

                        if (logunicoeneluniverso.LogEmail == true)
                        {
                            CabeceraSSS sol_aux = ObtenerCabecera(logunicoeneluniverso.LogSolicitudId);

                            //sol_aux.Descripcion = logunicoeneluniverso.LogDescripcion;
                            sol_aux.Descripcion = sol_aux.Descripcion + " <br />" + "Evento: " + logunicoeneluniverso.LogDescripcion;
                            sol_aux.EstadoNombre = NombreTipoLog;

                            rem = EnviarEmailSSSAutomatico(sol_aux);
                        }
                        else
                        {
                            rem = "Sin envío de email   ";
                        }

                        if (!string.IsNullOrEmpty(rem))
                        {
                            if (rem.Length > 495)
                            {
                                logunicoeneluniverso.LogResultadoEnvioEmail = rem.Substring(0, 495) + "...";
                                AuxiliarLog.EnvioEmail = rem.Substring(0, 495) + "...";
                            }
                            else
                            {
                                logunicoeneluniverso.LogResultadoEnvioEmail = rem;
                                AuxiliarLog.EnvioEmail = rem;
                            }
                        }

                        // PENDIENTE: ¿que hacer si NO envia Email?


                        // 2.- Crear Log

                        bd_log.Log.Add(AuxiliarLog);
                        int resultado = bd_log.SaveChanges();

                        if (resultado > 0)
                        {
                            // Sumar horas del log a la solicitud
                            //Solicitud auxSSS = new Solicitud(){ SolicitudId = logunicoeneluniverso.LogSolicitudId, Horas = logunicoeneluniverso.LogHoraSolicitud.Value + logunicoeneluniverso.LogHora.Value };
                            if (logunicoeneluniverso.LogHora.Value > 0)
                                sh = SumarHorasSSS(logunicoeneluniverso.LogSolicitudId, logunicoeneluniverso.LogHora.Value);

                            // PENDIENTE: ¿que hacer si NO suma correctamente las horas? (Variable sh distinta a 'ok')

                            TempData["exito"] = "Log " + AuxiliarLog.Id.ToString() + " creado";
                            return RedirectToAction("ModificarSSS", "SS", new { @id = AuxiliarLog.SolicitudId });
                        }
                        else
                        {
                            TempData["error"] = messages_errors + "No fue creado el registro";
                            //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                            return RedirectToAction("ErrorSSS", "SS");
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            }
                        }

                        TempData["error"] = messages_errors;
                        //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                        return RedirectToAction("ErrorSSS", "SS");
                    }
                }
            }
            else
            {
                TempData["error"] = messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
                //TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                return RedirectToAction("ErrorSSS", "SS");
            }
        }


        public ActionResult CrearLogSolicitudSoporteSistemas_y_CerrarSSS(LogSSS logunicoeneluniverso)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string messages_errors = "Error al Crear Log y Cerrar SSS: ";
            string sh = string.Empty, ce = string.Empty, rem = string.Empty, NombreTipoLog = string.Empty;

            if (!logunicoeneluniverso.LogAccionId.HasValue)
                logunicoeneluniverso.LogAccionId = 2;

            logunicoeneluniverso.LogTipoId = 8;         // Tipo 8 = Cierre

            if (ModelState.IsValid)
            {
                using (SoporteICSA_prdEntities bd_log = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        Log AuxiliarLog = new Log();
                        AuxiliarLog = ConstruirLog(logunicoeneluniverso);

                        if (AuxiliarLog.TipoLog != null)
                            NombreTipoLog = AuxiliarLog.TipoLog.NombreTL;

                        AuxiliarLog.TipoLog = null;


                        // 1.- Enviar EMAIL
                        // 2.- Crear LOG


                        // 1.- Enviar EMAIL

                        CabeceraSSS sol_aux = ObtenerCabecera(logunicoeneluniverso.LogSolicitudId);

                        //sol_aux.Descripcion = logunicoeneluniverso.LogDescripcion;
                        sol_aux.Descripcion = sol_aux.Descripcion + " <br />" + "Evento: " + logunicoeneluniverso.LogDescripcion;
                        sol_aux.EstadoNombre = NombreTipoLog;

                        rem = EnviarEmailSSSAutomatico(sol_aux);

                        if (!string.IsNullOrEmpty(rem))
                        {
                            if (rem.Length > 495)
                            {
                                logunicoeneluniverso.LogResultadoEnvioEmail = rem.Substring(0, 495) + "...";
                                AuxiliarLog.EnvioEmail = rem.Substring(0, 495) + "...";
                            }
                            else
                            {
                                logunicoeneluniverso.LogResultadoEnvioEmail = rem;
                                AuxiliarLog.EnvioEmail = rem;
                            }
                        }

                        AuxiliarLog.Email = true;

                        // PENDIENTE: ¿que hacer si NO envia Email?


                        // 2.- Crear Log

                        bd_log.Log.Add(AuxiliarLog);
                        int resultado = bd_log.SaveChanges();

                        if (resultado > 0)
                        {
                            // Sumar horas del log a la solicitud
                            //Solicitud auxSSS = new Solicitud(){ SolicitudId = logunicoeneluniverso.LogSolicitudId, Horas = logunicoeneluniverso.LogHoraSolicitud.Value + logunicoeneluniverso.LogHora.Value };
                            if (logunicoeneluniverso.LogHora.Value > 0)
                                sh = SumarHorasSSS(logunicoeneluniverso.LogSolicitudId, logunicoeneluniverso.LogHora.Value);
                            // PENDIENTE: ¿que hacer si NO suma correctamente las horas? (Variable sh distinta a 'ok')

                            // Cerrar solicitud con estado Resuelta
                            ce = CambiarEstadoSSS(logunicoeneluniverso.LogSolicitudId, 3);          // Estado 3 = resuelta

                            TempData["exito"] = "Log " + AuxiliarLog.Id.ToString() + " creado";
                            //return RedirectToAction("ModificarSSS", "SS", new { @id = AuxiliarLog.SolicitudId });
                            //return RedirectToAction("MisSSSPendientes", "SS", new { @idred = "SOCOVESA\\" + @System.Environment.UserName });
                            return RedirectToAction("MisSSSPendientes", "SS", new { @idred = System.Security.Principal.WindowsIdentity.GetCurrent().Name });
                        }
                        else
                        {
                            TempData["error"] = messages_errors + "No fue creado el registro";
                            //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                            return RedirectToAction("ErrorSSS", "SS");
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            }
                        }

                        TempData["error"] = messages_errors;
                        //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                        return RedirectToAction("ErrorSSS", "SS");
                    }
                }
            }
            else
            {
                TempData["error"] = messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
                //TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                return RedirectToAction("ErrorSSS", "SS");
            }
        }


        public List<LogSSS> ObtenerLog(int idsolicitud)                 // de Log (BD) a LogSSS (MVC)
        {
            string aux = string.Empty;
            List<LogSSS> ListaLogSSS = new List<LogSSS>();
            LogSSS logUnitario;

            using (SoporteICSA_prdEntities bd_log = new SoporteICSA_prdEntities())
            {
                List<Log> ListaLogBD = bd_log.Log.Where(e => e.SolicitudId == idsolicitud).OrderByDescending(f => f.Fecha).ToList();

                if (ListaLogBD.Count() > 0)
                {
                    foreach (var itemlog in ListaLogBD)
                    {
                        logUnitario = new LogSSS();

                        if (itemlog.Email.HasValue)
                            logUnitario.LogEmail = itemlog.Email.Value;

                        if (itemlog.AccionId.HasValue)
                            logUnitario.LogAccionId = itemlog.AccionId.Value;

                        if (!string.IsNullOrEmpty(itemlog.AccionLog.Accion))
                            logUnitario.LogAccionNombre = itemlog.AccionLog.Accion;

                        if (itemlog.TipoId.HasValue)
                            logUnitario.LogTipoId = itemlog.TipoId.Value;

                        if (!string.IsNullOrEmpty(itemlog.TipoLog.NombreTL))
                            logUnitario.LogTipoIdNombre = itemlog.TipoLog.NombreTL;

                        if (!string.IsNullOrEmpty(itemlog.Usuario))
                            logUnitario.LogUsuarioIdRed = itemlog.Usuario;

                        if (itemlog.UsuarioId.HasValue)
                            logUnitario.LogUsuarioId = itemlog.UsuarioId.Value;

                        logUnitario.LogFecha = itemlog.Fecha;

                        if (!string.IsNullOrEmpty(itemlog.Descripcion))
                            logUnitario.LogDescripcion = itemlog.Descripcion;

                        if (itemlog.Horas.HasValue)
                            logUnitario.LogHora = itemlog.Horas.Value;

                        if (itemlog.PersonalId.HasValue)
                            logUnitario.LogPersonalId = itemlog.PersonalId.Value;

                        if (!string.IsNullOrEmpty(itemlog.Tecnico))
                            logUnitario.LogTecnicoEmail = itemlog.Tecnico;

                        ListaLogSSS.Add(new LogSSS()
                        {
                            LogSolicitudId = idsolicitud,
                            LogEmail = logUnitario.LogEmail,
                            LogAccionId = logUnitario.LogAccionId,
                            LogAccionNombre = logUnitario.LogAccionNombre,
                            LogTipoId = logUnitario.LogTipoId,
                            LogTipoIdNombre = logUnitario.LogTipoIdNombre,
                            LogUsuarioIdRed = logUnitario.LogUsuarioIdRed,
                            LogUsuarioId = logUnitario.LogUsuarioId,
                            LogFecha = logUnitario.LogFecha,
                            LogDescripcion = logUnitario.LogDescripcion,
                            LogHora = logUnitario.LogHora,
                            LogTecnicoEmail = logUnitario.LogTecnicoEmail,
                            LogPersonalId = logUnitario.LogPersonalId
                        });
                    }
                }
                else
                {
                    // solicitud no tiene logs
                }
            }

            return ListaLogSSS;
        }


        public string CrearLogSolicitudSoporteSistemas_Automatico(LogSSS logunicoeneluniverso)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string messages_errors = "Error al Crear Log 'Automático': ", NombreTipoLog = string.Empty;

            if (!logunicoeneluniverso.LogAccionId.HasValue)
                logunicoeneluniverso.LogAccionId = 1;

            if (ModelState.IsValid)
            {
                Log AuxiliarLog = new Log();
                AuxiliarLog = ConstruirLog(logunicoeneluniverso);

                if (AuxiliarLog.TipoLog != null)
                    NombreTipoLog = AuxiliarLog.TipoLog.NombreTL;

                AuxiliarLog.TipoLog = null;


                using (SoporteICSA_prdEntities bd_log = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        bd_log.Log.Add(AuxiliarLog);
                        int resultado = bd_log.SaveChanges();

                        if (resultado > 0)
                        {
                            //TempData["exito"] = "Log " + AuxiliarLog.Id.ToString() + " creado";
                            return "ok";
                        }
                        else
                        {
                            return messages_errors + "No fue creado el registro";
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            }
                        }

                        return messages_errors;
                    }
                }
            }
            else
            {
                return messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
            }
        }


        public string SumarHorasSSS(int idsolicitud, decimal hh)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string messages_errors = "Error al Sumar Horas de SSS: ";

            if (ModelState.IsValid)
            {
                //Solicitud auxiliarSSS = new Solicitud();
                //auxiliarSSS = ConstruirSolicitud(objetounicoeneluniverso);

                //Solicitud auxSSS = new Solicitud()
                //{
                //    SolicitudId = idsol,
                //    Horas = hh
                //};

                using (SoporteICSA_prdEntities bd_sm = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        Solicitud sol = bd_sm.Solicitud.Where(a => a.SolicitudId == idsolicitud).FirstOrDefault();

                        if (sol.Horas.HasValue)
                            sol.Horas = sol.Horas.Value + hh;
                        else
                            sol.Horas = 0 + hh;

                        bd_sm.Solicitud.Attach(sol);
                        bd_sm.Entry(sol).State = System.Data.Entity.EntityState.Modified;

                        int resultado = bd_sm.SaveChanges();

                        if (resultado > 0)
                        {
                            return "ok";
                        }
                        else
                        {
                            return messages_errors + "No se sumaron las horas";
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            }
                        }

                        return messages_errors;
                    }
                }
            }
            else
            {
                return messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
            }
        }


        public List<PersonalRol> ObtenerRolUsuarioConIdPersonal(int idpersonal)
        {
            List<PersonalRol> personal_rol = new List<PersonalRol>();
            string error = string.Empty;

            try
            {
                using (SoporteICSA_prdEntities bd_rol = new SoporteICSA_prdEntities())
                {
                    // en tabla PersonalRol se busca por PersonalId
                    personal_rol = bd_rol.PersonalRol.Where(e => e.PersonalId.Equals(idpersonal)).ToList();

                    if (personal_rol.Count > 0)
                    {
                        // tiene algún rol
                    }
                    else
                    {
                        // No tiene roles
                    }
                }
            }
            catch (Exception er)
            {
            }

            return personal_rol;
        }


        public string CambiarEstadoSSS(int idsolicitud, int estadoFinal)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string messages_errors = "Error al Cambiar el Estado de la SSS: ";

            if (ModelState.IsValid)
            {
                //Solicitud auxiliarSSS = new Solicitud();
                //auxiliarSSS = ConstruirSolicitud(objetounicoeneluniverso);

                //Solicitud auxSSS = new Solicitud()
                //{
                //    SolicitudId = idsol,
                //    Horas = hh
                //};

                using (SoporteICSA_prdEntities bd_ce = new SoporteICSA_prdEntities())
                {
                    try
                    {
                        Solicitud sol = bd_ce.Solicitud.Where(a => a.SolicitudId == idsolicitud).FirstOrDefault();

                        if (estadoFinal > 0)
                        {
                            sol.EstadoSolicitudId = estadoFinal;
                            sol.EstadoSolicitud = bd_ce.EstadoSolicitud.Where(e => e.EstadoSolicitudId == estadoFinal).Single();

                            if (sol.EstadoSolicitud.tipo.Equals("final"))
                            {
                                sol.fecha_resolucion = DateTime.Now;
                            }
                            else
                            {
                                sol.fecha_resolucion = null;
                            }
                        }

                        bd_ce.Solicitud.Attach(sol);
                        bd_ce.Entry(sol).State = System.Data.Entity.EntityState.Modified;

                        int resultado = bd_ce.SaveChanges();

                        if (resultado > 0)
                        {
                            return "ok";
                        }
                        else
                        {
                            return messages_errors + "No fue posible actualizar el estado de la SSS. ";
                        }
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                messages_errors = messages_errors + string.Format("Error '{0}' occurrió en {1} a {2}" + "\r\n",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            }
                        }

                        return messages_errors;
                    }
                }
            }
            else
            {
                return messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
            }
        }


        public List<DocumentoSSS> ObtenerDocumento(int idsolicitud)
        {
            List<DocumentoSSS> listaDocumentos = new List<DocumentoSSS>
            {
                new DocumentoSSS(){ DocumentoSolicitudId = 1, DocumentoDescripcion = "Diagrama", DocumentoNombre = "diagramaap.bpm", DocumentoTipo = "Bizagi", DocumentoFecha = DateTime.Today, DocumentoUsuario = "vsandoval"},
                new DocumentoSSS(){ DocumentoSolicitudId = 1, DocumentoDescripcion = "Especificación de Requerimientos", DocumentoNombre = "requerimiento.doc", DocumentoTipo = "Word", DocumentoFecha = DateTime.Today, DocumentoUsuario = "vsandoval"}
            };

            return listaDocumentos;
        }


        public Email ConstruirEmailAutomático(CabeceraSSS sol)
        {
            Email ea = new Email();

            ea.solicitudid = sol.SolicitudId;

            if (sol.Incidente)
                ea.solicitudtipo = "Incidente";
            else
                ea.solicitudtipo = "Requerimiento";

            ea.solicituddescripcion = sol.Descripcion;
            ea.solicitudestado = sol.EstadoNombre;

            ea.solicitantenombre = sol.SolicitanteNombre;
            ea.solicitanteubicacion = sol.UbicacionNombre;
            ea.solicitanteemail = sol.SolicitanteEmail;
            ea.solicitantetelefono = sol.Telefono;

            if (!string.IsNullOrEmpty(sol.AsignadoAEmail))
            {
                ea.tecnicoemail = sol.AsignadoAEmail;
                ea.tecniconombre = sol.AsignadoANombre;
            }
            else
            {
                ea.tecniconombre = "Sin Asignar";
                //    ea.tecnicoemail = "operacionesti@socovesa.cl";
            }



            return ea;
        }


        public string EnviarEmailAutomático(Email email)
        {
            string resultado = string.Empty;
            string emailtecnico = string.Empty, emailsolicitante = string.Empty;

            string from_pes = "operacionesti@socovesa.cl";

            // 1.- Construir mensaje
            // 2.- Identificar cliente de correo
            // 3.- Mostrar por pantalla si fue exitoso o no


            /* 1.- -------------------------MENSAJE DE CORREO---------------------- */

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mensaje = new System.Net.Mail.MailMessage();

            // Correo electronico desde la que enviamos el mensaje
            mensaje.From = new System.Net.Mail.MailAddress(from_pes, "TI Socovesa");

            //Direccion de correo electronico a la que queremos enviar el mensaje
            if (!string.IsNullOrEmpty(email.solicitanteemail))
            {
                mensaje.To.Add(email.solicitanteemail);
                emailsolicitante = " (" + email.solicitanteemail + ")";
            }
            else
            {
            }

            if (!string.IsNullOrEmpty(email.tecnicoemail))
            {
                mensaje.To.Add(email.tecnicoemail);
                emailtecnico = " (" + email.tecnicoemail + ")";
            }
            else
            {
            }

            //mensaje.To.Add("victorssandovaln@gmail.com");
            mensaje.CC.Add("operacionesti@socovesa.cl");
            mensaje.Bcc.Add("vsandoval@socovesa.cl");
            //mensaje.Bcc.Add("jtapia@socovesa.cl");
            //mensaje.Bcc.Add("sibarra@socovesa.cl");

            // Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario
            mensaje.Subject = "Solicitud Soporte Sistemas nro. " + email.solicitudid.ToString() + " (" + email.solicitudtipo + ")";
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;

            // Cuerpo del Correo.          

            var message = new StringBuilder();
            message.Append("Técnico           : " + email.tecniconombre + emailtecnico + " <br />");
            message.Append("<hr width = '60%' align='left'>");
            message.Append("Número solicitud  : " + email.solicitudid.ToString() + " <br />");
            message.Append("Tipo              : " + email.solicitudtipo + " <br />");
            message.Append("Estado            : " + email.solicitudestado + " <br />");
            message.Append("                                                  <br />");
            message.Append("Usuario           : " + email.solicitantenombre + emailsolicitante + " <br />");
            message.Append("Número contacto   : " + email.solicitantetelefono + " <br />");
            //message.Append("Evento: " + email.solicituddescripcion + " <br />");
            message.Append("                                                  <br />");
            message.Append("Descripción: " + email.solicituddescripcion + " <br />");
            message.Append("<hr width = '60%' align='left'>");

            //message.Append("Ubicación: " + email.solicitanteubicacion + " <br />");
            //message.Append("Teléfono: " + email.solicitantetelefono + " <br />");
            //message.Append("<hr width = '75%' align='left'>");

            // Firma del email

            message.Append("                                                  <br />");
            message.Append("<b>Mesa de Ayuda Socovesa</b> <br />");
            message.Append("                                                  <br />");
            message.Append("Gerencia de Sistemas | Eliodoro Yañez 2962, Providencia, Santiago <br />");
            message.Append("Email: mesadeayuda@socovesa.cl | Teléfono: 22 840 7700 <br />");
            message.Append("                                                  <br />");
            //message.Append("<img src = '~/Content/Images/LogoEmpresasSocovesa.jpg' />");

            mensaje.Body = message.ToString();


            // Incoporar imagen final al email automático

            string htmlMessage = @"<html><body>" + message.ToString() + "<img src='cid:EmbeddedContent_1' width='425' height='45' /></body></html>";

            //Create two views, one text, one HTML.
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(htmlMessage, Encoding.UTF8, MediaTypeNames.Text.Html);
            // Create a plain text message for client that don't support HTML
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(Regex.Replace(htmlMessage, "<[^>]+?>", string.Empty), Encoding.UTF8, MediaTypeNames.Text.Plain);

            string mediaType = MediaTypeNames.Image.Jpeg;

            //LinkedResource img = new LinkedResource(@"C:\LogoEmpresasSocovesa.jpg", mediaType);
            LinkedResource img = new LinkedResource(Server.MapPath("~/Content/Images/LogoEmpresasSocovesa.jpg"), mediaType);
            // Make sure you set all these values!!!
            img.ContentId = "EmbeddedContent_1";
            img.ContentType.MediaType = mediaType;
            img.TransferEncoding = TransferEncoding.Base64;
            img.ContentType.Name = img.ContentId;
            img.ContentLink = new Uri("cid:" + img.ContentId);
            htmlView.LinkedResources.Add(img);

            //////////////////////////////////////////////////////////////
            mensaje.AlternateViews.Add(plainView);
            mensaje.AlternateViews.Add(htmlView);



            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;

            // Seteo que el server notifique solamente en el error de entrega
            // Delivery notifications
            mensaje.DeliveryNotificationOptions =
              DeliveryNotificationOptions.OnFailure |
              DeliveryNotificationOptions.OnSuccess |
              DeliveryNotificationOptions.Delay;

            // Ask for a read receipt
            mensaje.Headers.Add("Reply-To", from_pes);


            /* 2.- -------------------------CLIENTE DE CORREO----------------------*/

            // Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient("internal-relay.socovesa.cl", 25);

            // Hay que crear las credenciales del correo emisor
            //cliente.UseDefaultCredentials = true;
            cliente.Credentials = new System.Net.NetworkCredential(from_pes, "password");

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail                
            //cliente.Port = 587;
            //cliente.EnableSsl = true;
            //cliente.Host = "smtp.gmail.com";


            /* 3.- -------------------------EJECUTAR ENVIO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mensaje);

                //TempData["exito"] = "Email enviado! Nos pondremos en contacto con ud. a la brevedad.";
                //return RedirectToAction("ExitoSSS", "SS");
                return "Email enviado   ";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                //Log("No se envia el correo " + Lista[i].Correo + ".");

                //TempData["error"] = "No fue enviado el email!" + ex.ToString();
                //return RedirectToAction("ErrorSSS", "SS");
                return "Email NO enviado: " + ex.ToString() + "   ";
            }
        }


        public string EnviarEmailSSSAutomatico(CabeceraSSS solicitud)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string messages_errors = "Error al Enviar Email 'Automático': ";

            Email cea = new Email();
            string eea = string.Empty;

            if (ModelState.IsValid)
            {
                cea = ConstruirEmailAutomático(solicitud);

                eea = EnviarEmailAutomático(cea);

                return eea;
            }
            else
            {
                return messages_errors + string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
            }
        }



        public string DB_Obtener_Solicitantes()
        {
            string Cuentas = string.Empty;
            List<UsuarioSSS> solicitantes = new List<UsuarioSSS>();

            try
            {
                using (SoporteICSA_prdEntities bd_sss = new SoporteICSA_prdEntities())
                {
                    solicitantes = bd_sss.Database.SqlQuery<UsuarioSSS>("SP_DB_Obtener_Solicitantes").ToList();
                }

                if (solicitantes.Count > 0)
                {
                    foreach (UsuarioSSS sol in solicitantes)
                    {
                        //Cuentas = Cuentas + "'" + dr["EMAIL"].ToString() + "', ";
                        Cuentas = Cuentas + "{" +
                            "'nickname':'" + sol.nickname + "'" + "," +

                            "'cargo':'" + sol.cargo + "'" + "," +
                            "'direccion':'" + sol.direccion + "'" + "," +
                            "'empresaid':'" + sol.empresaid + "'" + "," +
                            "'telefono':'" + sol.telefono + "'" + "," +
                            "'ubicacion':'" + sol.ubicacion + "'" + "," +

                            "'email':'" + sol.email + "'" + "," +
                            "'nombrecompleto':'" + sol.nombrecompleto + "'" +
                            "}, ";
                    }

                    Cuentas = Cuentas.TrimEnd(' ');
                    Cuentas = Cuentas.TrimEnd(',');
                }
                else
                {
                    Cuentas = " .- No se generó listado de solicitantes desde SP_DB_Obtener_Solicitantes -. ";
                }

            }
            catch (Exception er)
            {
                Cuentas = " .- ERROR al obtener solicitantes desde SP_DB_Obtener_Solicitantes: " + er.Message + "-. ";
            }

            return Cuentas;
        }












        // *****************************************************************************************************************
        // ********************************************************************************************************** ACTION
        // *****************************************************************************************************************


        [HttpGet]
        public ActionResult CrearSSS()
        {
            ViewBag.Current = "CrearSSS";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                CabeceraSSS crap = IniciarCabecera();

                IniciarViewbag();

                return View(crap);
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CrearSSS(CabeceraSSS cabecera_corta)
        {
            List<Modificacion> lista_crea_sol = new List<Modificacion>();

            //return CrearSolicitudSoporteSistemas(cabecera_corta, "Rápida");
            return CrearModificarSolicitudSoporteSistemas(cabecera_corta, 1, "rápida", lista_crea_sol);


            //string messages_errors = string.Empty;

            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            //if (ModelState.IsValid)
            //{
            //    Solicitud solicitudCrearSSS = new Solicitud();

            //    solicitudCrearSSS = ConstruirSolicitud(objetounicoentodoeluniverso);
            //    solicitudCrearSSS.Creacion = "Rapida";

            //    using (SoporteICSA_prdEntities bd = new SoporteICSA_prdEntities())
            //    {

            //        try {
            //            bd.Solicituds.Add(solicitudCrearSSS);
            //            int resultado = bd.SaveChanges();

            //            if (resultado > 0)
            //            {
            //                messages_errors = "Solicitud " + solicitudCrearSSS.Creacion + " " + solicitudCrearSSS.SolicitudId.ToString() + " creada";
            //                return RedirectToAction("ModificarSSS", "SS", new { @id = solicitudCrearSSS.SolicitudId });
            //            }
            //            else
            //            {
            //                messages_errors = "No fue creada la Solicitud";
            //                return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
            //            }

            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
            //            {
            //                // Get entry

            //                DbEntityEntry entry = item.Entry;
            //                string entityTypeName = entry.Entity.GetType().Name;

            //                // Display or log error messages

            //                foreach (DbValidationError subItem in item.ValidationErrors)
            //                {
            //                    messages_errors = messages_errors + string.Format("Error '{0}' occurred in {1} at {2}" + "\r\n",
            //                             subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
            //                    //Console.WriteLine(message);
            //                }
            //            }

            //            return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
            //        }
            //    }
            //}
            //else
            //{
            //    messages_errors = string.Join("; ", ModelState.Values
            //                        .SelectMany(x => x.Errors)
            //                        .Select(x => x.ErrorMessage));

            //    return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
            //}

        }


        [HttpGet]
        public ActionResult CrearSSSComp()
        {
            ViewBag.Current = "CrearSSSComp";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                CabeceraSSS ccomp = IniciarCabecera();

                IniciarViewbag();

                return View(ccomp);
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CrearSSSComp(CabeceraSSS cabecera_completa)
        {
            List<Modificacion> lista_crea_sol = new List<Modificacion>();

            //return CrearSolicitudSoporteSistemas(cabecera_completa, "Completa");
            return CrearModificarSolicitudSoporteSistemas(cabecera_completa, 1, "completa", lista_crea_sol);

            //string messages_errors = string.Empty;

            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            //if (ModelState.IsValid)
            //{
            //    Solicitud solicitudCrearSSSComp = new Solicitud();

            //    solicitudCrearSSSComp = ConstruirSolicitud(cabecera_completa);
            //    solicitudCrearSSSComp.Creacion = "Completa";

            //    using (SoporteICSA_prdEntities bd = new SoporteICSA_prdEntities())
            //    {

            //        try
            //        {
            //            bd.Solicituds.Add(solicitudCrearSSSComp);
            //            int resultado = bd.SaveChanges();

            //            if (resultado > 0)
            //            {
            //                messages_errors = "Solicitud " + solicitudCrearSSSComp.Creacion + " " + solicitudCrearSSSComp.SolicitudId.ToString() + " creada";
            //                TempData["exito"] = messages_errors;
            //                return RedirectToAction("ModificarSSS", "SS", new { @id = solicitudCrearSSSComp.SolicitudId });
            //            }
            //            else
            //            {
            //                messages_errors = "No fue creada la Solicitud";
            //                TempData["error"] = messages_errors;
            //                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
            //                return RedirectToAction("ErrorSSS", "SS");
            //            }

            //        }
            //        catch (DbEntityValidationException ex)
            //        {
            //            foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
            //            {
            //                // Get entry

            //                DbEntityEntry entry = item.Entry;
            //                string entityTypeName = entry.Entity.GetType().Name;

            //                // Display or log error messages

            //                foreach (DbValidationError subItem in item.ValidationErrors)
            //                {
            //                    messages_errors = messages_errors + string.Format("Error '{0}' occurred in {1} at {2}" + "\r\n",
            //                             subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
            //                    //Console.WriteLine(message);
            //                }
            //            }

            //            TempData["error"] = messages_errors;
            //            //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
            //            return RedirectToAction("ErrorSSS", "SS");
            //        }
            //    }
            //}
            //else
            //{
            //    messages_errors = string.Join("; ", ModelState.Values
            //                        .SelectMany(x => x.Errors)
            //                        .Select(x => x.ErrorMessage));

            //    TempData["error"] = messages_errors;
            //    //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
            //    return RedirectToAction("ErrorSSS", "SS");
            //}
        }


        [HttpGet]
        public ActionResult ModificarSSS(int? id)
        {
            ViewBag.Current = "ModificarSSS";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                if (id.HasValue)
                {
                    using (SoporteICSA_prdEntities ctx = new SoporteICSA_prdEntities())
                    {
                        IniciarViewbag();

                        SolicitudSS solicitud = new SolicitudSS
                        {
                            Cabecera = ObtenerCabecera(int.Parse(id.ToString())),
                            Logs = ObtenerLog(int.Parse(id.ToString())),
                            Documentos = ObtenerDocumento(1)
                        };

                        if (solicitud.Cabecera.SolicitudId == 0)
                        {
                            messages_errors = "No fue posible cargar la Solicitud, ya que Solicitud Soporte Sistemas (SSS) nro. " + id.ToString() + " NO fue encontrada";
                            TempData["error"] = messages_errors;
                            return RedirectToAction("ErrorSSS", "SS");
                        }


                        // Para Logs, valores originales
                        TempData["estadoid"] = solicitud.Cabecera.EstadoId;
                        TempData["tiposolicitudid"] = solicitud.Cabecera.TipoSolicitudId;

                        //TempData["solicitante"] = solicitud.Cabecera.SolicitanteEmail;
                        if (string.IsNullOrEmpty(solicitud.Cabecera.SolicitanteEmail))
                            TempData["solicitante"] = string.Empty;
                        else
                            TempData["solicitante"] = solicitud.Cabecera.SolicitanteEmail;


                        TempData["descripcion"] = solicitud.Cabecera.Descripcion;

                        if (solicitud.Cabecera.FechaComprometida.HasValue)
                        {
                            DateTime fc = solicitud.Cabecera.FechaComprometida.Value;
                            DateTime aux_fc = new DateTime(fc.Year, fc.Month, fc.Day, 0, 0, 0);
                            //TempData["fechacomprometida"] = solicitud.Cabecera.FechaComprometida;
                            TempData["fechacomprometida"] = aux_fc;
                        }

                        if (solicitud.Cabecera.EmpresaId.HasValue)
                            TempData["empresaid"] = solicitud.Cabecera.EmpresaId.Value;
                        else
                            TempData["empresaid"] = 0;

                        if (string.IsNullOrEmpty(solicitud.Cabecera.AsignadoAEmail))
                            TempData["tecnico"] = string.Empty;
                        else
                            TempData["tecnico"] = solicitud.Cabecera.AsignadoAEmail;

                        if (solicitud.Cabecera.CategoriaId.HasValue)
                            TempData["categoriaid"] = solicitud.Cabecera.CategoriaId.Value;
                        else
                            TempData["categoriaid"] = 0;

                        return View(solicitud);
                    }

                }
                else
                {
                    messages_errors = "No fue posible cargar la Solicitud, ya que no se reconoce el nro. solicitud";
                    TempData["error"] = messages_errors;
                    return RedirectToAction("ErrorSSS", "SS");
                }
            }

        }


        [HttpPost]
        [ValidateInput(false)]
        //public ActionResult ModificarSSS(CabeceraSSS cabeceraunicaeneluniverso)
        public ActionResult ModificarSSS(SolicitudSS solicitud_completa)
        {
            // LOGS 

            List<Modificacion> lista_mod_sol = new List<Modificacion>();

            // no es factible identificar personal en este momento ya que si un email no existe en tabla 
            // PersonalId (nuevo tecnico), se obtiene Id igual a 1 (sin asignar). Esto ya que la funcion
            // Construir Solicitud crea un nuevo tecnico

            if (string.IsNullOrEmpty(TempData["tecnico"].ToString()) && string.IsNullOrEmpty(solicitud_completa.Cabecera.AsignadoAEmail))
            {
                // ambos campos NO contienen datos
            }
            else if (!string.IsNullOrEmpty(TempData["tecnico"].ToString()) && !string.IsNullOrEmpty(solicitud_completa.Cabecera.AsignadoAEmail))
            {
                // ambos campos contienen datos

                if (TempData["tecnico"].ToString().Equals(solicitud_completa.Cabecera.AsignadoAEmail.ToString()))
                {
                    // mismo tecnico => no crear log
                }
                else
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Técnico",
                        campoId = "tecnico",
                        tipoModificacionId = 2,
                        codigoOriginalId = solicitud_completa.Cabecera.AsignadoAId.Value,
                        codigoOriginalNombre = TempData["tecnico"].ToString(),
                        comentario = "Se modificó el campo 'Técnico', de: '" + TempData["tecnico"].ToString() + "' a: '"
                    });
                }
            }
            else if (string.IsNullOrEmpty(TempData["tecnico"].ToString()) || string.IsNullOrEmpty(solicitud_completa.Cabecera.AsignadoAEmail))
            {
                if (string.IsNullOrEmpty(TempData["tecnico"].ToString()))
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Técnico",
                        campoId = "tecnico",
                        tipoModificacionId = 5,
                        codigoOriginalId = 0,
                        codigoOriginalNombre = " ",
                        comentario = "Se incorporó el técnico '"
                    });
                }
                else if (string.IsNullOrEmpty(solicitud_completa.Cabecera.AsignadoAEmail))
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Técnico",
                        campoId = "tecnico",
                        tipoModificacionId = 5,
                        codigoOriginalId = solicitud_completa.Cabecera.AsignadoAId.Value,
                        codigoOriginalNombre = TempData["tecnico"].ToString(),
                        comentario = "Se eliminó el técnico '" + TempData["tecnico"].ToString() + "'"
                    });
                }
                else
                {

                }
            }

            if (TempData["estadoid"].ToString().Equals(solicitud_completa.Cabecera.EstadoId.ToString()))
            {
                // mismo estado => no crear log
            }
            else
            {
                // solicitar log
                lista_mod_sol.Add(new Modificacion()
                {
                    SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                    entidad = "solicitud",
                    campoNombre = "Estado",
                    campoId = "estadoid",
                    tipoModificacionId = 2,
                    codigoOriginalId = int.Parse(TempData["estadoid"].ToString()),
                    codigoOriginalNombre = solicitud_completa.Cabecera.EstadoNombre,
                    comentario = "Se modificó el campo 'Estado', de: '" + solicitud_completa.Cabecera.EstadoNombre + "' a: '"
                    //codigoNuevoId = solicitud_completa.Cabecera.EstadoId,
                    //codigoNuevoNombre = ""
                });
            }

            if (TempData["tiposolicitudid"].ToString().Equals(solicitud_completa.Cabecera.TipoSolicitudId.ToString()))
            {
                // mismo tipo solicitud => no crear log
            }
            else
            {
                // solicitar log
                lista_mod_sol.Add(new Modificacion()
                {
                    SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                    entidad = "solicitud",
                    campoNombre = "Tipo Solicitud",
                    campoId = "tiposolicitudid",
                    tipoModificacionId = 2,
                    codigoOriginalId = int.Parse(TempData["tiposolicitudid"].ToString()),
                    codigoOriginalNombre = solicitud_completa.Cabecera.TipoSolicitudNombre,
                    comentario = "Se modificó el campo 'Tipo Solicitud', de: '" + solicitud_completa.Cabecera.TipoSolicitudNombre + "' a: '"
                    //codigoNuevoId = solicitud_completa.Cabecera.TipoSolicitudId,
                    //codigoNuevoNombre = ""
                });
            }


            if (string.IsNullOrEmpty(TempData["solicitante"].ToString()) && string.IsNullOrEmpty(solicitud_completa.Cabecera.SolicitanteEmail))
            {
                // ambos campos NO contienen datos
            }
            else if (!string.IsNullOrEmpty(TempData["solicitante"].ToString()) && !string.IsNullOrEmpty(solicitud_completa.Cabecera.SolicitanteEmail))
            {
                // ambos campos contienen datos

                if (TempData["solicitante"].ToString().Equals(solicitud_completa.Cabecera.SolicitanteEmail.ToString()))
                {
                    // mismo solicitante => no crear log
                }
                else
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Solicitante",
                        campoId = "solicitante",
                        tipoModificacionId = 2,
                        codigoOriginalId = 0,
                        codigoOriginalNombre = TempData["solicitante"].ToString(),
                        comentario = "Se modificó el campo 'Solicitante', de: '" + TempData["solicitante"].ToString() + "' a: '"
                        //codigoNuevoId = solicitud_completa.Cabecera.EstadoId,
                        //codigoNuevoNombre = ""
                    });
                }

            }
            else if (string.IsNullOrEmpty(TempData["solicitante"].ToString()) || string.IsNullOrEmpty(solicitud_completa.Cabecera.SolicitanteEmail))
            {
                if (string.IsNullOrEmpty(TempData["solicitante"].ToString()))
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Solicitante",
                        campoId = "solicitante",
                        tipoModificacionId = 5,
                        codigoOriginalId = 0,
                        codigoOriginalNombre = " ",
                        comentario = "Se incorporó el solicitante '"
                    });
                }
                else if (string.IsNullOrEmpty(solicitud_completa.Cabecera.SolicitanteEmail))
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Solicitante",
                        campoId = "solicitante",
                        tipoModificacionId = 5,
                        codigoOriginalId = solicitud_completa.Cabecera.SolicitanteId.Value,
                        codigoOriginalNombre = TempData["solicitante"].ToString(),
                        comentario = "Se eliminó el solicitante '" + TempData["solicitante"].ToString() + "'"
                    });
                }
                else
                {

                }
            }


            if (TempData["descripcion"].ToString().Equals(solicitud_completa.Cabecera.Descripcion.ToString()))
            {
                // mismo solicitante => no crear log
            }
            else
            {
                // solicitar log
                lista_mod_sol.Add(new Modificacion()
                {
                    SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                    entidad = "solicitud",
                    campoNombre = "Descripción",
                    campoId = "descripcion",
                    tipoModificacionId = 2,
                    codigoOriginalId = 0,
                    codigoOriginalNombre = TempData["descripcion"].ToString(),
                    comentario = "Se modificó el campo 'Descripción', de: '" + TempData["descripcion"].ToString() + "' a: '"
                    //codigoNuevoId = solicitud_completa.Cabecera.EstadoId,
                    //codigoNuevoNombre = ""
                });
            }

            if ((int.Parse(TempData["empresaid"].ToString()) == 0) && (!solicitud_completa.Cabecera.EmpresaId.HasValue))
            {
                // ambos campos NO contienen datos
            }
            else if ((int.Parse(TempData["empresaid"].ToString()) > 0) && (solicitud_completa.Cabecera.EmpresaId.HasValue))
            {
                // ambos campos contienen datos

                if (TempData["empresaid"].ToString().Equals(solicitud_completa.Cabecera.EmpresaId.ToString()))
                {
                    // misma empresa => no crear log
                }
                else
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Empresa",
                        campoId = "empresaid",
                        tipoModificacionId = 2,
                        codigoOriginalId = int.Parse(TempData["empresaid"].ToString()),
                        codigoOriginalNombre = solicitud_completa.Cabecera.EmpresaNombre,
                        comentario = "Se modificó el campo 'Empresa', de: '" + solicitud_completa.Cabecera.EmpresaNombre + "' a: '"
                        //codigoNuevoId = solicitud_completa.Cabecera.EmpresaId.Value,
                        //codigoNuevoNombre = ""
                    });
                }
            }
            else if ((int.Parse(TempData["empresaid"].ToString()) == 0) || (!solicitud_completa.Cabecera.EmpresaId.HasValue))
            {
                if (int.Parse(TempData["empresaid"].ToString()) == 0)
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Empresa",
                        campoId = "empresaid",
                        tipoModificacionId = 5,
                        codigoOriginalId = 0,
                        codigoOriginalNombre = " ",
                        comentario = "Se incorporó la empresa '"
                    });
                }
                else if (!solicitud_completa.Cabecera.EmpresaId.HasValue)
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Empresa",
                        campoId = "empresaid",
                        tipoModificacionId = 5,
                        codigoOriginalId = int.Parse(TempData["empresaid"].ToString()),
                        codigoOriginalNombre = solicitud_completa.Cabecera.EmpresaNombre,
                        comentario = "Se eliminó la empresa '" + solicitud_completa.Cabecera.EmpresaNombre + "' "
                    });
                }
                else
                {

                }
            }

            if ((int.Parse(TempData["categoriaid"].ToString()) == 0) && (!solicitud_completa.Cabecera.CategoriaId.HasValue))
            {
                // ambos campos NO contienen datos
            }
            else if ((int.Parse(TempData["categoriaid"].ToString()) > 0) && (solicitud_completa.Cabecera.CategoriaId.HasValue))
            {
                // ambos campos contienen datos

                if (TempData["categoriaid"].ToString().Equals(solicitud_completa.Cabecera.CategoriaId.ToString()))
                {
                    // misma categoria => no crear log
                }
                else
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Categoría",
                        campoId = "categoriaid",
                        tipoModificacionId = 2,             // modificacion
                        codigoOriginalId = int.Parse(TempData["categoriaid"].ToString()),
                        codigoOriginalNombre = solicitud_completa.Cabecera.CategoriaNombre,
                        comentario = "Se modificó el campo 'Categoría', de: '" + solicitud_completa.Cabecera.CategoriaNombre + "' a: '"
                        //codigoNuevoId = solicitud_completa.Cabecera.CategoriaId.Value,
                        //codigoNuevoNombre = ""
                    });
                }
            }
            else if ((int.Parse(TempData["categoriaid"].ToString()) == 0) || (!solicitud_completa.Cabecera.CategoriaId.HasValue))
            {
                if (int.Parse(TempData["categoriaid"].ToString()) == 0)
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Categoría",
                        campoId = "categoriaid",
                        tipoModificacionId = 5,             // actualizacion
                        codigoOriginalId = 0,
                        codigoOriginalNombre = " ",
                        comentario = "Se incorporó la categoría '"
                    });
                }
                else if (!solicitud_completa.Cabecera.CategoriaId.HasValue)
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Categoría",
                        campoId = "categoriaid",
                        tipoModificacionId = 6,             // eliminacion
                        codigoOriginalId = int.Parse(TempData["categoriaid"].ToString()),
                        codigoOriginalNombre = solicitud_completa.Cabecera.CategoriaNombre,
                        comentario = "Se eliminó la categoría '" + solicitud_completa.Cabecera.CategoriaNombre + "'"
                    });
                }
                else
                {

                }
            }



            if (string.IsNullOrEmpty(TempData["fechacomprometida"].ToString()) && (solicitud_completa.Cabecera.FechaComprometida.HasValue == false))
            {
                // ambos campos NO contienen datos
            }
            else if (!string.IsNullOrEmpty(TempData["fechacomprometida"].ToString()) && (solicitud_completa.Cabecera.FechaComprometida.HasValue == true))
            {
                // ambos campos contienen datos

                if (TempData["fechacomprometida"].ToString().Equals(solicitud_completa.Cabecera.FechaComprometida.ToString()))
                {
                    // misma fecha => no crear log
                }
                else
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Fecha Comprometida",
                        campoId = "fechacomprometida",
                        tipoModificacionId = 2,
                        codigoOriginalId = 0,
                        codigoOriginalNombre = TempData["fechacomprometida"].ToString(),
                        comentario = "Se modificó el campo 'Fecha Comprometida', de: '" + TempData["fechacomprometida"].ToString().Substring(0, 10) + "' a: '"
                    });
                }
            }
            else if (string.IsNullOrEmpty(TempData["fechacomprometida"].ToString()) || (solicitud_completa.Cabecera.FechaComprometida.HasValue == false))
            {
                if (string.IsNullOrEmpty(TempData["fechacomprometida"].ToString()))
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Fecha Comprometida",
                        campoId = "fechacomprometida",
                        tipoModificacionId = 5,
                        codigoOriginalId = 0,
                        codigoOriginalNombre = " ",
                        comentario = "Se incorporó la Fecha Comprometida '"
                    });
                }
                else if (solicitud_completa.Cabecera.FechaComprometida.HasValue == false)
                {
                    // solicitar log
                    lista_mod_sol.Add(new Modificacion()
                    {
                        SolicitudId = solicitud_completa.Cabecera.SolicitudId,
                        entidad = "solicitud",
                        campoNombre = "Fecha Comprometida",
                        campoId = "fechacomprometida",
                        tipoModificacionId = 5,
                        codigoOriginalId = 0,
                        codigoOriginalNombre = TempData["fechacomprometida"].ToString(),
                        comentario = "Se eliminó la Fecha Comprometida '" + TempData["fechacomprometida"].ToString() + "'"
                    });
                }
                else
                {

                }
            }



            return CrearModificarSolicitudSoporteSistemas(solicitud_completa.Cabecera, 2, "", lista_mod_sol);
        }


        [HttpPost]
        public ActionResult BuscarSSS(int? buscarsss)
        {
            ViewBag.Current = "BuscarSSS";

            string messages_errors = string.Empty;

            if (buscarsss.HasValue)
            {
                messages_errors = "¡Solicitud " + buscarsss.ToString() + " encontrada!";
                TempData["exito"] = messages_errors;
                return RedirectToAction("ModificarSSS", "SS", new { @id = buscarsss });
            }
            else
            {
                messages_errors = "No fue posible cargar la Solicitud Soporte Sistemas (SSS) " + buscarsss.ToString() + "ya que no ingresó un nro. entero. ";
                TempData["error"] = messages_errors;
                return RedirectToAction("ErrorSSS", "SS");
            }
        }


        [HttpGet]
        public ActionResult MisSSSPendientes(string idred)
        {
            ViewBag.Current = "MisSSSPendientes";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                string[] ir;
                int idpersonal;
                List<CabeceraSSS> lista_auxiliar_misssspendientes = new List<CabeceraSSS>();

                if (!string.IsNullOrEmpty(idred))
                {
                    ir = idred.ToString().Split('\\');
                    string us_ir = ir[1].Trim();
                    string do_ir = ir[0].Trim();

                    idpersonal = ObtenerIdPersonal(ir[1].Trim());

                    if (idpersonal == 0)
                    {
                        messages_errors = "No fue posible obtener el Id Personal del usuario " + idred + " desde función ObtenerIdPersonal()";
                        TempData["error"] = messages_errors;
                        //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                        return RedirectToAction("ErrorSSS", "SS");
                    }
                    else
                    {
                        // buscar solicitudes de este usuario cuyo estado no sea de tipo "final"

                        using (SoporteICSA_prdEntities bd_sol = new SoporteICSA_prdEntities())
                        {
                            var listaMisSolicitudesPendientes = bd_sol.Solicitud.
                                Where(m => (m.PersonalId == idpersonal)).
                                Join(bd_sol.EstadoSolicitud, c => c.EstadoSolicitudId, o => o.EstadoSolicitudId, (c, o) => new
                                {
                                    c.SolicitudId,
                                    c.Descr,
                                    c.EsIncidente,
                                    c.SolicitanteNombre,
                                    c.TipoSolicitudId,
                                    c.EmpresaId,
                                    o.tipo
                                }).Where(o => o.tipo.Equals("inicial")).OrderByDescending(f => f.SolicitudId).ToList();


                            if (listaMisSolicitudesPendientes.Count() > 0)
                            {
                                foreach (var msp in listaMisSolicitudesPendientes)
                                {
                                    lista_auxiliar_misssspendientes.Add(ObtenerCabecera(msp.SolicitudId));
                                }
                            }
                            else
                            {
                            }

                            //ViewBag.TotalCabeceras = lista_auxiliar_misssspendientes.Count();
                            //ViewData["cabeceras_MisSSSPendientes"] = lista_auxiliar_misssspendientes;
                            return View(lista_auxiliar_misssspendientes);
                        }
                    }
                }
                else
                {
                    messages_errors = "No fue posible obtener el Id Red del usuario desde la consola";
                    TempData["error"] = messages_errors;
                    //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                    return RedirectToAction("ErrorSSS", "SS");
                }
            }
        }


        [HttpGet]
        public ActionResult SSSPendientes()
        {
            ViewBag.Current = "SSSPendientes";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                List<CabeceraSSS> lista_auxiliar_ssspendientes = new List<CabeceraSSS>();

                // buscar solicitudes cuyo estado no sea de tipo "final"

                using (SoporteICSA_prdEntities bd_pend = new SoporteICSA_prdEntities())
                {
                    var listaSolicitudesPendientes = bd_pend.Solicitud.
                        Join(bd_pend.EstadoSolicitud, c => c.EstadoSolicitudId, o => o.EstadoSolicitudId, (c, o) => new
                        {
                            c.SolicitudId,
                            c.Descr,
                            c.EsIncidente,
                            c.SolicitanteNombre,
                            c.TipoSolicitudId,
                            c.EmpresaId,
                            o.tipo
                            //}).Where(o => o.tipo.Equals("inicial")).ToList();
                        }).Where(o => o.tipo.Equals("inicial")).OrderByDescending(f => f.SolicitudId).ToList();


                    if (listaSolicitudesPendientes.Count() > 0)
                    {
                        foreach (var msp in listaSolicitudesPendientes)
                        {
                            lista_auxiliar_ssspendientes.Add(ObtenerCabecera(msp.SolicitudId));
                        }
                    }
                    else
                    {
                    }

                    //ViewBag.TotalCabeceras = lista_auxiliar_misssspendientes.Count();
                    //ViewData["cabeceras_MisSSSPendientes"] = lista_auxiliar_misssspendientes;
                    return View(lista_auxiliar_ssspendientes);
                }
            }
        }


        [HttpGet]
        public ActionResult SSSSinAsignar()
        {
            ViewBag.Current = "SSSSinAsignar";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                List<CabeceraSSS> lista_auxiliar_ssssinasignar = new List<CabeceraSSS>();

                // buscar solicitudes cuyo estado no sea de tipo "final"

                using (SoporteICSA_prdEntities bd_sa = new SoporteICSA_prdEntities())
                {
                    var listaSolicitudesSinAsignar = bd_sa.Solicitud.
                        Where(m => m.PersonalId == 1).
                        Join(bd_sa.EstadoSolicitud, c => c.EstadoSolicitudId, o => o.EstadoSolicitudId, (c, o) => new
                        {
                            c.SolicitudId,
                            c.Descr,
                            c.EsIncidente,
                            c.SolicitanteNombre,
                            c.TipoSolicitudId,
                            c.EmpresaId,
                            o.tipo
                        }).Where(o => o.tipo.Equals("inicial")).ToList();


                    if (listaSolicitudesSinAsignar.Count() > 0)
                    {
                        foreach (var msp in listaSolicitudesSinAsignar)
                        {
                            lista_auxiliar_ssssinasignar.Add(ObtenerCabecera(msp.SolicitudId));
                        }
                    }
                    else
                    {
                    }

                    return View(lista_auxiliar_ssssinasignar);
                }
            }
        }


        [HttpGet]
        public ActionResult DashboardAdm()
        {
            ViewBag.Current = "DashboardAdm";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                //string res = FormarCadenaParaGraficoTorta("SP_DB_Obtener_Tipo_Solicitud", "Tipo", "Cant.");

                //ViewBag.tipoSolicitudConcatenado = res;

                return View();
            }
        }


        [HttpGet]
        public ActionResult DashboardTec()
        {
            ViewBag.Current = "DashboardTec";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult IncidentesPendientes()
        {
            ViewBag.Current = "IncidentesPendientes";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                List<CabeceraSSS> lista_auxiliar_incidentes_pendientes = new List<CabeceraSSS>();

                // buscar solicitudes con estado NO final (o inicial) y con campo "EsIncidente" igual a TRUE

                using (SoporteICSA_prdEntities bd_ip = new SoporteICSA_prdEntities())
                {
                    var listaIncidentesPendientes = bd_ip.Solicitud.
                        Where(m => m.EsIncidente == 1).
                        Join(bd_ip.EstadoSolicitud, c => c.EstadoSolicitudId, o => o.EstadoSolicitudId, (c, o) => new
                        {
                            c.SolicitudId,
                            c.Descr,
                            c.EsIncidente,
                            c.SolicitanteNombre,
                            c.TipoSolicitudId,
                            c.EmpresaId,
                            o.tipo
                        }).Where(o => o.tipo.Equals("inicial")).ToList();


                    if (listaIncidentesPendientes.Count() > 0)
                    {
                        foreach (var msp in listaIncidentesPendientes)
                        {
                            lista_auxiliar_incidentes_pendientes.Add(ObtenerCabecera(msp.SolicitudId));
                        }
                    }
                    else
                    {
                    }

                    return View(lista_auxiliar_incidentes_pendientes);
                }
            }
        }


        [HttpGet]
        public ActionResult MisIncidentesPendientes(string idred) // Formato idred: Socovesa\\vsandoval
        {
            ViewBag.Current = "MisIncidentesPendientes";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                string[] ir;
                int idpersonal;
                List<CabeceraSSS> lista_auxiliar_misincidentespendientes = new List<CabeceraSSS>();

                if (!string.IsNullOrEmpty(idred))
                {
                    ir = idred.ToString().Split('\\');
                    string us_ir = ir[1].Trim();
                    string do_ir = ir[0].Trim();

                    idpersonal = ObtenerIdPersonal(ir[1].Trim());

                    if (idpersonal == 0)
                    {
                        messages_errors = "No fue posible obtener el Id Personal del usuario " + idred + " desde función ObtenerIdPersonal()";
                        TempData["error"] = messages_errors;
                        //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                        return RedirectToAction("ErrorSSS", "SS");
                    }
                    else
                    {
                        // buscar solicitudes de este usuario cuyo estado no sea de tipo "final"

                        using (SoporteICSA_prdEntities bd_sol = new SoporteICSA_prdEntities())
                        {
                            var listaMisIncidentesPendientes = bd_sol.Solicitud.
                                Where(m => (m.PersonalId == idpersonal) && (m.EsIncidente == 1)).
                                Join(bd_sol.EstadoSolicitud, c => c.EstadoSolicitudId, o => o.EstadoSolicitudId, (c, o) => new
                                {
                                    c.SolicitudId,
                                    c.Descr,
                                    c.EsIncidente,
                                    c.SolicitanteNombre,
                                    c.TipoSolicitudId,
                                    c.EmpresaId,
                                    o.tipo
                                }).Where(o => o.tipo.Equals("inicial")).ToList();


                            if (listaMisIncidentesPendientes.Count() > 0)
                            {
                                foreach (var msp in listaMisIncidentesPendientes)
                                {
                                    lista_auxiliar_misincidentespendientes.Add(ObtenerCabecera(msp.SolicitudId));
                                }
                            }
                            else
                            {
                            }

                            //ViewBag.TotalCabeceras = lista_auxiliar_misssspendientes.Count();
                            //ViewData["cabeceras_MisSSSPendientes"] = lista_auxiliar_misssspendientes;
                            return View(lista_auxiliar_misincidentespendientes);
                        }
                    }
                }
                else
                {
                    messages_errors = "No fue posible obtener el Id Red del usuario desde la consola";
                    TempData["error"] = messages_errors;
                    //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                    return RedirectToAction("ErrorSSS", "SS");
                }
            }
        }


        [HttpGet]
        public ActionResult MisSSSResueltas(string idred)
        {
            ViewBag.Current = "MisSSSResueltas";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                string[] ir;
                int idpersonal;
                List<CabeceraSSS> lista_auxiliar_missssresueltas = new List<CabeceraSSS>();

                if (!string.IsNullOrEmpty(idred))
                {
                    ir = idred.ToString().Split('\\');
                    string us_ir = ir[1].Trim();
                    string do_ir = ir[0].Trim();

                    idpersonal = ObtenerIdPersonal(ir[1].Trim());

                    if (idpersonal == 0)
                    {
                        messages_errors = "No fue posible obtener el Id Personal del usuario " + idred + " desde función ObtenerIdPersonal()";
                        TempData["error"] = messages_errors;
                        //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                        return RedirectToAction("ErrorSSS", "SS");
                    }
                    else
                    {
                        // buscar solicitudes de este usuario cuyo estado sea de tipo "final"

                        using (SoporteICSA_prdEntities bd_sol = new SoporteICSA_prdEntities())
                        {
                            // si fecha hoy es 12-03-2018, entonces FechaCorte valdrá 12-12-2017
                            DateTime FechaCorte = DateTime.Now - new TimeSpan(90, 0, 0, 0);

                            // buscar Mis Solicitudes Resueltas de los ultimos 3 meses (fecha resolucion desde 90 días atrás)
                            var listaMisSolicitudesResueltas = bd_sol.Solicitud.
                                Where(m => (m.PersonalId == idpersonal) && (m.fecha_creacion.Value > (FechaCorte))).
                                Join(bd_sol.EstadoSolicitud, c => c.EstadoSolicitudId, o => o.EstadoSolicitudId, (c, o) => new
                                {
                                    c.SolicitudId,
                                    c.Descr,
                                    c.EsIncidente,
                                    c.SolicitanteNombre,
                                    c.TipoSolicitudId,
                                    c.EmpresaId,
                                    o.tipo
                                }).Where(o => o.tipo.Equals("final")).ToList();


                            if (listaMisSolicitudesResueltas.Count() > 0)
                            {
                                foreach (var msp in listaMisSolicitudesResueltas)
                                {
                                    lista_auxiliar_missssresueltas.Add(ObtenerCabecera(msp.SolicitudId));
                                }
                            }
                            else
                            {
                            }

                            //ViewBag.TotalCabeceras = lista_auxiliar_misssspendientes.Count();
                            //ViewData["cabeceras_MisSSSPendientes"] = lista_auxiliar_misssspendientes;
                            return View(lista_auxiliar_missssresueltas);
                        }
                    }
                }
                else
                {
                    messages_errors = "No fue posible obtener el Id Red del usuario desde la consola";
                    TempData["error"] = messages_errors;
                    //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                    return RedirectToAction("ErrorSSS", "SS");
                }
            }
        }


        [HttpGet]
        public ActionResult SSSResueltas()
        {
            ViewBag.Current = "SSSResueltas";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                List<CabeceraSSS> lista_auxiliar_missssresueltas = new List<CabeceraSSS>();

                // buscar solicitudes cuyo estado sea de tipo "final"

                using (SoporteICSA_prdEntities bd_sol = new SoporteICSA_prdEntities())
                {
                    // si fecha hoy es 12-03-2018, entonces FechaCorte valdrá 12-12-2017
                    DateTime FechaCorte = DateTime.Now - new TimeSpan(90, 0, 0, 0);

                    // buscar Solicitudes Resueltas de los ultimos 3 meses (fecha resolucion desde 90 días atrás)
                    var listaMisSolicitudesResueltas = bd_sol.Solicitud.
                        Where(m => (m.fecha_creacion.Value > (FechaCorte))).
                        Join(bd_sol.EstadoSolicitud, c => c.EstadoSolicitudId, o => o.EstadoSolicitudId, (c, o) => new
                        {
                            c.SolicitudId,
                            c.Descr,
                            c.EsIncidente,
                            c.SolicitanteNombre,
                            c.TipoSolicitudId,
                            c.EmpresaId,
                            o.tipo
                        }).Where(o => o.tipo.Equals("final")).ToList();


                    if (listaMisSolicitudesResueltas.Count() > 0)
                    {
                        foreach (var msp in listaMisSolicitudesResueltas)
                        {
                            lista_auxiliar_missssresueltas.Add(ObtenerCabecera(msp.SolicitudId));
                        }
                    }
                    else
                    {
                    }

                    //ViewBag.TotalCabeceras = lista_auxiliar_misssspendientes.Count();
                    //ViewData["cabeceras_MisSSSPendientes"] = lista_auxiliar_misssspendientes;
                    return View(lista_auxiliar_missssresueltas);
                }
            }
        }


        [HttpGet]
        public ActionResult SSSTotal()
        {
            ViewBag.Current = "SSSTotal";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                List<CabeceraSSS> lista_auxiliar_misssstotal = new List<CabeceraSSS>();

                // buscar solicitudes cuyo estado sea de tipo "final"

                using (SoporteICSA_prdEntities bd_sol = new SoporteICSA_prdEntities())
                {
                    // si fecha hoy es 12-03-2018, entonces FechaCorte valdrá 12-12-2017
                    DateTime FechaCorte = DateTime.Now - new TimeSpan(90, 0, 0, 0);

                    // buscar todas las Solicitudes Resueltas de los ultimos 3 meses (fecha resolucion desde 90 días atrás)
                    var listaTotalSolicitudes = bd_sol.Solicitud.
                        Where(m => (m.fecha_creacion.Value > (FechaCorte)));

                    if (listaTotalSolicitudes.Count() > 0)
                    {
                        foreach (var msp in listaTotalSolicitudes)
                        {
                            lista_auxiliar_misssstotal.Add(ObtenerCabecera(msp.SolicitudId));
                        }
                    }
                    else
                    {
                    }

                    //ViewBag.TotalCabeceras = lista_auxiliar_misssspendientes.Count();
                    //ViewData["cabeceras_MisSSSPendientes"] = lista_auxiliar_misssspendientes;
                    return View(lista_auxiliar_misssstotal);
                }
            }
        }



        [HttpGet]
        public ActionResult Dashboard()
        {
            ViewBag.Current = "Dashboard";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                //string res = FormarCadenaParaGraficoTorta("SP_DB_Obtener_Tipo_Solicitud", "Tipo", "Cant.");

                //ViewBag.tipoSolicitudConcatenado = res;

                return View();
            }
        }


        [HttpGet]
        public ActionResult FormularioContacto(string idred)
        {
            ViewBag.Current = "FormularioContacto";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                string[] ir;
                ir = idred.ToString().Split('\\');
                string us_ir = ir[1].Trim();
                string do_ir = ir[0].Trim();

                Email emailformulariocontacto = ConstruirEmailFormularioContacto(ObtenerUsuarioDesdeActiveDirectoryConNickname(us_ir));

                return View(emailformulariocontacto);
            }
        }


        [HttpPost]
        public ActionResult FormularioContacto()
        {
            //ViewBag.Current = "FormularioContacto";

            string resultado = string.Empty;

            // 1.- Construir mensaje
            // 2.- Identificar cliente de correo
            // 3.- Mostrar por pantalla si fue exitoso o no


            /* 1.- -------------------------MENSAJE DE CORREO---------------------- */

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mensaje = new System.Net.Mail.MailMessage();

            // Correo electronico desde la que enviamos el mensaje
            mensaje.From = new System.Net.Mail.MailAddress("vsandoval@socovesa.cl", "Solicitud Soporte Sistemas");

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mensaje.To.Add("victorssandovaln@gmail.com");
            //mensaje.To.Add("vsandoval@socovesa.cl");
            //mensaje.Bcc.Add("vsandoval@socovesa.cl");
            //mensaje.Bcc.Add("jtapia@socovesa.cl");
            //mensaje.Bcc.Add("sibarra@socovesa.cl");

            // Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario
            mensaje.Subject = "Prueba";
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;

            // Cuerpo del Correo.                                    
            mensaje.Body = "Mensaje de prueba";
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;

            // Seteo que el server notifique solamente en el error de entrega
            // Delivery notifications
            mensaje.DeliveryNotificationOptions =
              DeliveryNotificationOptions.OnFailure |
              DeliveryNotificationOptions.OnSuccess |
              DeliveryNotificationOptions.Delay;

            // Ask for a read receipt
            mensaje.Headers.Add("Notificaciones", "vsandoval@socovesa.cl");


            /* 2.- -------------------------CLIENTE DE CORREO----------------------*/

            // Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient("internal-relay.socovesa.cl", 25);

            // Hay que crear las credenciales del correo emisor
            //cliente.UseDefaultCredentials = true;
            cliente.Credentials = new System.Net.NetworkCredential("vsandoval@socovesa.cl", "MaySoc2018");

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail                
            //cliente.Port = 587;
            //cliente.EnableSsl = true;
            //cliente.Host = "smtp.gmail.com";


            /* 3.- -------------------------EJECUTAR ENVIO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mensaje);

                TempData["exito"] = "Email enviado! Nos pondremos en contacto con ud. a la brevedad.  ";
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                return RedirectToAction("ExitoSSS", "SS");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
                //Log("No se envia el correo " + Lista[i].Correo + ".");

                TempData["error"] = "No fue enviado el email!" + ex.ToString();
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                return RedirectToAction("ErrorSSS", "SS");
            }
        }


        [HttpGet]
        public ActionResult ExitoSSS()
        {
            ViewBag.Current = "ExitoSSS";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                string exito = string.Empty;

                if (TempData.ContainsKey("exito"))
                    exito = TempData["exito"].ToString();

                ViewBag.MensajeExito = exito;

                return View();
            }
        }


        [HttpGet]
        //public PartialViewResult CrearLogSSS(CabeceraSSS cab)
        public PartialViewResult CrearLogSSS(int solicitudid, decimal solicitudhora, int personalid, string tecnico, string tipoestado)
        {
            ViewBag.Current = "CrearLogSSS";

            IniciarViewbagLog();

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                //return RedirectToAction("LoginErrorSSS");
                return PartialView("LoginErrorSSS");
            }
            else
            {
                LogSSS lm = IniciarLogSSS();

                lm.LogSolicitudId = solicitudid;
                //lm.LogSolicitudId = cab.SolicitudId;
                lm.LogSolicitudTipoEstadoIdNombre = tipoestado;
                //lm.LogSolicitudTipoEstadoIdNombre = cab.EstadoTipo;

                lm.LogAccionId = 2;                                                 // Manual
                lm.LogTipoId = 4;                                                   // Avance
                lm.LogUsuarioIdRed = @System.Environment.UserName;

                if (personalid > 0)
                    lm.LogPersonalId = personalid;
                //lm.LogPersonalId = cab.AsignadoAId;

                if (!string.IsNullOrEmpty(tecnico))
                {
                    string[] e_tec = tecnico.ToString().Split('@');
                    lm.LogTecnicoEmail = e_tec[0].Trim();
                    //lm.LogTecnicoNombre = cab.AsignadoANombre;
                }

                // CabeceraSSS cc = IniciarCabecera();
                // IniciarViewbag();

                //// para envio de email
                //lm.LogSolicitanteEmail = cab.SolicitanteEmail;
                //lm.LogSolicitanteNombre = cab.SolicitanteEmail;
                //lm.LogSolicitanteUbicacion = cab.UbicacionNombre;
                //lm.LogSolicitanteTelefono = cab.Telefono;
                //lm.LogSolicitudDescripción = cab.Descripcion;

                return PartialView(lm);
            }
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CrearLogSSS(LogSSS nuevo_Log, string submitButton)
        {
            //if(nuevo_Log.LogEmail == true)
            //{
            //    // enviar email
            //}

            switch (submitButton)
            {
                case "CrearLogSSS":
                    // delegate sending to another controller action
                    return CrearLogSolicitudSoporteSistemas_Manual(nuevo_Log);
                case "CerrarSSS":
                    // call another action to perform the cancellation
                    //return CrearLogSolicitudSoporteSistemas_y_CerrarSSS(nuevo_Log);
                    return CrearLogSolicitudSoporteSistemas_y_CerrarSSS(nuevo_Log);
                default:
                    //    // If they've submitted the form without a submitButton, 
                    //    // just return the view again.
                    return CrearLogSolicitudSoporteSistemas_Manual(nuevo_Log);
            }

            //return CrearLogSolicitudSoporteSistemas_Manual(nuevo_Log);
        }


        [HttpGet]
        public ActionResult SubirDocumento()
        {
            ViewBag.Current = "SubirDocumento";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult SubirDocumento(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("UploadDocument");
        }



        public ActionResult ObtenerUsuarios()
        {
            ViewBag.Current = "ObtenerUsuarios";

            // Empresas

            using (SoporteICSA_prdEntities ctx = new SoporteICSA_prdEntities())
            {
                Personal pers = ctx.Personal.Where(e => e.PersonalId == 1).FirstOrDefault();
                List<Personal> lpers = ctx.Personal.ToList();

                ViewBag.ListaPersonal = lpers;

                return Json(lpers, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpGet]
        public ActionResult ErrorSSS(CabeceraSSS cab_con_error)
        {
            ViewBag.Current = "ErrorSSS";

            string messages_errors = string.Empty;

            if (Session["RolId"] == null)
            {
                // Accediento a esta página sin pasar por función AutoLoginSSS
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = "Accediento a esta página sin pasar por función AutoLoginSSS";

                messages_errors = "Accediento a esta página sin pasar por función AutoLoginSSS";
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
            else
            {
                string error = string.Empty;

                if (TempData.ContainsKey("error"))
                    error = TempData["error"].ToString();

                ViewBag.MensajeError = error;

                return View();
            }
        }


        [HttpGet]
        public ActionResult LoginErrorSSS(CabeceraSSS cab_con_error)
        {
            ViewBag.Current = "LoginErrorSSS";

            string error = string.Empty, messages_errors = string.Empty;

            //string rolid = Session["RolId"] as string;

            if (Session["RolId"] == null)
            {
                if (TempData.ContainsKey("error"))
                {
                    error = TempData["error"].ToString();
                }
                else
                {
                    error = "Accediento a esta página sin pasar por función AutoLoginSSS";
                }
            }
            else
            {
                if (Session["Mensaje"] == null)
                {
                    error = "Rol Id: " + Session["RolId"].ToString();
                }
                else
                {
                    error = "Error: " + Session["Mensaje"].ToString() + "\r\n - " + " Rol Id: " + Session["RolId"].ToString();
                }
            }

            ViewBag.MensajeError = error;

            return View();
        }


        [HttpGet]
        //public ActionResult AutoLoginSSS(string idred)                                            // Formato idred: Socovesa\\vsandoval
        public ActionResult AutoLoginSSS()
        {
            ViewBag.Current = "AutoLoginSSS";

            string idred = System.Security.Principal.WindowsIdentity.GetCurrent().Name;             // Formato idred: Socovesa\\vsandoval

            List<PersonalRol> personal_rol = new List<PersonalRol>();
            string error = string.Empty, messages_errors = string.Empty;
            bool salir = false;
            string[] ir;
            int idpersonal;
            UsuarioSSS usuario_sesion = new UsuarioSSS();

            if (!string.IsNullOrEmpty(idred))
            {
                ir = idred.ToString().Split('\\');
                string us_ir = ir[1].Trim();
                string do_ir = ir[0].Trim();

                idpersonal = ObtenerIdPersonal(ir[1].Trim());

                if (idpersonal == 0)
                {
                    error = "No fue posible obtener el Id Personal del usuario " + idred + " desde función ObtenerIdPersonal()";

                    // nickname no existe en tabla Personal de la App. SSS
                    Session["RolId"] = 0;
                    Session["Supervisor"] = false;
                    Session["Mensaje"] = error;

                    messages_errors = error;
                    TempData["error"] = messages_errors;
                    //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                    //return RedirectToAction("ErrorSSS", "SS");
                    return RedirectToAction("LoginErrorSSS");
                }
                else
                {
                    // 1.- validar que usuario este en el Active Directory
                    //usuario_sesion = ObtenerUsuarioDesdeActiveDirectory(email);
                    usuario_sesion = ObtenerUsuarioDesdeActiveDirectoryConNickname(ir[1].Trim());

                    if (usuario_sesion.id == 0)
                    {
                        error = "Usuario IdRed" + idred + " con IdPersonal: " + idpersonal + " no existe en el Active Directory (" + usuario_sesion.error + ")";

                        // nickname no existe en el Active Directory
                        Session["RolId"] = 0;
                        Session["Supervisor"] = false;
                        Session["Mensaje"] = error;

                        messages_errors = error;
                        TempData["error"] = messages_errors;
                        //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                        //return RedirectToAction("ErrorSSS", "SS");
                        return RedirectToAction("LoginErrorSSS");
                    }
                    else
                    {
                        // 2.- validar si es supervisor en la App. SSS

                        using (SoporteICSA_prdEntities bd_rol = new SoporteICSA_prdEntities())
                        {
                            // en tabla PersonalRol se busca por PersonalId
                            personal_rol = bd_rol.PersonalRol.Where(e => e.PersonalId.Equals(idpersonal)).ToList();

                            if (personal_rol.Count > 0)
                            {
                                // tiene algún rol

                                foreach (PersonalRol pr in personal_rol)
                                {
                                    //if (pr.RolId == 6)                        // Es Supervisor
                                    //{
                                    //    System.Web.HttpContext.Current.Session["Supervisor"] = true;
                                    //}
                                    //else                                      // Es Tecnico
                                    //{
                                    //    System.Web.HttpContext.Current.Session["Supervisor"] = false;
                                    //}

                                    if (pr.RolId == 6)                          //  6: Supervisor
                                    {
                                        Session["RolId"] = pr.RolId;
                                        Session["Supervisor"] = true;
                                        Session["Mensaje"] = "Acceso habilitado como Supervisor";
                                        //Session["PersonalId"] = pr.PersonalId;
                                        //Session["UserNickName"] = ir[1].Trim();

                                        salir = true;
                                    }
                                    else
                                    {
                                        Session["RolId"] = 5;
                                        Session["Supervisor"] = false;
                                        Session["Mensaje"] = "Acceso habilitado como Técnico";
                                        //Session["PersonalId"] = pr.PersonalId;
                                        //Session["UserNickName"] = ir[1].Trim();
                                    }

                                    if (salir)
                                        break;
                                }

                                return RedirectToAction("MisSSSPendientes", "SS", new { @idred = idred });
                            }
                            else
                            {
                                error = "Usuario sin acceso habilitado (sin Rol asignado)";

                                // No tiene roles
                                Session["RolId"] = 0;
                                Session["Supervisor"] = false;
                                Session["Mensaje"] = error;
                                //Session["PersonalId"] = pr.PersonalId;
                                //Session["UserNickName"] = ir[1].Trim();

                                messages_errors = error;
                                TempData["error"] = messages_errors;
                                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                                //return RedirectToAction("ErrorSSS", "SS");
                                return RedirectToAction("LoginErrorSSS");
                            }
                        }
                    }
                }
            }
            else
            {
                error = "No fue posible obtener el Id Red del usuario al cargar la aplicación";

                // No tiene idred
                Session["RolId"] = 0;
                Session["Supervisor"] = false;
                Session["Mensaje"] = error;

                messages_errors = error;
                TempData["error"] = messages_errors;
                //return RedirectToAction("ErrorSSS", "SS", new { @id = messages_errors });
                //return RedirectToAction("ErrorSSS", "SS");
                return RedirectToAction("LoginErrorSSS");
            }
        }

    }
}
