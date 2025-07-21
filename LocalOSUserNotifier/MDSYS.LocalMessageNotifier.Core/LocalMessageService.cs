using System.Diagnostics;

namespace MDSYS.LocalMessageNotifier.Core
{
    public sealed class LocalMessageService : ILocalMessageService
    {
        private const string _msgExePath = "msg.exe";

        public async Task<(bool Success, string Output)> SendMessageAsync(string sessionId, string message, CancellationToken cancellationToken = default)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = _msgExePath,
                Arguments = $"{sessionId} {message}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            try
            {
                cancellationToken.ThrowIfCancellationRequested();
                using var process = Process.Start(processStartInfo);
                if (process == null) return (false, "Failed to start msg.exe process.");

                string output = await process.StandardOutput.ReadToEndAsync(cancellationToken);
                string error = await process.StandardError.ReadToEndAsync(cancellationToken);
                await process.WaitForExitAsync(cancellationToken);

                if (process.ExitCode == 0)
                {
                    return (true, output);
                }
                else
                {
                    string errorMessage = $"Error sending message. Exit Code: {process.ExitCode}. Details: {error}";
                    return (false, errorMessage);
                }
            }
            catch (OperationCanceledException)
            {
                return (false, "Send operation canceled.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
