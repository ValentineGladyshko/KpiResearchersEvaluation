namespace DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KpiResearchersContext : DbContext
    {        
        private static KpiResearchersContext kpiResearchersContext;
        private KpiResearchersContext() : base("name=KpiResearchersContext")
        {
        }        

        public static KpiResearchersContext GetKpiResearchersContext()
        {
            if (kpiResearchersContext == null)
            {
                kpiResearchersContext = new KpiResearchersContext();
            }

            return kpiResearchersContext;
        }
        public virtual DbSet<AccountPublication> AccountPublications { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Chair> Chairs { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<OrcidAccount> OrcidAccounts { get; set; }
        public virtual DbSet<OrcidPublication> OrcidPublications { get; set; }
        public virtual DbSet<Publication> Publications { get; set; }
        public virtual DbSet<ResearcherAccount> ResearcherAccounts { get; set; }
        public virtual DbSet<ResearcherOrcid> ResearcherOrcids { get; set; }
        public virtual DbSet<Researcher> Researchers { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.AccountPublications)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.ResearcherAccounts)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Chair>()
                .HasMany(e => e.Researchers)
                .WithRequired(e => e.Chair)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Chairs)
                .WithRequired(e => e.Faculty)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrcidAccount>()
                .HasMany(e => e.OrcidPublications)
                .WithRequired(e => e.OrcidAccount)                
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<OrcidAccount>()
                .HasMany(e => e.ResearcherOrcids)
                .WithRequired(e => e.OrcidAccount)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Publication>()
                .HasMany(e => e.AccountPublications)
                .WithRequired(e => e.Publication)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Researcher>()
                .HasMany(e => e.ResearcherAccounts)
                .WithRequired(e => e.Researcher)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Researcher>()
                .HasMany(e => e.ResearcherOrcids)
                .WithRequired(e => e.Researcher)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Type>()
                .Property(e => e.TypeName)
                .IsFixedLength();

            modelBuilder.Entity<Type>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.Type)
                .WillCascadeOnDelete(true);
        }
    }
}
