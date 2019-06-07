# raygun4xamarin-samples

## raygun4xamarin.forms Sample App

A sample Xamarin Forms (4.0) application integrated with the Raygun4Xamarin.Forms provider.

# Raygun4Xamarin.Forms provider

## Installation

The provider targets .NET Standard 2.0 and is available through NuGet packages, found [here](https://www.nuget.org/packages/raygun4xamarin.forms/).

The currently supported platforms are Android and iOS with the following versions or newer:
 
 * Xamarin.Android 8.0
 * Xamarin.iOS 10.0

## Configuration

## Initialisation

The initialisation of Raygun must occur early in the apps initial startup phase. We recommend doing this in the constructor of your Application class. 

Using the static 'Init' method will also ensure a shared RaygunClient instance is available through the static 'Current' property on the RaygunClient class.

``` csharp
public partial class App : Application
{
  public App()
  {
    InitializeComponent();

	// Initialising the Raygun client 
	RaygunClient.Init("_API_KEY_");

	// Remaining application setup logic
	MainPage = new MainPage();
  }
}
```

## Unique User Tracking

Providing user information will allow Raygun to correlate error reports and RUM events with specific users.
Assigning user information is performed by assigning a RaygunUserInfo object to your Raygun client. 

``` csharp
RaygunClient.Current.User = new RaygunUserInfo("_UNIQUE_ID_")
{
  FirstName   = "Ronald";
  FullName    = "Ronald Raygun";
  Email       = "ronald@raygun.com";
  IsAnonymous = false;
};
```

## Crash Reporting

Once the Raygun client is initialised you can enable the client's Crash Reporting functionality.

``` csharp
RaygunClient.Current.EnableCrashReporting();
```

Once enabled your are able to:
* Automatically report unhandled exceptions
* Manually report errors
* Record breadcrumbs

### Before send event handling

``` csharp
RaygunClient.Current.BeforeSendingCrashReportEvent += (sender, e) =>
{
  if (e.Report.Details.Error.ClassName == "NotImplementedException")
  {
   	e.Cancel = true;
  }
};
```

### Manually reporting errors

``` csharp
try
{
  DoSomethingRisky();
}
catch (Exception exception)
{
  RaygunClient.Current.Send(exception);
}
```

### Recording breadcrumbs

``` csharp
RaygunClient.Current.RecordBreadcrumb("Entered login screen");
```

## Real User Monitoring

Once the Raygun client is initialised you can enable the client's Real User Monitoring (RUM) functionality.

``` csharp
RaygunClient.Current.EnableRealUserMonitoring();
```

Once enabled your are able to:
* Automatically report user sessions
* Automatically report view timing events
* Manually report timing events

### Manually report timing events

``` csharp
RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.ViewLoaded, "TestView", 123);
```
