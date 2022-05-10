using System.Net.Sockets;

namespace Hurricane.Server.Security
{
    public record Connection(Guid ConnectionId, TcpClient c);
}