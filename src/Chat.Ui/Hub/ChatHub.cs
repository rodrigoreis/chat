using Chat.Models;
using Chat.Repositories.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IConnectionsRepository _connectionsRepository;

        public ChatHub(IConnectionsRepository connectionsRepository)
        {
            _connectionsRepository = connectionsRepository;
        }

        public override Task OnConnectedAsync()
        {
            var user = JsonConvert.DeserializeObject<User>(Context.GetHttpContext().Request.Query["user"]);
            _connectionsRepository.Add(Context.ConnectionId, user);
            Clients.All.SendAsync("chat", _connectionsRepository.GetAllUser(), user);
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(ChatMessage chat)
        {
            await Clients.Client(_connectionsRepository.GetUserId(chat.destination)).SendAsync("Receive", chat.sender, chat.message);
        }
    }
}