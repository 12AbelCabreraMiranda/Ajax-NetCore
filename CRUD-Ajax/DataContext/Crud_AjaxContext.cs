using CRUD_Ajax.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Ajax.DataContext
{
    public class Crud_AjaxContext: DbContext
    {
        public Crud_AjaxContext(DbContextOptions<Crud_AjaxContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
