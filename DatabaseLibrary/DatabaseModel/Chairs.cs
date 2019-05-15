namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Chairs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chairs()
        {
            Researchers = new HashSet<Researchers>();
        }

        [Key]
        public int ChairId { get; set; }

        public int FacultyId { get; set; }

        [StringLength(50)]
        public string ChairName { get; set; }

        public virtual Faculties Faculties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Researchers> Researchers { get; set; }
    }
}
