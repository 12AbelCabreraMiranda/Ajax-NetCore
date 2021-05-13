using CRUD_Ajax.DataContext;
using CRUD_Ajax.Models;
using Microsoft.AspNetCore.Mvc;
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
            return Json(_context.Usuario.ToList());
        }
        
        //GUARDAR LOS DATOS EN EL MODELO
        public bool Add(Usuario user)
        {            
            bool valor;
            var usuario = _context.Usuario.Where(u=>u.NombreUsuario.Equals(user.NombreUsuario)).ToList();
            if (!usuario.Count.Equals(0))
            {
                valor = false;
            }
            else
            {
                var Tuser = new Usuario()
                {
                    Nombre = user.Nombre,
                    NombreUsuario = user.NombreUsuario,
                    Estado = 1
                };
                _context.Usuario.Add(Tuser);
                _context.SaveChanges();
                valor = true;                
            }
            
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
            var usuario = _context.Usuario.Where(u=>u.UsuarioId.Equals(user.UsuarioId)).FirstOrDefault();
            if (usuario.UsuarioId.Equals(user.UsuarioId) && usuario.NombreUsuario.Equals(user.NombreUsuario))
            {

                usuario.Nombre = user.Nombre;
                usuario.NombreUsuario = user.NombreUsuario;
                
                _context.Usuario.Update(usuario);
                _context.SaveChanges();
                valor = true;
            }
            else
            {
                if (usuario.UsuarioId.Equals(user.UsuarioId) && usuario.NombreUsuario!=user.NombreUsuario)
                {
                    var usuario2 = _context.Usuario.Where(u => u.NombreUsuario.Equals(user.NombreUsuario)).ToList();
                    if (!usuario2.Count.Equals(0))
                    {
                        valor = false;//Dato existente encontrado
                    }
                    else
                    {
                        usuario.Nombre = user.Nombre;
                        usuario.NombreUsuario = user.NombreUsuario;
                        
                        _context.Usuario.Update(usuario);
                        _context.SaveChanges();

                        valor = true;
                    }
                }
               
            }
            return valor;//Pendiente, atualizar con el mismo usuario y con el mismo usuario actualizando su usuario sin que sea igual que de los que ya existe

        }

        //ELIMINA UN REGISTRO
        public JsonResult Delete(int ID)
        {
            var data = _context.Usuario.FirstOrDefault(x => x.UsuarioId == ID);
            _context.Usuario.Remove(data);
            _context.SaveChanges();
            return Json(data);
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
