using CSL.Encryption;

namespace Hurricane.Client.Security
{
    public class Wrapper
    {
        public AES256KeyBasedProtector Protector { get; set; }

        public Wrapper()
        {
            byte[] key = File.ReadAllBytes("Key.txt");
            Protector = new AES256KeyBasedProtector(key);
        }
    }
}