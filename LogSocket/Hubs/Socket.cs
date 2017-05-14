using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LogSocket.Hubs
{
    public class Socket : Hub
    {
        public static HashSet<string> Subscribers = new HashSet<string>();

        public async Task Subscribe(string stream)
        {
            await Groups.Add(Context.ConnectionId, stream);
        }

        public async Task Unsubscribe(string stream)
        {
            await Groups.Remove(Context.ConnectionId, stream);
        }

        public void WriteLog(string stream, string message)
        {
            Subscribers.Add(stream);
            Clients.Group(stream).addMessage(message);
        }
    }
}