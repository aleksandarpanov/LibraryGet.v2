using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryGet.Model.STANDARD;
using LibraryGet.Web.CORE.Interfaces;
using LibraryGet.Web.CORE.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryGet.Web.CORE.Pages.Books
{
    public class CreateModel : PageModel
    {
        private IBookService bookService;

        [BindProperty]
        public Book NewBook { get; set; }

        [TempData]
        public string Message { get; set; }

        public CreateModel(IBookService service)
        {
            bookService = service;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Book book = await bookService.CreateBookAsync(NewBook);
                if (book != null)
                {
                    Message = "New Book Added Successfully !";
                    return RedirectToPage("/Books/Index");
                }
                else
                {
                    Message = "Saving the book failed, please try again.";
                }
            }

            return Page();
        }
    }
}