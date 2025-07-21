namespace MDSYS.LocalMessageNotifier.Core
{
    public record WindowsUser(string UserName, string SessionId, string Status)
    {
        // Overriding ToString for a more user-friendly display in the UI.
        public override string ToString()
        {
            return $"{UserName} ({Status})";
        }
    }
}
