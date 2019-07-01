using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Model.STANDARD;
using LibraryGet.Web.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Services
{
    public class BookService : IBookService
    {
        public BookService(IBookRepository bookRepository)
        {
            DataManager = bookRepository;
        }

        #region Public Properties

        /// <summary>
        /// Gets the book manager.
        /// </summary>
        /// <value>
        /// The book manager.
        /// </value>
        public IBookRepository DataManager { get; }

        #endregion

        #region IBookService

        public Task<List<Book>> BookReadAllAsAdminAsync(int pageNumber)
        {
            return DataManager.BookReadAllAsAdminAsync(pageNumber);
        }

        public Task<List<Book>> BookReadAllAsUserAsync(string id, int pageNumber)
        {
            return DataManager.BookReadAllAsUserAsync(id, pageNumber);
        }
        public Task<int> BookGetCount()
        {
            return DataManager.BookGetCount();
        }

        public Task<Book> CreateBookAsync(Book book)
        {
            return DataManager.CreateBookAsync(book);
        }

        #endregion
    }
}
