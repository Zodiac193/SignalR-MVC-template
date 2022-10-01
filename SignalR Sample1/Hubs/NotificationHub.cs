using Microsoft.AspNetCore.SignalR;

namespace SignalR_Sample1.Hubs
{
    public class NotificationHub : Hub
    {
        public static int notificationCounter = 0;
        public static List<string> messages = new();

        public async Task SendMessege(string messege)
        {
            if (!string.IsNullOrEmpty(messege))
            {
                notificationCounter++;
                messages.Add(messege);
                await LoadMessages();
            }
        }

        public async Task LoadMessages()
        {
            await Clients.All.SendAsync("LoadNotification",messages,notificationCounter);
        }
    }
}
