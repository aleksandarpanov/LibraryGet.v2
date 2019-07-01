using LibraryGet.DataAccess.STANDARD.Interfaces;
using LibraryGet.Model.STANDARD;
using LibraryGet.Web.CORE.Areas.Identity.Data;
using LibraryGet.Web.CORE.Hubs;
using LibraryGet.Web.CORE.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace LibraryGet.Web.CORE.Services
{
    public class BookReservationService : IBookReservationService
    {
        private readonly IHubContext<ChatHub> singlarHubContext;
        

        public BookReservationService(UserManager<ApplicationUser> userManager, IBookReservationRepository bookReservationRepository, IBookRepository bookRepository, IHubContext<ChatHub> hubContext)
        {
            UserManager = userManager;
            DataManager = bookReservationRepository;
            BookDataManager = bookRepository;
            singlarHubContext = hubContext;
        }

        #region Public Properties

        public IBookReservationRepository DataManager { get; }

        public IBookRepository BookDataManager { get; }

        public UserManager<ApplicationUser> UserManager { get; }

        #endregion

        #region IBookReservationService
        public Task<List<BookReservation>> BookReservationReadAllAsync()
        {
            return DataManager.BookReservationReadAllAsync();
        }

        public async Task<BookReservation> CreateAsync(int bookID, string appUserID)
        {
            BookReservation bookReservation = await DataManager.CreateAsync(bookID, appUserID);

            if (bookReservation != null && bookReservation.BookReservationID > 0)
            {
                var usersOfRole = await UserManager.GetUsersInRoleAsync("Admin");
                if (usersOfRole.Count > 0)
                {
                    await singlarHubContext.Clients.User(usersOfRole[0].UserName).SendAsync("BookRequest", bookReservation);
                }
            }

            return bookReservation;
        }

        public async Task<bool> UpdateAsync(int bookReservationID, int bookReservationStatusID, int bookID, string bookName, string userName)
        {
            bool result = false;

            try
            {
                using (var txscope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    result = await DataManager.UpdateAsync(bookReservationID, bookReservationStatusID);
                    if (result)
                    {

                        result = await BookDataManager.BookUpdateCountAsync(bookID, bookReservationStatusID);

                        if (result)
                        {
                            txscope.Complete();

                            if (bookReservationStatusID == (int)BookReservationStatusEnum.Reserved)
                            {
                                await singlarHubContext.Clients.User(userName).SendAsync("BookResponse", bookName);
                            }
                            else if (bookReservationStatusID == (int)BookReservationStatusEnum.Returned)
                            {
                                await singlarHubContext.Clients.User(userName).SendAsync("BookReturned", bookName);
                            }
                        }
                        else
                        {
                            txscope.Dispose();
                        }
                    }
                    else
                    {
                        txscope.Dispose();
                    }
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
