using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;

using Raygun4Xamarin.Forms;
using Raygun4Xamarin.Forms.iOS;

namespace Raygun.Forms.SampleApp.iOS
{
  // The UIApplicationDelegate for the application. This class is responsible for launching the 
  // User Interface of the application, as well as listening (and optionally responding) to 
  // application events from iOS.
  [Register("AppDelegate")]
  public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
  {
    //
    // This method is invoked when the application has loaded and is ready to run. In this 
    // method you should instantiate the window, load the UI into it and then make the window
    // visible.
    //
    // You have 17 seconds to return from this method, or iOS will terminate your application.
    //
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
      global::Xamarin.Forms.Forms.Init();
      LoadApplication(new App());

      // Pass on platform specific information to Raygun.
      RaygunPlatform.Configure();

      // Record platform level breadcrumb.
      RaygunClient.Current.RecordBreadcrumb("Finished launching app delegate", RaygunBreadcrumbType.Manual, RaygunBreadcrumbLevel.Debug);

      // Report a generic exception with a stack trace.
      GenerateBasicException();

      // Report an aggregated exception.
      GenerateAggregateException();

      // Report an exception that contains inner exceptions.
      GenerateExceptionWithInnerException();

      // Report a native Objective-C exception.
      GenerateNativeException();

      // Send a test RUM timing event.
      RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.ViewLoaded, "TestView", 123);

      // Test timing events that will be ignored.
      RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.ViewLoaded, "DebugView", 123);
      RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.NetworkCall, "www.debug.com", 123);

      return base.FinishedLaunching(app, options);
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

    private void GenerateNativeException()
    {
      try
      {
        Raygun4Xamarin.Binding.Crash.iOS.Crash.ThrowGenericException();
      }
      catch (Exception ex)
      {
        RaygunClient.Current.Send(ex, new List<string> { "CustomTag" }, new Dictionary<string, object> { { "CustomString", "Value" }, { "CustomNumber", 123 } });
      }
    }
  }
}
