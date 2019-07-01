using LibraryGet.Web.CORE.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryGet.Web.CORE.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Books/Index");
            }
            else
            {
                return LocalRedirect("/Identity/Account/Login");
            }
        }
    }
}
