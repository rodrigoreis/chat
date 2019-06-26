using Chat.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static Dictionary<string, User> _connections = new Dictionary<string, User>();

        public override Task OnConnectedAsync()
        {
            var user = JsonConvert.DeserializeObject<User>(Context.GetHttpContext().Request.Query["user"]);
            if (!_connections.ContainsKey(Context.ConnectionId)) _connections.Add(Context.ConnectionId, user);
            var allUsers = (from con in _connections select con.Value).ToList();
            Clients.All.SendAsync("chat", allUsers, user);
            return base.OnConnectedAsync();
        }


        public async Task SendMessage(ChatMessage chat)
        {
            var userid = (from con in _connections where con.Value.key == chat.destination select con.Key).FirstOrDefault();
            await Clients.Client(userid).SendAsync("Receive", chat.sender, chat.message);
        }
    }
}