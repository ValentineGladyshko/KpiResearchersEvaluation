namespace DatabaseLibrary.DatabaseModel
{
    using DatabaseLibrary.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class OrcidAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrcidAccount()
        {
            OrcidPublications = new HashSet<OrcidPublication>();
            ResearcherOrcids = new HashSet<ResearcherOrcid>();
        }

        public int OrcidAccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrcId { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string PublonsId { get; set; }

        [StringLength(50)]
        public string ScopusAuthorId { get; set; }

        [StringLength(200)]
        public string PublonsLink { get; set; }

        [StringLength(200)]
        public string ScopusLink { get; set; }

        public int PublicationsCount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrcidPublication> OrcidPublications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResearcherOrcid> ResearcherOrcids { get; set; }

        public override string ToString()
        {
            string result = "Name: " + FullName + "\t orcid: " + OrcId + "\t publons id: " + PublonsId +
                "\t scopus author id:" + ScopusAuthorId + "\t publons link:" + PublonsLink + 
                "\t scopus link:" + ScopusLink + "\n===== publications =====";
            foreach (var item in OrcidPublications)
                result += "\n\n" + item.ToString();

            return result;
        }
        public int Save(IUnitOfWork unitOfWork)
        {
            var list = unitOfWork.OrcidAccounts.Find(item => item.OrcId == OrcId).ToList();
            if (list.Count == 0)
            {
                unitOfWork.OrcidAccounts.Create(this);
                unitOfWork.Save();
            }

            else
            {
                OrcidAccountId = unitOfWork.OrcidAccounts.Find(item => item.OrcId == OrcId).First().OrcidAccountId;
            }

            var publicationsList = unitOfWork.OrcidPublications.GetAll().Where(item => item.OrcidAccountId == OrcidAccountId).ToList();
            foreach (var publication in OrcidPublications)
            {
                var accountPublication = publicationsList.Find(item => item.Title ==
                    publication.Title);
                if (accountPublication == null)
                {
                    unitOfWork.OrcidPublications.Create(publication);
                    unitOfWork.Save();
                }                
            }

            return OrcidAccountId;
        }
    }
}
