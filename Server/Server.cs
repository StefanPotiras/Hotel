using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleTcp;

namespace Server
{
    class Server
    {
        private SimpleTcpServer _server;

        public Server(string Ip)
        {
            _server = new SimpleTcpServer(Ip);
        }

        public void Start()
        {
            _server.Events.ClientConnected += ClientConnected;
        }

        private void ClientConnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}] client connected");
        }

        private void ClientDisconnected(object sender, ConnectionEventArgs e)
        {
            Console.WriteLine($"[{e.IpPort}] client disconnected: {e.Reason}");
        }

        private void DataReceived(object sender, DataReceivedEventArgs e)
        {

            Console.WriteLine($"[{e.IpPort}]: {Encoding.UTF8.GetString(e.Data)}");
            _server.Send(e.IpPort,"L-am primit boss");
        }
    }
}
