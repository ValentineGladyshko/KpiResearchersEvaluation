namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PublicationReasearchers
    {
        [Key]
        public int PublicationResearcherId { get; set; }

        public int PublicationId { get; set; }

        public int ResearcherId { get; set; }

        public virtual ScopusPublications ScopusPublications { get; set; }

        public virtual Researchers Researchers { get; set; }
    }
}
