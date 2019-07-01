using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string BookDescription { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Booked { get; set; }

        [Required]
        public string AuthorName { get; set; }

    }
}
