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
        public JsonResult List()
        {
            return Json(_context.Usuario.ToList());
        }
        
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
        public JsonResult GetbyID(int ID)
        {
            return Json(_context.Usuario.FirstOrDefault(x => x.UsuarioId == ID));
        }
        public JsonResult Update(Usuario user)
        {
            var data = _context.Usuario.FirstOrDefault(x => x.UsuarioId == user.UsuarioId);
            if (data != null)
            {
                data.Nombre = user.Nombre;
                data.NombreUsuario = user.NombreUsuario;
                data.Estado = user.Estado;                
                _context.SaveChanges();
            }
            return Json(data);
        }
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
