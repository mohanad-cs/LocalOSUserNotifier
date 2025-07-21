using MDSYS.LocalMessageNotifier.Core;
using Microsoft.Extensions.DependencyInjection;

namespace MDSYS.LocalMessageNotifier.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var mainForm = serviceProvider.GetRequiredService<FrmMessageSender>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services)
        {

            // Register services as singletons to maintain state (like event subscriptions)
            services.AddSingleton<IUserSessionService, UserSessionService>();
            services.AddSingleton<ILocalMessageService, LocalMessageService>();

            string templateFilePath = Path.Combine(Application.StartupPath, Constants.MessageTemplateFileName);
            services.AddSingleton<IMessageTemplateFileService>(provider =>
                new MessageTemplateFileService(templateFilePath)
            );

            // Register forms as transient so a new instance is created each time
            services.AddTransient<FrmMessageSender>();
            services.AddTransient<FrmMessageTemplate>();
            services.AddTransient<AboutBox>();
        }
    }
}