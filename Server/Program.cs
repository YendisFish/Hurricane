using Hurricane.Server.Security;

namespace Hurricane.Server
{
    public class EntryPoint
    {
        public static async Task Main()
        {
            int port = 28736;

            Wrapper wrapper = new();

            ThreadedServer server = new ThreadedServer(wrapper, port);
            await server.Start();
        }
    }
}
