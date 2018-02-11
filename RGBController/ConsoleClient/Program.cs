using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        //Wystarczy go odpalić

        static void Main(string[] args)
        {
            var connection = new HubConnection("http://kolejkomatapp4.azurewebsites.net/signalr/hubs");
            var myHub = connection.CreateHubProxy("mainHub");

            myHub.On<string>("hello", s1 =>
            {
                Console.WriteLine("hello: {0}", s1);
            });

            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                  task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }
            }).Wait();

            Console.ReadLine();
            connection.Stop();
        }
    }
}
