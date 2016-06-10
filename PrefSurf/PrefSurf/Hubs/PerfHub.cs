using Microsoft.AspNet.SignalR;

namespace PrefSurf.Hubs
{
    public class PerfHub : Hub
    {
        public void Send(string message)
        {
            Clients.All.newMessage(
                Context.User.Identity.Name + " says " + message
                );
        }
    }
}