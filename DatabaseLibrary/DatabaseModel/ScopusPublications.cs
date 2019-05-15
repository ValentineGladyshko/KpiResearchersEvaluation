namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScopusPublications
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScopusPublications()
        {
            PublicationReasearchers = new HashSet<PublicationReasearchers>();
            ScopusAccountPublications = new HashSet<ScopusAccountPublications>();
        }

        [Key]
        public int ScopusPublicationId { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Source { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int TimesCited { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PublicationReasearchers> PublicationReasearchers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScopusAccountPublications> ScopusAccountPublications { get; set; }
    }
}
