using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryGet.Model.STANDARD
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [Display(Name = "Book Name")]
        [StringLength(256, ErrorMessage ="Name should be between 2 and 256 char in length", MinimumLength = 2)]
        public string BookName { get; set; }

        [Required]
        [Display(Name = "Book Description")]
        [StringLength(2048, ErrorMessage = "Name should be between 5 and 2048 char in length", MinimumLength = 5)]
        public string BookDescription { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        [RegularExpression("\\d+", ErrorMessage = "Please enter valid number.")]
        public int Quantity { get; set; }

        public int Booked { get; set; }

        public int Available { get; set; }
        [Required]
        [Display(Name = "Author Name")]
        [StringLength(128, ErrorMessage = "Name should be between 2 and 128 char in length", MinimumLength = 2)]
        public string AuthorName { get; set; }

        public string BookStatus { get; set; }

    }
}
