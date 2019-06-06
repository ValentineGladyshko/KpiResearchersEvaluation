namespace DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AccountPublication
    {
        public int AccountPublicationId { get; set; }

        public int AccountId { get; set; }

        public int PublicationId { get; set; }

        public virtual Account Account { get; set; }

        public virtual Publication Publication { get; set; }
    }
}
