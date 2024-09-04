using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Service.Interface;

namespace TravelAgencyApplication.Service.Implementation
{
    public class BookService: IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        public BookService(IRepository<Book> countryRepository)
        {
            _bookRepository = countryRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll().ToList();
        }

        public Book GetDetailsForBook(Guid? id)
        {
            return _bookRepository.Get(id);
        }

    }
}
