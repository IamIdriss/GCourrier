#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GCourrier.Shared;

namespace GCourrier.Server.Data
{
    public class GCourrierDbContext : DbContext
    {
        public GCourrierDbContext (DbContextOptions<GCourrierDbContext> options)
            : base(options)
        {
        }

        public DbSet<GCourrier.Shared.Agent> Agent { get; set; }

        public DbSet<GCourrier.Shared.Department> Department { get; set; }
    }
}
