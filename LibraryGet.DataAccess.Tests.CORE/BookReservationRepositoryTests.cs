using FluentAssertions;
using LibraryGet.DataAccess.STANDARD.Dapper;
using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Model.STANDARD;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGet.DataAccess.Tests.CORE
{
    [TestClass]
    public class BookReservationRepositoryTests : RepositoryTestBase<BookReservationRepository>
    {
        [TestMethod]
        public Task Book_Reservation_Create_Test()
        {
            // arange
            IBookReservationRepository bookReservationRepository = CreateRepository(Init());
            string appUserID = "2620e21f-9946-46b7-ada9-dfd7498dc91c";
            int bookID = 1;

            // act
            BookReservation bookReservation = bookReservationRepository.CreateAsync(bookID, appUserID).GetAwaiter().GetResult();

            // assert
            bookReservation.BookID.Should().BeGreaterThan(0);
            bookReservation.BookReservationStatusID = (int)BookReservationStatusEnum.Pending;

            // act - try to save again
            bookReservation = bookReservationRepository.CreateAsync(bookID, appUserID).GetAwaiter().GetResult();

            // assert
            bookReservation.BookID.Should().Equals(0);

            return Task.FromResult(true);
        }

        [TestMethod]
        public Task Book_Reservation_Update_Test()
        {
            // arange
            IBookReservationRepository bookReservationRepository = CreateRepository(Init());

            BookReservation bookReservation = new BookReservation() { BookReservationID = 5, ReturnDate = DateTime.Now.AddDays(15), BookReservationStatusID = (int)BookReservationStatusEnum.Reserved };

            // act
            // bool result = bookReservationRepository.UpdateAsync(bookReservation).GetAwaiter().GetResult();

            // assert
            // result.Should().BeTrue();

            return Task.FromResult(true);
        }

        [TestMethod]
        public Task Book_Reservation_Read_All_Test()
        {
            // arange
            IBookReservationRepository bookReservationRepository = CreateRepository(Init());

            // act
            List<BookReservation> bookReservations = bookReservationRepository.BookReservationReadAllAsync().GetAwaiter().GetResult();

            // assert
            bookReservations.Should().NotBeNull();
            bookReservations.Count.Should().BeGreaterOrEqualTo(0);

            return Task.FromResult(true);
        }
    }
}
