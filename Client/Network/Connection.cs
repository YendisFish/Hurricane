using System.Net.Sockets;

namespace Hurricane.Client.Network
{
    public record Connection(TcpClient client, StreamWriter w, StreamReader r)
    {
        public async Task WriteLine(string data)
        {
            await w.WriteLineAsync(data);
            await w.FlushAsync();
        }

        public async Task<string?> ReadLine()
        {
            string? data = await r.ReadLineAsync();
            return data;
        }
    }
}