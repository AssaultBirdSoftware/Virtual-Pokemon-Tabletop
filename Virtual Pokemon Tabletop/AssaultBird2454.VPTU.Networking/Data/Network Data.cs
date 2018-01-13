namespace AssaultBird2454.VPTU.Networking.Data
{
    internal enum Commands
    {
        SetBufferSize = 1,
        SSL_Enable = 2,
        SSL_Dissable = 3,
        SSL_Active = 4
    }

    internal enum ResponseCode
    {
        OK = 0,
        Ready = 1,
        None = 2,
        Avaliable = 100,
        Not_Implemented = 501,
        Not_Avaliable = 503,
        Forbiden = 403,
        Not_Found = 404,
        Error = 500
    }

    internal class InternalNetworkCommand : NetworkCommand
    {
        public InternalNetworkCommand(Commands _Command, ResponseCode _Response = ResponseCode.None)
        {
            CommandType = _Command;
            Response = _Response;
        }

        public Commands CommandType { get; set; }
        public ResponseCode Response { get; set; }

        public string Command => "Network Command";
    }

    public interface NetworkCommand
    {
        string Command { get; }
    }
}