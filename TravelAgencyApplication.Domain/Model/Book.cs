using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyApplication.Domain.Model
{
    public class Book: BaseEntity
    {
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
        public double Price { get; set; }
        public string? BookImage { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorLastName { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherDescription { get; set; }
    }
}
