namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ResearcherScopuses
    {
        [Key]
        public int ResearcherScopusId { get; set; }

        public int ResearcherId { get; set; }

        public int ScopusAccountId { get; set; }

        public virtual Researchers Researchers { get; set; }

        public virtual ScopusAccounts ScopusAccounts { get; set; }
    }
}
