namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScopusAccountPublications
    {
        [Key]
        public int ScopusAccountPublicationId { get; set; }

        public int ScopusAccountId { get; set; }

        public int ScopusPublicationId { get; set; }

        public virtual ScopusAccounts ScopusAccounts { get; set; }

        public virtual ScopusPublications ScopusPublications { get; set; }
    }
}
