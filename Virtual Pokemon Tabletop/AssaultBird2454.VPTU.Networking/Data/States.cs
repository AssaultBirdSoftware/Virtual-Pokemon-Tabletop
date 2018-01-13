namespace AssaultBird2454.VPTU.Networking.Data
{
    public enum Client_ConnectionStatus
    {
        Connecting = 1,
        Connected = 2,
        Disconnected = 3,
        Dropped = 4,
        Rejected = 5,
        ServerFull = 6,
        Encrypted = 7
    }

    public enum Server_Status
    {
        Offline = 0,
        Starting = 1,
        Online = 2
    }
}