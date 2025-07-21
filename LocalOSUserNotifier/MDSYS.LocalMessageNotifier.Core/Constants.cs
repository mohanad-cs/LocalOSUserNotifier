namespace MDSYS.LocalMessageNotifier.Core
{
    public static class Constants
    {
        public const string MessageTemplateFileName = "MessageTemplate.json";
        public const string ToolName = "Windows Users Message Notifier";
        public const string ToolDescription = "A utility for system administrators to send notifications to user sessions on the local machine.\r\n\r\nView users by connection status (Active, Disconnected, etc.).\r\n\r\nSend targeted messages or broadcast to all filtered users.\r\n\r\nSave and reuse message templates for common announcements.\r\n\r\nDeveloped by Mohanad Shamsan.";
        public const string ToolVersion = "1.0.1.1";
        public const string ToolAuthor = "MDSYS - Mohanad Shamsan";
        public const string ToolCompany = "MDSYS";
        public const string ToolCopyright = "Copyright © 2023 MDSYS. All rights reserved.";
        public const string ToolLicense = "This tool is licensed under the GNU General Public License v3.0. See LICENSE file for details.";
        public const int MaxMessageLength = 255;
    }
}
