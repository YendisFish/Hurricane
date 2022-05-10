namespace Hurricane.Client.Network
{
    public record DataPacket(DataType? DT, string? Data, byte[]? Key);
}