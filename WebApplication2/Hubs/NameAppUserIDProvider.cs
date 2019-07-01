using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Hubs
{
    public class NameAppUserIDProvider : IUserIdProvider
    {
        #region IUserIdProvider

        /// <summary>
        /// Gets the user ID for the specified connection.
        /// </summary>
        /// <param name="connection">The connection to get the user ID for.</param>
        /// <returns>
        /// The user ID for the specified connection.
        /// </returns>
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
        }

        #endregion
    }
}
