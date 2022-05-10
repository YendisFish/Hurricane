using Hurricane.Client.Security;

namespace Hurricane.Client
{
    public class Message
    {
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; private set; }

        public Message(string sender, DateTime timestamp, string content)
        {
            Sender = sender;
            Timestamp = timestamp;
            Content = content;
        }

        public async Task SetContent(string content, Wrapper wrapper)
        {
            string newContent = await wrapper.Protector.Protect(content);
            Content = newContent;
        }
    }
}