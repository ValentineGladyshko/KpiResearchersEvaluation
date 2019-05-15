namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrcidAccounts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrcidAccounts()
        {
            ResearcherOrcids = new HashSet<ResearcherOrcids>();
        }

        [Key]
        public int OrcidAccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrcId { get; set; }

        [StringLength(200)]
        public string Info { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearcherOrcids> ResearcherOrcids { get; set; }
    }
}
