using System;
using System.Collections.Generic;
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

      // Report a native Objective-C exception.
      GenerateNativeException();

      return base.FinishedLaunching(app, options);
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
