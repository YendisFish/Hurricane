using Hurricane.Client.Network;
using Hurricane.Client.Security;
using System.Net;
using System.Net.Sockets;
using CSL.Encryption;
using Newtonsoft.Json;

namespace Hurricane.Client
{
    public class EntryPoint
    {
        public static async Task Main()
        {
            Console.Write("Enter a username > ");
            string? username = Console.ReadLine();

            Wrapper wrapper = new Wrapper();

            Client.Network.Client c = new Hurricane.Client.Network.Client(wrapper, IPAddress.Parse("127.0.0.1"), 28736);
            TcpClient cli = await c.Connect();
            Connection con = new Connection(cli, new StreamWriter(cli.GetStream(), System.Text.Encoding.ASCII), new StreamReader(cli.GetStream(), System.Text.Encoding.ASCII));

            ThreadStart ts = new ThreadStart(async ()=> await ListenForData(con));
            Thread t = new Thread(ts);
            t.Start();

            while(true)
            {
                Console.WriteLine($"Enter Message | {username} > ");
                string? content = Console.ReadLine();
                
                Message m = new Message(username ?? "user", DateTime.Now, content ?? "");

                DataPacket pc = new DataPacket(DataType.POST, JsonConvert.SerializeObject(m), wrapper.Protector.GetKey());
                Console.WriteLine(await DataHandler.SerializeData(pc));
                await con.WriteLine(await DataHandler.SerializeData(pc));
            }
        }

        public static async Task ListenForData(Connection con)
        {
            while(true)
            {
                while(con.client.GetStream().DataAvailable)
                {
                    foreach(char dat in await con.ReadLine())
                    {
                        Console.Write(dat);
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
