namespace DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Researcher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Researcher()
        {
            ResearcherAccounts = new HashSet<ResearcherAccount>();
            ResearcherOrcids = new HashSet<ResearcherOrcid>();
        }

        public int ResearcherId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public int ChairId { get; set; }

        public virtual Chair Chair { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearcherAccount> ResearcherAccounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearcherOrcid> ResearcherOrcids { get; set; }
    }
}
