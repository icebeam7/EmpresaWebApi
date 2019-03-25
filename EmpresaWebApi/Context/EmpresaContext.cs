using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using EmpresaWebApi.Models;

namespace EmpresaWebApi.Context
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext(DbContextOptions<EmpresaContext> options) : base(options)
        {

        }

        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
    }
}
