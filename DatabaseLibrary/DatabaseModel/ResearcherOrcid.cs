namespace DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ResearcherOrcid
    {
        public int ResearcherOrcidId { get; set; }

        public int ResearcherId { get; set; }

        public int OrcidAccountId { get; set; }

        public virtual OrcidAccount OrcidAccount { get; set; }

        public virtual Researcher Researcher { get; set; }
    }
}
