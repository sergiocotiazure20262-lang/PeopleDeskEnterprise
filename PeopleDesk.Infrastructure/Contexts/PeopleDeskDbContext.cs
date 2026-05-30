using Microsoft.EntityFrameworkCore;
using PeopleDesk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Infrastructure.Contexts
{
    public class PeopleDeskDbContext : DbContext
    {
        public PeopleDeskDbContext(DbContextOptions<PeopleDeskDbContext> options)
            : base(options)
        {
        }

        public DbSet<Chamado> Chamados => Set<Chamado>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PeopleDeskDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
