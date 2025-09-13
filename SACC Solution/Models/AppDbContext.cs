using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace SACC_Solution.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Ventas> ventas { get; set; }
        public DbSet<MetodoPago> MetodoPago { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
