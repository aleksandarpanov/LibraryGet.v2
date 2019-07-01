using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryGet.Model.STANDARD
{
    public class BookReservation
    {
        #region Public Properties  

        /// <summary>
        /// Gets or sets the book reservation identifier.
        /// </summary>
        /// <value>
        /// The book reservation identifier.
        /// </value>
        public int BookReservationID { get; set; }

        /// <summary>
        /// Gets or sets the return date.
        /// </summary>
        /// <value>
        /// The return date.
        /// </value>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        public int BookID { get; set; }

        /// <summary>
        /// Gets or sets the name of the book.
        /// </summary>
        /// <value>
        /// The name of the book.
        /// </value>
        public string BookName { get; set; }

        /// <summary>
        /// Gets or sets the application user identifier.
        /// </summary>
        /// <value>
        /// The application user identifier.
        /// </value>
        public string AppUserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the available.
        /// </summary>
        /// <value>
        /// The available.
        /// </value>
        public int Available { get; set; }

        /// <summary>
        /// Gets or sets the book status identifier.
        /// </summary>
        /// <value>
        /// The book status identifier.
        /// </value>
        public int BookReservationStatusID { get; set; }

        /// <summary>
        /// Gets or sets the name of the book status.
        /// </summary>
        /// <value>
        /// The name of the book status.
        /// </value>
        public string BookReservationStatusName { get; set; }

        #endregion
    }
}
