namespace MDSYS.LocalMessageNotifier.Core
{
    public interface ILocalMessageService
    {
        Task<(bool Success, string Output)> SendMessageAsync(string sessionId, string message, CancellationToken cancellationToken = default);
    }
}
