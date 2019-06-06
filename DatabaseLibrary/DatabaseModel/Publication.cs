namespace DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Publication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Publication()
        {
            AccountPublications = new HashSet<AccountPublication>();
        }

        public int PublicationId { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [StringLength(300)]
        public string Source { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int CitationCount { get; set; }

        [StringLength(300)]
        public string Authors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountPublication> AccountPublications { get; set; }
    }
}
