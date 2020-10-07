using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Web.Hubs
{
    public class DebugHub : Hub<IDebugClient>
    {
        public async Task SendMessage(string message)
        {
            Console.WriteLine(message);
            await Clients.Caller.ReceiveMessage(message);
        }
    }
}
