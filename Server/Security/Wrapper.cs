using CSL.Encryption;

namespace Hurricane.Server.Security
{
    public class Wrapper
    {
        public AES256KeyBasedProtector Protector { get; set; }

        public Wrapper()
        {
            Protector = new AES256KeyBasedProtector();
            File.WriteAllBytes("Key.txt", Protector.GetKey());
        }
    }
}