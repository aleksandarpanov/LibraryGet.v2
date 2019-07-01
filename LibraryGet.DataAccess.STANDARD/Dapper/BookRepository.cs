using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LibraryGet.DataAccess.STANDARD.Helpers;
using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Model.STANDARD;
using Microsoft.Extensions.Options;

namespace LibraryGet.DataAccess.STANDARD.Dapper
{
    public class BookRepository : EntityBaseRepository, IBookRepository
    {
        #region Constructors

        public BookRepository(IOptions<AppSettings> settings) : base(settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppUserRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public BookRepository(string connectionString) : base(connectionString)
        {
        }

        #endregion

        #region IBookRespository

        public async Task<List<Book>> BookReadAllAsAdminAsync(int pageNumber)
        {
            var result = await DB.QueryAsync<Book>("BookReadAllAsAdmin_sp", new { PageNumber = pageNumber },  commandType: CommandType.StoredProcedure);

            if (result == null && result.ToList().Count == 0)
            {
                return null;
            }

            return result.ToList();
        }

        public async Task<List<Book>> BookReadAllAsUserAsync(string id, int pageNumber)
        {
            var result = await DB.QueryAsync<Book>("BookReadAllAsUser_SP", new { AppUserID = id, pageNumber }, commandType: CommandType.StoredProcedure);

            if (result == null && result.ToList().Count == 0)
            {
                return null;
            }

            return result.ToList();
        }

        public async Task<int> BookGetCount()
        {
            var result = await DB.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Book");

            return result;
        }

        /// <summary>
        /// Creates the book asynchronous.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        /// Returns a book.
        /// </returns>
        public async Task<Book> CreateBookAsync(Book book)
        {
            var result = await DB.QueryAsync<int>("BookCreate_sp", new { BookName = book.BookName, BookDescription = book.BookDescription, Quantity = book.Quantity, AuthorName = book.AuthorName }, commandType: CommandType.StoredProcedure);

            if (result == null)
            {
                return null;
            }

            book.BookID = result.ToList().FirstOrDefault();

            return book;
        }

        public async Task<bool> DeleteBookAsync(int bookID)
        {
            var result = await DB.ExecuteScalarAsync<bool>("BookDelete_sp", new { BookID = bookID }, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> BookUpdateCountAsync(int bookID, int bookReservationStatusID)
        {
            var result = await DB.ExecuteScalarAsync<bool>("BookUpdateCount_sp", new { BookID = bookID, BookReservationStatusID = bookReservationStatusID }, commandType: CommandType.StoredProcedure);

            return result;
        }

        #endregion
    }
}
