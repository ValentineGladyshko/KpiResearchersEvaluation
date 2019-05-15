namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KpiResearchersContext : DbContext
    {
        public KpiResearchersContext()
            : base("name=DatabaseModel")
        {
        }

        public virtual DbSet<Chairs> Chairs { get; set; }
        public virtual DbSet<Faculties> Faculties { get; set; }
        public virtual DbSet<GoogleSchoolars> GoogleSchoolars { get; set; }
        public virtual DbSet<OrcidAccounts> OrcidAccounts { get; set; }
        public virtual DbSet<PublicationReasearchers> PublicationReasearchers { get; set; }
        public virtual DbSet<ResearcherOrcids> ResearcherOrcids { get; set; }
        public virtual DbSet<Researchers> Researchers { get; set; }
        public virtual DbSet<ResearcherScopuses> ResearcherScopuses { get; set; }
        public virtual DbSet<ScopusAccountPublications> ScopusAccountPublications { get; set; }
        public virtual DbSet<ScopusAccounts> ScopusAccounts { get; set; }
        public virtual DbSet<ScopusPublications> ScopusPublications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chairs>()
                .HasMany(e => e.Researchers)
                .WithRequired(e => e.Chairs)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Faculties>()
                .HasMany(e => e.Chairs)
                .WithRequired(e => e.Faculties)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrcidAccounts>()
                .HasMany(e => e.ResearcherOrcids)
                .WithRequired(e => e.OrcidAccounts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Researchers>()
                .HasMany(e => e.PublicationReasearchers)
                .WithRequired(e => e.Researchers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Researchers>()
                .HasMany(e => e.ResearcherOrcids)
                .WithRequired(e => e.Researchers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Researchers>()
                .HasMany(e => e.ResearcherScopuses)
                .WithRequired(e => e.Researchers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ScopusAccounts>()
                .HasMany(e => e.ResearcherScopuses)
                .WithRequired(e => e.ScopusAccounts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ScopusAccounts>()
                .HasMany(e => e.ScopusAccountPublications)
                .WithRequired(e => e.ScopusAccounts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ScopusPublications>()
                .HasMany(e => e.PublicationReasearchers)
                .WithRequired(e => e.ScopusPublications)
                .HasForeignKey(e => e.PublicationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ScopusPublications>()
                .HasMany(e => e.ScopusAccountPublications)
                .WithRequired(e => e.ScopusPublications)
                .WillCascadeOnDelete(false);
        }
    }
}
