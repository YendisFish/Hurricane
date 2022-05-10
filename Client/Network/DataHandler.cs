using Newtonsoft.Json;

namespace Hurricane.Client.Network
{
    public class DataHandler
    {
        public static async Task<DataPacket?> DeserializeData(string? data)
        {
            try
            {
                DataPacket? dp = JsonConvert.DeserializeObject<DataPacket>(data ?? "");
                return dp;
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }

        public static async Task<string?> SerializeData(DataPacket? dp)
        {
            string? ret = JsonConvert.SerializeObject(dp);
            return ret;
        }
    }
}