using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalR_Sample1.Data;

namespace SignalR_Sample1.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _db;
        public ChatHub(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task SendMessageToAll(string user , string message)
        {
            await Clients.All.SendAsync("MessageReceived", user, message);
        }

        [Authorize]

        public async Task SendMessageToReciever(string sender, string reciever, string message)
        {
            var userId = _db.Users.FirstOrDefault(u => u.Email.ToLower()==reciever.ToLower()).Id;

            if (!string.IsNullOrEmpty(userId))
            {
                await Clients.User(userId).SendAsync("MessageReceived", sender, message);
            }

          
        }

    }
}
