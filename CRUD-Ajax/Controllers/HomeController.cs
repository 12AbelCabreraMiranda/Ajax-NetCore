using CRUD_Ajax.DataContext;
using CRUD_Ajax.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Ajax.Controllers
{
    public class HomeController : Controller
    {       
        private readonly Crud_AjaxContext _context;
        public HomeController(Crud_AjaxContext context) => _context = context;

        public IActionResult Index()
        {
            return View();
        }

        //LISTA DE DATOS EN TABLA
        public JsonResult List()
        {
            return Json(_context.Usuario.Where(u=>u.Estado==1).ToList());
        }
        
        //GUARDAR LOS DATOS EN EL MODELO
        public bool Add(Usuario user)
        {            
            bool valor;

            //PROC
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_insertar_users";
            cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 40).Value = user.Nombre + " sp_add";
            cmd.Parameters.Add("@NombreUsuario", System.Data.SqlDbType.VarChar, 40).Value = user.NombreUsuario;
            cmd.Parameters.Add("@Estado", System.Data.SqlDbType.Int).Value = 1;
            cmd.ExecuteNonQuery();
            conn.Close();
            //END PROC
            valor = true;

            //var usuario = _context.Usuario.Where(u=>u.NombreUsuario.Equals(user.NombreUsuario)).ToList();
            //if (!usuario.Count.Equals(0))
            //{
            //    valor = false;
            //}
            //else
            //{
            //    var Tuser = new Usuario()
            //    {
            //        Nombre = user.Nombre,
            //        NombreUsuario = user.NombreUsuario,
            //        Estado = 1
            //    };
            //    _context.Usuario.Add(Tuser);
            //    _context.SaveChanges();
            //    valor = true;                
            //}
            
            return valor;
        }

        //MUESTRA DATOS SELECCIONADO PARA ACTUALIZAR
        public JsonResult GetbyID(int ID)
        {
            return Json(_context.Usuario.FirstOrDefault(x => x.UsuarioId == ID));
        }
        
        //ACTUALIZA DATOS
        public bool Update(Usuario user)
        {
            var valor=false;

            //PROC
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_actualizar_users";
            cmd.Parameters.Add("@UsuarioId", System.Data.SqlDbType.Int).Value = user.UsuarioId;
            cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar, 40).Value = user.Nombre + " sp_update";
            cmd.Parameters.Add("@NombreUsuario", System.Data.SqlDbType.VarChar, 40).Value = user.NombreUsuario;
            cmd.Parameters.Add("@Estado", System.Data.SqlDbType.Int).Value = 1;
            cmd.ExecuteNonQuery();
            conn.Close();
            //END PROC
            valor = true;

            //OBTIENE DATOS DEL USUARIO ENCONTRADO
            //var usuario = _context.Usuario.Where(u=>u.UsuarioId.Equals(user.UsuarioId)).FirstOrDefault();

            ////ACTUALIZA DATOS DEJANDO SU MISMO NOMBRE DE USUARIO
            //if (usuario.UsuarioId.Equals(user.UsuarioId) && usuario.NombreUsuario.Equals(user.NombreUsuario))
            //{
            //    usuario.Nombre = user.Nombre;
            //    usuario.NombreUsuario = user.NombreUsuario;

            //    _context.Usuario.Update(usuario);
            //    _context.SaveChanges();
            //    valor = true;
            //}
            //else
            //{
            //    //ACTUALIZA EL ID ENCONTRADO, SI MODIFICA EL NOMBRE USUARIO DEBE SER DIFERENTE A LOS QUE YA EXISTEN
            //    if (usuario.UsuarioId.Equals(user.UsuarioId) && usuario.NombreUsuario!=user.NombreUsuario)
            //    {
            //        var usuario2 = _context.Usuario.Where(u => u.NombreUsuario.Equals(user.NombreUsuario)).ToList();
            //        if (!usuario2.Count.Equals(0))
            //        {
            //            valor = false;//Dato existente encontrado
            //        }
            //        else
            //        {
            //            usuario.Nombre = user.Nombre;
            //            usuario.NombreUsuario = user.NombreUsuario;

            //            _context.Usuario.Update(usuario);
            //            _context.SaveChanges();

            //            valor = true;
            //        }
            //    }

            //}
            return valor;
        }

        //ELIMINA UN REGISTRO
        public bool Delete(int ID)
        {
            var valor = false;
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "ps_eliminar_users";
            cmd.Parameters.Add("@UsuarioId", System.Data.SqlDbType.Int).Value = ID;
            cmd.Parameters.Add("@Estado", System.Data.SqlDbType.Int).Value = 0;
            cmd.ExecuteNonQuery();
            conn.Close();
            //END PROC
            valor = true;

            return valor;

            //var data = _context.Usuario.FirstOrDefault(x => x.UsuarioId == ID);
            //_context.Usuario.Remove(data);
            //_context.SaveChanges();
            //return Json(data);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
