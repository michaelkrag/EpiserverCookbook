using MovieShop.Business.Repository.EvidenceCollector.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MovieShop.Business.Repository.EvidenceCollector
{
    public class EvidenceContext : DbContext
    {
        public EvidenceContext() : base("EPiServerDB")
        {
        }

        public DbSet<Evidence> Evidences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}