using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryGet.Model.STANDARD;
using LibraryGet.Web.CORE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryGet.Web.CORE.Pages.BooksReservation
{
    public class IndexModel : PageModel
    {
        public IndexModel(IBookReservationService bookReservationService)
        {
            BookReservationService = bookReservationService;
        }

        [TempData]
        public string Message { get; set; }

        public List<BookReservation> BookReservations { get; set;}
        public IBookReservationService BookReservationService { get; }

        public async Task OnGetAsync()
        {
            BookReservations = await BookReservationService.BookReservationReadAllAsync();
        }

        public async Task<IActionResult> OnPostApproveAsync(int bookReservationID, int bookReservationStatusID, int bookID, string bookName, string userName)
        {
            bool result = await BookReservationService.UpdateAsync(bookReservationID, bookReservationStatusID, bookID, bookName, userName);

            if (result)
            {
                Message = "New reservation is approved.";
            }
            else
            {
                Message = "Approving reservation failed, please try again.";
            }

            return RedirectToPage("/BooksReservation/Index");
        }

        public async Task<IActionResult> OnPostReturnAsync(int bookReservationID, int bookReservationStatusID, int bookID, string bookName, string userName)
        {
            bool result = await BookReservationService.UpdateAsync(bookReservationID, bookReservationStatusID, bookID, bookName, userName);

            if (result)
            {
                Message = "Book returned properly.";
            }
            else
            {
                Message = "Returning book failed, please try again.";
            }

            return RedirectToPage("/BooksReservation/Index");
        }
    }
}