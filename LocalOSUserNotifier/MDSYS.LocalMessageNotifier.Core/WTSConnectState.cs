namespace MDSYS.LocalMessageNotifier.Core
{
    // <summary>
    /// Specifies the connection state of a Remote Desktop Services session.
    /// </summary>
    public enum WTSConnectState
    {
        Active,
        Connected,
        ConnectQuery,
        Shadow,
        Disconnected,
        Idle,
        Listen,
        Reset,
        Down,
        Init
    }
}
