using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace LogClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            //Waiting for the web server to start
            await Task.Delay(10000);

            var hubConnection = new HubConnection("http://localhost:56042/");
            IHubProxy socketHub = hubConnection.CreateHubProxy("Socket");
            await hubConnection.Start();

            var observable = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            observable.Subscribe(l =>
            {
                for (int i = 0; i < 5; i++)
                {
                    socketHub.Invoke("WriteLog", $"Stream{i}", $"Stream{i} {DateTime.Now}");
                }
            });
            Console.ReadLine();
        }
    }
}
