namespace DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ResearcherAccount
    {
        public int ResearcherAccountId { get; set; }

        public int ResearcherId { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        public virtual Researcher Researcher { get; set; }
    }
}
