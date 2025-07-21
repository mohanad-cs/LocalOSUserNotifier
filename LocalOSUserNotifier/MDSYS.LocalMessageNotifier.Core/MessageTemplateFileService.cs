using System.Text.Json;

namespace MDSYS.LocalMessageNotifier.Core
{
    public class MessageTemplateFileService : IMessageTemplateFileService
    {
        private readonly string _path;
        private static readonly JsonSerializerOptions _serializerOptions = new() { WriteIndented = true };

        public MessageTemplateFileService(string filePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);
            _path = filePath;
        }

        public async Task<List<MessageTemplate>> LoadAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (!File.Exists(_path))
                {
                    await File.WriteAllTextAsync(_path, "[]", cancellationToken);
                    return new List<MessageTemplate>();
                }

                string json = await File.ReadAllTextAsync(_path, cancellationToken);
                if (string.IsNullOrWhiteSpace(json)) return new List<MessageTemplate>();

                return JsonSerializer.Deserialize<List<MessageTemplate>>(json) ?? new List<MessageTemplate>();
            }
            catch (Exception)
            {
                return new List<MessageTemplate>();
            }
        }

        public async Task AddAsync(MessageTemplate messageTemplate, CancellationToken cancellationToken = default)
        {
            var messageTemplates = await LoadAsync(cancellationToken);
            if (messageTemplates.Any(x => x.MessageKey.Equals(messageTemplate.MessageKey, StringComparison.OrdinalIgnoreCase)))
            {
                await UpdateAsync(messageTemplate, cancellationToken);
            }
            else
            {
                messageTemplates.Add(messageTemplate);
                await SaveAsync(messageTemplates, cancellationToken);
            }
        }

        public async Task UpdateAsync(MessageTemplate messageTemplate, CancellationToken cancellationToken = default)
        {
            var messageTemplates = await LoadAsync(cancellationToken);
            int index = messageTemplates.FindIndex(m => m.MessageKey.Equals(messageTemplate.MessageKey, StringComparison.OrdinalIgnoreCase));
            if (index != -1)
            {
                messageTemplates[index] = messageTemplate;
                await SaveAsync(messageTemplates, cancellationToken);
            }
        }

        public async Task DeleteAsync(string messageKey, CancellationToken cancellationToken = default)
        {
            var messageTemplates = await LoadAsync(cancellationToken);
            if (messageTemplates.RemoveAll(x => x.MessageKey.Equals(messageKey, StringComparison.OrdinalIgnoreCase)) > 0)
            {
                await SaveAsync(messageTemplates, cancellationToken);
            }
        }
        public async Task<bool> ExistAsync(string messageKey, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(messageKey))
            {
                throw new ArgumentException("Message key cannot be null or empty.", nameof(messageKey));
            }
            var messageTemplates = await LoadAsync(cancellationToken);
            return messageTemplates.Any(x => x.MessageKey.Equals(messageKey, StringComparison.OrdinalIgnoreCase));
        }
        private async Task SaveAsync(List<MessageTemplate> messageTemplates, CancellationToken cancellationToken = default)
        {
            string json = JsonSerializer.Serialize(messageTemplates, _serializerOptions);
            await File.WriteAllTextAsync(_path, json, cancellationToken);
        }


    }
}
