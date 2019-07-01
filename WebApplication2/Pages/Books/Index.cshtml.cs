using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using JW;
using LibraryGet.Model.STANDARD;
using LibraryGet.Web.CORE.Data;
using LibraryGet.Web.CORE.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryGet.Web.CORE.Pages.Books
{
    public class IndexModel : PageModel
    {

        public static readonly string cacheKey = "_booksCount";

        public IndexModel(IBookService bookService, IBookReservationService bookReservationService, IMemoryCache cache)
        {
            Service = bookService;
            BookReservationService = bookReservationService;
            MemoryCache = cache;
        }

        #region Public Properties

        private IBookService Service { get; }
        private IBookReservationService BookReservationService { get; }

        #endregion

        [TempData]
        public string Message { get; set; }

        public IMemoryCache MemoryCache { get; }

        public PagedResult<Book> Items { get; set; }

        public List<Book> CurrentItems { get; set; }

        public int PageNo { get; set; }

        public Pager Pager { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int MaxPages { get; set; }

        public async Task OnGetAsync(int p = 1)
        {
            PageNo = p;

            if (User.IsInRole("Admin"))
            {
                CurrentItems = await Service.BookReadAllAsAdminAsync(p);
            }
            else
            {
                var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                CurrentItems = await Service.BookReadAllAsUserAsync(userID, p);
            }
            
            if (!MemoryCache.TryGetValue(cacheKey, out int count))
            {
                count = await Service.BookGetCount();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                // Set cache entry size by extension method.
                .SetSize(1)
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                MemoryCache.Set(cacheKey, count, cacheEntryOptions);

                TotalItems = count;
            }
            else
            {
                TotalItems = count;
            }
        }

        public async Task<IActionResult> OnPostReserveAsync(int ID)
        {
            if (ID > 0)
            {
                var userID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                BookReservation bookReservation = await BookReservationService.CreateAsync(ID, userID);
                if (bookReservation != null && bookReservation.BookReservationID > 0)
                {
                    Message = "New reservation is set. Wait for a message from the admin. !";
                }
                else
                {
                    Message = "Saving reservation failed, please try again.";
                }
            }

            return RedirectToPage("/Books/Index");
        }
    }
}