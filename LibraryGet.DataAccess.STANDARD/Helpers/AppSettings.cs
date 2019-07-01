using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryGet.DataAccess.STANDARD.Helpers
{
    public class AppSettings
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; }

        #endregion
    }
}
