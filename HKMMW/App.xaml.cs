using HKMMW.Activation;
using HKMMW.Contracts.Services;
using HKMMW.Core.Contracts.Services;
using HKMMW.Core.Services;
using HKMMW.Core.Test;
using HKMMW.Models;
using HKMMW.Services;
using HKMMW.ViewModels;
using HKMMW.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace HKMMW;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers

            // Services
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<IWebRequestService, WebRequestService>();

            services.AddSingleton<ITaskService, TaskService>();
            //services.AddSingleton<ITaskService, TestTaskService>();

            //ModInfo

            services.AddSingleton<IModLinksModInfoProvideService, ModLinksModInfoProvideService>();

            // Core Services
            services.AddSingleton<IFileService, FileService>();

            // Views and ViewModels
            services.AddSingleton<MAPIManageViewModel>();
            services.AddSingleton<MAPIManagePage>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<TasksViewModel>();
            services.AddSingleton<TasksPage>();
            services.AddSingleton<ModGroupsViewModel>();
            services.AddSingleton<ModGroupsPage>();
            services.AddSingleton<OnlineModsViewModel>();
            services.AddSingleton<OnlineModsPage>();
            services.AddSingleton<LocalModsViewModel>();
            services.AddSingleton<LocalModsPage>();
            services.AddSingleton<ShellPage>();
            services.AddSingleton<ShellViewModel>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {

        base.OnLaunched(args);

        await GetService<IActivationService>().ActivateAsync(args);
    }
}
