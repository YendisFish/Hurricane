using System.Net.Sockets;
using System.Net;
using Hurricane.Server.Types;

namespace Hurricane.Server.Security
{
    public class ThreadedServer
    {
        public Wrapper Wrapper { get; set; }
        public int Port { get; set; }
        public List<Connection> Connections { get; private set; }
        
        public ThreadedServer(Wrapper wrapper, int port)
        {
            Wrapper = wrapper;
            Port = port;
            Connections = new List<Connection>();
        }

        public async Task Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, Port);

            listener.Start();
            Console.WriteLine("Listening...");

            while(true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();

                ThreadStart start = new ThreadStart(async ()=> await HandleSocket(client));
                Thread t = new Thread(start);
                t.Start();
            }
        }

        public async Task HandleSocket(TcpClient client)
        {
            Connection me = new Connection(Guid.NewGuid(), client);
            Connections.Add(me);

            Console.WriteLine("New connection!");

            NetworkStream stream = client.GetStream();
            StreamWriter w = new StreamWriter(stream, System.Text.Encoding.ASCII);
            StreamReader r = new StreamReader(stream, System.Text.Encoding.ASCII);

            while(client.Connected)
            {
                string? data = await r.ReadLineAsync();

                if(data != "" && data?.Length > 0)
                {
                    DataPacket dp = await DataHandler.DeserializeData(data) ?? new DataPacket(DataType.EMPTY, null);

                    if(dp.DT == DataType.POST)
                    {
                        Console.WriteLine(dp.Data);
                        foreach(Connection con in Connections)
                        {
                            if(con.ConnectionId == me.ConnectionId)
                            {
                                continue;
                            }

                            Console.WriteLine("Sending data to client: " + con.ConnectionId);
                            await Post(con, dp);
                        }
                    }
                }
            }
        }

        public static async Task Post(Connection con, DataPacket dp)
        {
            NetworkStream stream = con.c.GetStream();
            StreamWriter w = new StreamWriter(stream, System.Text.Encoding.ASCII);

            await w.WriteLineAsync(await DataHandler.SerializeData(dp));
            await w.FlushAsync();
        }
    }
}