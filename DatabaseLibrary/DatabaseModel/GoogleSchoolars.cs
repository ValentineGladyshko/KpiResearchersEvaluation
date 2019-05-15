namespace KpiResearchersEvaluation.DatabaseLibrary.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GoogleSchoolars
    {
        [Key]
        public int GoogleSchoolarId { get; set; }

        public int HIndex { get; set; }

        public int I10Index { get; set; }
    }
}
