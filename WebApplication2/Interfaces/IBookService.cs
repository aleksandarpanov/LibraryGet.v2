using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Model.STANDARD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> BookReadAllAsAdminAsync(int pageNumber);

        Task<List<Book>> BookReadAllAsUserAsync(string id, int pageNumber);

        Task<int> BookGetCount();

        /// <summary>
        /// Creates the book asynchronous.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>Returns a book.</returns>
        Task<Book> CreateBookAsync(Book book);
    }
}
