using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;
            Clients.User(name).SendAsync("ReceiveMessage", "Connected from server");
            return base.OnConnectedAsync();
        }

        #region Public Methods

        public Task SendPrivateMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);
        }

        #endregion
    }
}
