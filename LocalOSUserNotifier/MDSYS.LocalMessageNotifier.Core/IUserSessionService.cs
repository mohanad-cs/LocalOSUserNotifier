namespace MDSYS.LocalMessageNotifier.Core
{
    public interface IUserSessionService : IDisposable
    {
       
        /// <summary>
        /// Gets a list of users on the local machine filtered by session state.
        /// </summary>
        /// <param name="states">A list of session states to include. If empty, defaults to Active.</param>
        /// <returns>A list of LoggedInUser objects.</returns>
        IEnumerable<WindowsUser> GetUsers(params WTSConnectState[] states);
    }
}
