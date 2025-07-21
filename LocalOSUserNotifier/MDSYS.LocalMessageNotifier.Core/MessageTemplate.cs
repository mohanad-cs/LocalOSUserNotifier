using System.Text.Json.Serialization;

namespace MDSYS.LocalMessageNotifier.Core
{
    public record MessageTemplate
    {
        public string MessageKey { get; init; }
        public string Message { get; init; }

        [JsonConstructor]
        public MessageTemplate(string messageKey, string message)
        {
            if (string.IsNullOrWhiteSpace(messageKey))
            {
                throw new ArgumentException($"'{nameof(messageKey)}' cannot be null or whitespace.", nameof(messageKey));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
            }
            if (message.Length > Constants.MaxMessageLength)
            {
                throw new ArgumentException($"Message Length Can not be greater than {Constants.MaxMessageLength}");
            }
            MessageKey = messageKey;
            Message = message;
        }

        // Overriding ToString for display in the ListBox.
        public override string ToString() => MessageKey;
    }
}
