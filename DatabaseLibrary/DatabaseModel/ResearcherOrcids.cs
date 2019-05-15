namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ResearcherOrcids
    {
        [Key]
        public int ResearcherOrcidId { get; set; }

        public int ResearcherId { get; set; }

        public int OrcidAccountId { get; set; }

        public virtual OrcidAccounts OrcidAccounts { get; set; }

        public virtual Researchers Researchers { get; set; }
    }
}
