using Dapper;
using LibraryGet.DataAccess.STANDARD.Helpers;
using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Model.STANDARD;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGet.DataAccess.STANDARD.Dapper
{
    public class BookReservationRepository : EntityBaseRepository, IBookReservationRepository
    {
        #region Constructors

        public BookReservationRepository(IOptions<AppSettings> settings) : base(settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public BookReservationRepository(string connectionString) : base(connectionString)
        {
        }

        #endregion

        #region IBookReservationRepository

        /// <summary>
        /// Creates the specified book reservation.
        /// </summary>
        /// <param name="bookReservation">The book reservation.</param>
        /// <returns>
        /// Returns a book reservation
        /// </returns>
        public async Task<BookReservation> CreateAsync(int bookID, string appUserID)
        {
            BookReservation bookReservation = null;
            try
            {
                var result = await DB.QueryAsync<BookReservation>("BookReservationCreate_sp", new { BookID = bookID, AppUserID = appUserID }, commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    bookReservation = result.FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                // throw ex;
            }

            return bookReservation;
        }

        /// <summary>
        /// Gets all books by application user identifier and status.
        /// </summary>
        /// <param name="appUserID">The application user identifier.</param>
        /// <param name="bookReservationStatusID">The book reservation status identifier.</param>
        /// <returns>
        /// Returns all books by app user.
        /// </returns>
        public async Task<List<BookReservation>> BookReservationReadAllAsync()
        {
            List<BookReservation> bookReservations = null;
            try
            {
                var response = await DB.QueryAsync<BookReservation>("BookReservationReadAll_sp", commandType: CommandType.StoredProcedure);

                bookReservations = response.ToList();
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            return bookReservations;
        }

        public async Task<bool> UpdateAsync(int bookReservationID, int bookReservationStatus)
        {
            bool result = false;

            try
            {
                DateTime returnDate = DateTime.MinValue;

                if (bookReservationStatus == (int)BookReservationStatusEnum.Reserved)
                {
                    returnDate = DateTime.Now.AddDays(15);
                }
                else if (bookReservationStatus == (int)BookReservationStatusEnum.Returned)
                {
                    returnDate = DateTime.Now;
                }

                if (returnDate != DateTime.MinValue)
                {
                    var response = await DB.ExecuteAsync("BookReservationUpdate_sp", new { BookReservationID = bookReservationID, ReturnDate = returnDate, BookReservationStatusID = bookReservationStatus }, commandType: CommandType.StoredProcedure);
                    result =  response > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion
    }
}
