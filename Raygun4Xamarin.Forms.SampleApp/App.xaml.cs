using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Raygun.Forms.SampleApp.Views;

using Raygun4Xamarin.Forms;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Raygun.Forms.SampleApp
{
  public partial class App : Application
  {
    public App()
    {
      InitializeComponent();

      // Initialise the settings that will configure the Raygun client.
      var settings = new RaygunSettings("JKFHTSrwUnBvymJCSjdaaA") 
      { 
        LogLevel           = LogLevel.Verbose,
        BreadcrumbLevel    = RaygunBreadcrumbLevel.Debug,
        ApplicationVersion = "1.0.0",
        IgnoredViews       = new List<string>() { "DebugView" },
        IgnoredUrls        = new List<string>() { "www.debug.com" },
      };

      // Initialise a client instance.
      var client = RaygunClient.Init(settings);

      client.BeforeSendingCrashReportEvent += BeforeSendingCrashReportEventHandler;
     
      // Setting tags that will be sent with every crash report.
      client.Tags = new List<string>() 
      { 
        "GlobalTag"
      };

      // Setting information that will be sent with every crash report.
      client.CustomData = new Dictionary<string, object>() 
      {
        { "GlobalString", "Value" },
        { "GlobalValue", 123 }
      };

      // Enabling the products.
      client.EnableCrashReporting();
      client.EnableRealUserMonitoring();

      client.RecordBreadcrumb("Initialised Raygun", RaygunBreadcrumbType.Manual, RaygunBreadcrumbLevel.Debug);

      MainPage = new MainPage();
    }

    private void BeforeSendingCrashReportEventHandler(object sender, RaygunSendingCrashReportEventArgs e)
    {
      Console.WriteLine($"[App] BeforeSendingCrashReportEventHandler was called for {e.Report.Details.Error.ClassName}: {e.Report.Details.Error.Message}");
    }

    protected override void OnStart()
    {
      // Handle when your app starts
      RaygunClient.Current.RecordBreadcrumb("App - OnStart", RaygunBreadcrumbType.Navigation);
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
      RaygunClient.Current.RecordBreadcrumb("App - OnSleep", RaygunBreadcrumbType.Navigation);
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
      RaygunClient.Current.RecordBreadcrumb("App - OnResume", RaygunBreadcrumbType.Navigation);
    }
  }
}
