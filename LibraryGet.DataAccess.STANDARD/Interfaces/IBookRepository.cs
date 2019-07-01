using LibraryGet.Model.STANDARD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryGet.DataAccess.STANDARD.Interfaces
{
    public interface IBookRepository
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

        Task<bool> DeleteBookAsync(int bookID);

        /// <summary>
        /// Books the update count asynchronous.
        /// </summary>
        /// <param name="bookID">The book identifier.</param>
        /// <param name="bookReservationStatus">The book reservation status.</param>
        /// <returns></returns>
        Task<bool> BookUpdateCountAsync(int bookID, int bookReservationStatusID);
    }
}
