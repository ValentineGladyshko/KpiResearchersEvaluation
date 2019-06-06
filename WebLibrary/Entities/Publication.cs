using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary.DatabaseModel;
using DatabaseLibrary.Interfaces;

namespace WebLibrary.Entities
{
    public class Publication
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public DateTime? Date { get; set; }
        public int CitationCount { get; set; }
        public string Authors { get; set; }

        public override string ToString()
        {
            if (Date != null)
                return "title: " + Title
                    + "\n\tsource: " + Source
                    + "\n\tdate: " + Date.Value.Year
                    + "\n\tcitation count: " + CitationCount
                    + "\n\tauthors: " + Authors;
            else
                return "title: " + Title
                    + "\n\tsource: " + Source
                    + "\n\tdate: \n\tcitation count: " + CitationCount
                    + "\n\tauthors: " + Authors;
        }

        public DatabaseLibrary.DatabaseModel.Publication ToPublication()
        {
            return new DatabaseLibrary.DatabaseModel.Publication
            {
                Title = Title,
                Source = Source,
                Date = Date,
                CitationCount = CitationCount,
                Authors = Authors
            };
        }

        public int Create(IUnitOfWork unitOfWork)
        {
            var publication = ToPublication();
            unitOfWork.Publications.Create(publication);
            unitOfWork.Save();
            return publication.PublicationId;
        }
    }
}
