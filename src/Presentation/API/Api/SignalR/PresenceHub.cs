using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Api.SignalR
{
    [Authorize]
    public class PresenceHub:Hub
    {
        private readonly PresenceTracker presence;

        public PresenceHub(PresenceTracker presence)
        {
            this.presence = presence;
        }
        public override async Task OnConnectedAsync()
        {
         

            var userName=Context?.User?.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.GivenName)?.Value;
            await presence.UserConnected(userName, Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOnline",userName);
            var currentUsers = await presence.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userName = Context?.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            await presence.UserDisconnected(userName, Context.ConnectionId);
            await Clients.Others.SendAsync("UserIsOffline", userName);

            var currentUsers = await presence.GetOnlineUsers();
            await Clients.All.SendAsync("GetOnlineUsers", currentUsers);

            await base.OnDisconnectedAsync(exception);
         


        }
    }
}
