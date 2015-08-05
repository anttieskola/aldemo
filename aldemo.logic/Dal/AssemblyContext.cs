using aldemo.logic.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace aldemo.logic.Dal
{
    public class AssemblyContext : DbContext
    {
        public AssemblyContext()
            : base("DbConnection")
        {
            // no-op
        }

        protected override void OnModelCreating(DbModelBuilder mb)
        {
            // nicer name schema
            mb.HasDefaultSchema("aldemo");
            // many to many
            mb.Entity<Project>()
                .HasMany<Line>(p => p.Lines)
                .WithMany(l => l.Projects)
                .Map(pl => 
                {
                    pl.MapLeftKey("ProjectId");
                    pl.MapRightKey("LineId");
                    pl.ToTable("ProjectLines");
                });
            base.OnModelCreating(mb);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}
