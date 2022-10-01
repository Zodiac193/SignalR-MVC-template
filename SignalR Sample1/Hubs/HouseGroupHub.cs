using Microsoft.AspNetCore.SignalR;

namespace SignalR_Sample1.Hubs
{
    public class HouseGroupHub : Hub
    {
        public static List<string> GroupsJoined { get; set; }= new List<string>();

        //Join House
        public async Task JoinHouse(string houseName)
        {
            if (!GroupsJoined.Contains(Context.ConnectionId+":"+houseName))
            {
                GroupsJoined.Add(Context.ConnectionId + ":" + houseName);
                //do something else
                string houseList = "";
                foreach (var str in GroupsJoined)
                {
                    if (str.Contains(Context.ConnectionId))
                    {
                        houseList += str.Split(":")[1] + " ";
                    }

                }

                await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName.ToLower(), true);
                await Clients.Others.SendAsync("newMemberAddedToHouse", houseName);
                await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
            }
        }

        //Leave House
        public async Task LeaveHouse(string houseName)
        {
            if (GroupsJoined.Contains(Context.ConnectionId + ":" + houseName))
            {
                GroupsJoined.Remove(Context.ConnectionId + ":" + houseName);

                //do something else
                string houseList = "";
                foreach (var str in GroupsJoined)
                {
                    if (str.Contains(Context.ConnectionId))
                    {
                        houseList += str.Split(":")[1] + " ";
                    }

                }

                await Clients.Caller.SendAsync("subscriptionStatus", houseList, houseName.ToLower(), false);
                await Clients.Others.SendAsync("newMemberRemovedToHouse", houseName);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
            }
        }
        //triggerHouseNotification
        public async Task TriggerHouseNotify(string houseName)
        {
              await Clients.Group(houseName).SendAsync("triggerHouseNotification", houseName);
        }
    }
}
