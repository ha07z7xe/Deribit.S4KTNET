namespace Deribit.S4KTNET.Core
{
    public enum ReconnectionType
    {
        Initial = 0,
        Lost = 1,
        NoMessageReceived = 2,
        Error = 3,
        ByUser = 4,
    }
}