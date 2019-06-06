using DatabaseLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DatabaseLibrary.DatabaseModel
{  

    public partial class Chair
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chair()
        {
            Researchers = new HashSet<Researcher>();
        }

        public int ChairId { get; set; }

        public int FacultyId { get; set; }

        [StringLength(50)]
        public string ChairName { get; set; }

        public virtual Faculty Faculty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Researcher> Researchers { get; set; }        
    }
}
