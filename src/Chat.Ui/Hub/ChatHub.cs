using Chat.Models;
using Chat.Repositories;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static ConnectionsRepository _connections = new ConnectionsRepository();

        public override Task OnConnectedAsync()
        {
            var user = JsonConvert.DeserializeObject<User>(Context.GetHttpContext().Request.Query["user"]);
            _connections.Add(Context.ConnectionId, user);
            Clients.All.SendAsync("chat", _connections.GetAllUser(), user);
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(ChatMessage chat)
        {
            await Clients.Client(_connections.GetUserId(chat.destination)).SendAsync("Receive", chat.sender, chat.message);
        }
    }
}