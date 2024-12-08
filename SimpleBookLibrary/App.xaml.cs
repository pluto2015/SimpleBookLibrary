using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using SimpleBookLibrary.Data;
using SimpleBookLibrary.Model.Config;
using SimpleBookLibrary.Service;
using System.Windows;

namespace SimpleBookLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceProvider = ConfigureServices();
            //this.InitializeComponent();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog();
            });
            // 加载NLog配置
            LogManager.Setup().LoadConfigurationFromFile("nlog.config");
            services.AddAutoMapper(x => x.AddProfile<MapperProfile>());

            services.AddSingleton<ConfigService>();
            services.AddSingleton<IBookService,BookService>();
            services.AddSingleton<IBorrowHistoryService,BorrowHistoryService>();
            services.AddSingleton<IDepartmentService,DepartmentService>();
            services.AddSingleton<IBorrowerService,BorrowerService>();

            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var logger = App.Current.ServiceProvider.GetService<ILogger<App>>();
            try
            {
                //迁移数据库
                DBMigration.Migrate();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                MessageBox.Show(ex.ToString(), "提示");
            }
        }
    }
}
