using LibraryGet.DataAccess.STANDARD.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace LibraryGet.DataAccess.STANDARD.Dapper
{
    public abstract class EntityBaseRepository
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBaseRepository"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public EntityBaseRepository(IOptions<AppSettings> settings)
        {
            Settings = settings.Value;
            DB = new SqlConnection(settings.Value.ConnectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBaseRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public EntityBaseRepository(string connectionString)
        {
            DB = new SqlConnection(connectionString);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public IDbConnection DB { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public AppSettings Settings { get; }

        #endregion
    }
}
