using System.Text.Json;
using System.Threading.Tasks;
using Chat.Models;
using Chat.Repositories.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Ui.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IConnectionsRepository _connectionsRepository;

        public ChatHub(IConnectionsRepository connectionsRepository)
        {
            _connectionsRepository = connectionsRepository;
        }

        public override Task OnConnectedAsync()
        {
            var user = JsonSerializer.Deserialize<User>(Context.GetHttpContext().Request.Query["user"]);
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