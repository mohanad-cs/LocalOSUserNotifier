namespace MDSYS.LocalMessageNotifier.Core
{
    public interface IMessageTemplateFileService
    {
        Task<List<MessageTemplate>> LoadAsync(CancellationToken cancellationToken = default);
        Task AddAsync(MessageTemplate messageTemplate, CancellationToken cancellationToken = default);
        Task UpdateAsync(MessageTemplate messageTemplate, CancellationToken cancellationToken = default);
        Task DeleteAsync(string messageKey, CancellationToken cancellationToken = default);
        Task<bool> ExistAsync(string messageKey, CancellationToken cancellationToken = default);
    }
}
