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
    }
  }
}