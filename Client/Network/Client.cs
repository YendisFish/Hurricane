using Hurricane.Client.Security;
using System.Net;
using System.Net.Sockets;

namespace Hurricane.Client.Network
{
    public class Client
    {
        public Wrapper Wrapper { get; set; }
        public IPAddress IP { get; set; }
        public int Port { get; set; }

        public Client(Wrapper wrapper, IPAddress ip, int port)
        {
            Wrapper = wrapper;
            IP = ip;
            Port = port;
        }

        public async Task<TcpClient> Connect()
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync(IP, Port);

            return client;
        }
    }
}