using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace MDSYS.LocalMessageNotifier.Core
{
    public sealed class UserSessionService : IUserSessionService
    {

        public event EventHandler? SessionsChanged;

        public UserSessionService()
        {
            // Subscribe to system events for automatic refresh
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        public IEnumerable<WindowsUser> GetUsers(params WTSConnectState[] states)
        {
            // If no states are provided, default to only Active sessions.
            var targetStates = (states == null || states.Length == 0)
                ? new HashSet<WTSConnectState> { WTSConnectState.Active }
                : new HashSet<WTSConnectState>(states);

            var loggedInUsers = new List<WindowsUser>();
            IntPtr serverHandle = IntPtr.Zero; // WTS_CURRENT_SERVER_HANDLE
            IntPtr sessionInfoPtr = IntPtr.Zero;

            try
            {
                if (WTSEnumerateSessions(serverHandle, 0, 1, out sessionInfoPtr, out int sessionCount))
                {
                    IntPtr currentSession = sessionInfoPtr;

                    for (int i = 0; i < sessionCount; i++)
                    {
                        WTS_SESSION_INFO info = Marshal.PtrToStructure<WTS_SESSION_INFO>(currentSession);
                        currentSession = IntPtr.Add(currentSession, Marshal.SizeOf(typeof(WTS_SESSION_INFO)));

                        if (WTSQuerySessionInformation(serverHandle, info.SessionId, WTS_INFO_CLASS.WTSConnectState, out IntPtr connectStatePtr, out _))
                        {
                            var connectState = (WTSConnectState)Marshal.ReadInt32(connectStatePtr);
                            WTSFreeMemory(connectStatePtr);

                            // NEW: Filter by the provided list of states.
                            if (targetStates.Contains(connectState))
                            {
                                if (WTSQuerySessionInformation(serverHandle, info.SessionId, WTS_INFO_CLASS.WTSUserName, out IntPtr userNamePtr, out _))
                                {
                                    string? userName = Marshal.PtrToStringAnsi(userNamePtr);
                                    WTSFreeMemory(userNamePtr);

                                    if (!string.IsNullOrEmpty(userName))
                                    {
                                        loggedInUsers.Add(new WindowsUser(userName, info.SessionId.ToString(), connectState.ToString()));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<WindowsUser>();
            }
            finally
            {
                if (sessionInfoPtr != IntPtr.Zero)
                {
                    WTSFreeMemory(sessionInfoPtr);
                }
            }

            return loggedInUsers;
        }

        /// <summary>
        /// Handles system-wide session change events and raises the service's event.
        /// </summary>
        private void SystemEvents_SessionSwitch(object? sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLogon || e.Reason == SessionSwitchReason.SessionLogoff)
            {
                SessionsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Unsubscribe from system events when the service is disposed.
        /// </summary>
        public void Dispose()
        {
            SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
            GC.SuppressFinalize(this);
        }

        #region P/Invoke Definitions

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSEnumerateSessions(IntPtr hServer, int reserved, int version, out IntPtr ppSessionInfo, out int pCount);

        [DllImport("Wtsapi32.dll")]
        private static extern void WTSFreeMemory(IntPtr pMemory);

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WTS_INFO_CLASS infoClass, out IntPtr ppBuffer, out int pBytesReturned);

        private enum WTS_INFO_CLASS
        {
            WTSUserName = 5,
            WTSConnectState = 9,
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_SESSION_INFO
        {
            public int SessionId;
            public IntPtr pWinStationName;
            public WTSConnectState State; // Using the public enum now
        }

        #endregion
    }
}
