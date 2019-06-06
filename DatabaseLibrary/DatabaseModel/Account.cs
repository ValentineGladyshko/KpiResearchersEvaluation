namespace DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            AccountPublications = new HashSet<AccountPublication>();
            ResearcherAccounts = new HashSet<ResearcherAccount>();
        }

        public int AccountId { get; set; }

        public int TypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        public int HIndex { get; set; }

        public int I10Index { get; set; }

        public int PublicationsCount { get; set; }

        public int CitationCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountPublication> AccountPublications { get; set; }

        public virtual Type Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearcherAccount> ResearcherAccounts { get; set; }
    }
}
