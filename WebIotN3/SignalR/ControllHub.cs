using Microsoft.AspNetCore.SignalR;

namespace WebIotN3.SignalR
{
    public class ControllHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendControl(string data)
        {
            
            await Clients.All.SendAsync("ReceiveControl", data);
        }

    }
}
