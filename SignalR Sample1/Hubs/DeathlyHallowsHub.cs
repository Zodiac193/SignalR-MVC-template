using Microsoft.AspNetCore.SignalR;

namespace SignalR_Sample1.Hubs
{
    public class DeathlyHallowsHub : Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return SD.DeathlyHallowRace;
        }



    }
}
