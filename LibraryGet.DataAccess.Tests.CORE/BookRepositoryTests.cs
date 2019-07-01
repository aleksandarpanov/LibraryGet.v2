using FluentAssertions;
using LibraryGet.DataAccess.STANDARD.Dapper;
using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Model.STANDARD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGet.DataAccess.Tests.CORE
{
    [TestClass]
    public class BookRepositoryTests : RepositoryTestBase<BookRepository>
    {
        #region Test Methods

        /// <summary>
        /// Gets all books should return more than zero.
        /// </summary>
        [TestMethod]
        public Task Book_ReadAll_As_Admin_Test()
        {
            // arange
            IBookRepository bookRepository = CreateRepository(Init());

            // act
            List<Book> books = bookRepository.BookReadAllAsAdminAsync(1).GetAwaiter().GetResult();

            // assert
            books.Should().NotBeNull();
            books.Count.Should().BeLessOrEqualTo(10);
            books[0].BookStatus.Should().Equals(string.Empty);
            return Task.FromResult(true);
        }

        [TestMethod]
        public Task Book_ReadAll_As_User_Test()
        {
            // arange
            IBookRepository bookRepository = CreateRepository(Init());
            string userID = "2620e21f-9946-46b7-ada9-dfd7498dc91c";

            // act
            List<Book> books = bookRepository.BookReadAllAsUserAsync(userID, 1).GetAwaiter().GetResult();

            // assert
            books.Should().NotBeNull();
            books.Count.Should().BeLessOrEqualTo(10);
            books[0].BookStatus.Should().NotBe(string.Empty);
            return Task.FromResult(true);
        }

        [TestMethod]
        public Task Book_Get_Count_Test()
        {
            // arange
            IBookRepository bookRepository = CreateRepository(Init());

            // act
            int count = bookRepository.BookGetCount().GetAwaiter().GetResult();

            // assert
            count.Should().NotBe(0);
            count.Should().BeGreaterThan(5);

            return Task.FromResult(true);
        }

        [TestMethod]
        public Task Book_Create_Test()
        {
            // arange
            IBookRepository bookRepository = CreateRepository(Init());
            Book book = new Book { BookName = "Test Book", BookDescription = "Test Desc", Quantity = 4, AuthorName = "Test Author" };

            // act
            Book newBook = bookRepository.CreateBookAsync(book).GetAwaiter().GetResult();

            // assert
            newBook.BookID.Should().BeGreaterThan(0);

            int bookID = newBook.BookID;

            //act
            newBook = bookRepository.CreateBookAsync(book).GetAwaiter().GetResult();

            newBook.BookID.Should().Equals(-1);

            bool result = bookRepository.DeleteBookAsync(bookID).GetAwaiter().GetResult();

            result.Should().BeTrue();

            return Task.FromResult(true);
        }

        #endregion

        //#region Private Methods
        //private IBookRepository CreateRepository(string connectionString)
        //{
        //    return new BookRepository(connectionString);
        //}

        //private string Init()
        //{
        //    var config = new ConfigurationBuilder()
        //    .AddJsonFile("appSettings.json")
        //    .Build();

        //    return config["ConnectionString"];
        //}

        //#endregion
    }
}
