namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScopusAccounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScopusAccounts()
        {
            ResearcherScopuses = new HashSet<ResearcherScopuses>();
            ScopusAccountPublications = new HashSet<ScopusAccountPublications>();
        }

        [Key]
        public int ScopusAccountId { get; set; }

        public int ScopusId { get; set; }

        public int HIndex { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearcherScopuses> ResearcherScopuses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScopusAccountPublications> ScopusAccountPublications { get; set; }
    }
}
