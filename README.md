# Raygun4Xamarin-samples

## Raygun4Xamarin.Forms Sample App

A sample Xamarin Forms (4.0) application integrated with the Raygun4Xamarin.Forms provider.

# Raygun4Xamarin.Forms provider

## Where is my app API key?
When sending exceptions to the Raygun service, an app API key is required to map the messages to your application.

When you create a new application in your Raygun dashboard, your app API key is displayed within the instructions page. You can also find the API key by clicking the "Application Settings" button in the side bar of the Raygun dashboard.

## Installation

The provider targets .NET Standard 2.0 and is available through NuGet packages, found [here](https://www.nuget.org/packages/raygun4xamarin.forms/).

The currently supported platforms are Android and iOS with the following versions or newer:
 
 * Xamarin.Android 8.0
 * Xamarin.iOS 10.0

## Initialize RaygunClient

In the constructor of the `App` class in your base project, call the static `RaygunClient.Init()` method with your app API key. Using the static `Init` method will ensure a shared `RaygunClient` instance is available through the static property `Current`.

In `App.xaml.cs:`

``` csharp
public partial class App : Application
{
  public App()
  {
    InitializeComponent();

    // Initialising the Raygun client 
    RaygunClient.Init("YOUR_API_KEY");

    // Enable Raygun Products
    RaygunClient.Current.EnableCrashReporting(); 
    RaygunClient.Current.EnableRealUserMonitoring(); 

    // Remaining application setup logic
    MainPage = new MainPage();
  }
}
```

## Configure Raygun for your platform

Each platform being targeted requires an additional configuration step using the `RaygunPlatform` class in the following places. 

### For Android:

In `MainActivity.cs:`

``` csharp
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
{
  protected override void OnCreate(Bundle savedInstanceState)
  {
    // MainActivity startup logic
    base.OnCreate(savedInstanceState);
    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    LoadApplication(new App());

    // Configure Raygun for the current platform
    RaygunPlatform.Configure(this);
  }
}
```

**Android Troubleshooting**

You may need to install the `Xamarin.Android.Support.LocalBroadcastManager` package if you are having troubles getting Raygun working in your Android project. 
You may also need to set the minimum SDK version of your project to Android 9.

### For iOS:

In `AppDelegate.cs:`

``` csharp
public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
{
  public override bool FinishedLaunching(UIApplication app, NSDictionary options)
  {
    // AppDelegate startup logic
    global::Xamarin.Forms.Forms.Init();
    LoadApplication(new App());

    // Configure Raygun for the current platform
    RaygunPlatform.Configure();

    return base.FinishedLaunching(app, options);
  }
}
```

See the [Raygun4Xamarin](https://github.com/MindscapeHQ/raygun4xamarin) repository for more details on using this provider.