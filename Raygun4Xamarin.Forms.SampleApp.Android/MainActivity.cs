using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.OS;

using Raygun4Xamarin.Forms;
using Raygun4Xamarin.Forms.Android;

namespace Raygun.Forms.SampleApp.Droid
{
  [Activity(Label = "Raygun.Forms.SampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
  public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
  {
    protected override void OnCreate(Bundle savedInstanceState)
    {
      TabLayoutResource = Resource.Layout.Tabbar;
      ToolbarResource = Resource.Layout.Toolbar;

      base.OnCreate(savedInstanceState);
      global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
      LoadApplication(new App());

      // Pass on platform specific information to Raygun.
      RaygunPlatform.Configure(this);

      // Record platform level breadcrumb.
      RaygunClient.Current.RecordBreadcrumb("Creating main activity", RaygunBreadcrumbType.Manual, RaygunBreadcrumbLevel.Debug);

      // Report a generic exception with a stack trace.
      GenerateBasicException();

      // Report an aggregated exception.
      GenerateAggregateException();

      // Report an exception that contains inner exceptions.
      GenerateExceptionWithInnerException();

      // Report an unobserved task exception.
      GenerateUnobservedTaskException();

      // Send a test RUM timing event.
      RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.ViewLoaded, "TestView", 123);

      // Test timing events that will be ignored.
      RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.ViewLoaded, "DebugView", 123);
      RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.NetworkCall, "www.debug.com", 123);
    }

    private void GenerateBasicException()
    {
      try
      {
        new StackGenerator().DoSomething(new Exception("A Raygun Test Exception (Basic)"));
      }
      catch (Exception ex)
      {
        RaygunClient.Current.Send(ex, new List<string> { "CustomTag" }, new Dictionary<string, object> { { "CustomString", "Value" }, { "CustomNumber", 123 } });
      }
    }

    private void GenerateAggregateException()
    {
      try
      {
        var exceptions = new List<Exception>()
        {
          new Exception("A Raygun Test Exception (Basic 1 of 2)"),
          new Exception("A Raygun Test Exception (Basic 2 of 2)")
        };

        new StackGenerator().DoSomething(new AggregateException("A Raygun Test Exception (Aggregate)", exceptions));
      }
      catch (Exception ex)
      {
        RaygunClient.Current.Send(ex, new List<string> { "CustomTag" }, new Dictionary<string, object> { { "CustomString", "Value" }, { "CustomNumber", 123 } });
      }
    }

    private void GenerateExceptionWithInnerException()
    {
      try
      {
        new StackGenerator().DoSomething(new Exception("A Raygun Test Exception (BasicWithInner)", new Exception("A Raygun Test Exception (Inner)")));
      }
      catch (Exception ex)
      {
        RaygunClient.Current.Send(ex, new List<string> { "CustomTag" }, new Dictionary<string, object> { { "CustomString", "Value" }, { "CustomNumber", 123 } });
      }
    }

    private void GenerateUnobservedTaskException()
    {
      DoSomeTask();
    }

    private void DoSomeTask()
    {
      RaygunClient.Current.RecordBreadcrumb("About to do some task", RaygunBreadcrumbType.Console, RaygunBreadcrumbLevel.Warning);

      // Testing unobserved task exception handling.
      Task.Factory.StartNew(() =>
      {
        Thread.Sleep(1000);
        throw new Exception("A Raygun Test Exception (Unobserved)");
      });

      Thread.Sleep(2000);

      GC.Collect();
      GC.WaitForPendingFinalizers();
    }
  }
}