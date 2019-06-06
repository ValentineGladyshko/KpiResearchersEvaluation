using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace WebLibrary.Entities
{
    public class Researcher
    {
        public int TypeId { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public int HIndex { get; set; }
        public int I10Index { get; set; }
        public int CitationCount { get; set; }

        public List<Publication> Publications { get; set; }

        public Researcher()
        {
            Publications = new List<Publication>();
        }

        public override string ToString()
        {
            string result = "Name: " + FullName + "\t id: " + Id + "\t h-index: " + HIndex +
                "\t i10-index :" + I10Index + "\t citation count:" + CitationCount + "\n===== publications =====";
            foreach (var item in Publications)
                result += "\n\n" + item.ToString();

            return result;
        }

        public Account ToAccount()
        {
            return new Account
            {
                FullName = FullName,
                TypeId = TypeId,
                UserId = Id,
                HIndex = HIndex,
                I10Index = I10Index,
                PublicationsCount = Publications.Count,
                CitationCount = CitationCount
            };
        }

        public int Save(IUnitOfWork unitOfWork)
        {
            var account = ToAccount();
            var list = unitOfWork.Accounts.Find(item => item.UserId == account.UserId && item.TypeId == account.TypeId).ToList();
            if (list.Count == 0)
            {
                unitOfWork.Accounts.Create(account);
                unitOfWork.Save();
            }
            else
            {
                var updateAccount = list.First();

                updateAccount.CitationCount = account.CitationCount;
                updateAccount.HIndex = account.HIndex;
                updateAccount.I10Index = account.I10Index;

                unitOfWork.Accounts.Update(updateAccount);
                unitOfWork.Save();

                account = updateAccount;                
            }

            var publicationsList = unitOfWork.AccountPublications.GetAll().Where(item => item.Account.TypeId == account.TypeId).ToList();
            foreach (var publication in Publications)
            {
                var accountPublication = publicationsList.Find(item => item.Publication.Title ==
                    publication.Title && item.Publication.Source == publication.Source);
                if (accountPublication == null)
                {
                    var publicationId = publication.Create(unitOfWork);

                    unitOfWork.AccountPublications.Create(new AccountPublication()
                    {
                        AccountId = account.AccountId,
                        PublicationId = publicationId
                    });
                    unitOfWork.Save();
                }

                else
                {
                    var updatePublication = accountPublication.Publication;
                    updatePublication.CitationCount = publication.CitationCount;

                    unitOfWork.Publications.Update(updatePublication);
                    unitOfWork.Save();
                }
            }

            return account.AccountId;
        }
    }
}
