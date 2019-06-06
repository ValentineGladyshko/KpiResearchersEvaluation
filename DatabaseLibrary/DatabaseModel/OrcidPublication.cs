namespace DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrcidPublication
    {
        public int OrcidPublicationId { get; set; }

        public int OrcidAccountId { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }        

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public virtual OrcidAccount OrcidAccount { get; set; }

        public override string ToString()
        {
            if (Date != null)
                return "title: " + Title
                    + "\n\tdate: " + Date.Value.Year;
            else
                return "title: " + Title
                    + "\n\tdate:";
        }
    }
}
