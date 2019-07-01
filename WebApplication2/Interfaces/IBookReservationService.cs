using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Model.STANDARD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Interfaces
{
    public interface IBookReservationService
    {
        /// <summary>
        /// Creates the specified book reservation.
        /// </summary>
        /// <param name="bookReservation">The book reservation.</param>
        /// <returns>Returns a book reservation</returns>
        Task<BookReservation> CreateAsync(int bookID, string appUserID);

        /// <summary>
        /// Updates the specified book reservation.
        /// </summary>
        /// <param name="bookReservation">The book reservation.</param>
        /// <returns>Returns bool, true if update succeed.</returns>
        Task<bool> UpdateAsync(int bookReservationID, int bookReservationStatusID, int bookID, string bookName, string userName);

        /// <summary>
        /// Gets all books by application user identifier and status.
        /// </summary>
        /// <param name="appUserID">The application user identifier.</param>
        /// <param name="bookReservationStatusID">The book reservation status identifier.</param>
        /// <returns>Returns all books by app user.</returns>
        Task<List<BookReservation>> BookReservationReadAllAsync();
    }
}
