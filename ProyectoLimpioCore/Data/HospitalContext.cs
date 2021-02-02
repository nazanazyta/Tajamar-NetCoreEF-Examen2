using Microsoft.EntityFrameworkCore;
using ProyectoLimpioCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoLimpioCore.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options)
            : base(options) { }

        public DbSet<Coche> Coches { get; set; }
    }
}
