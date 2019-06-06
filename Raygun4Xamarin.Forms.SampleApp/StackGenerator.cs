using System;
//using Raygun4Xamarin.Forms;

namespace Raygun.Forms.SampleApp
{
  public class StackGenerator
  {
    public StackGenerator()
    {
    }

    public void DoSomething(Exception exception)
    {
      //RaygunClient.Current.RecordBreadcrumb("About to do something", RaygunBreadcrumbType.Console, RaygunBreadcrumbLevel.Info);

      DoSomethingElse(exception);
    }

    public void DoSomethingElse(Exception exception)
    {
      DoSomethingMore(exception);
    }

    public void DoSomethingMore(Exception exception)
    {
      DoSomethingRisky(exception);
    }

    public void DoSomethingRisky(Exception exception)
    {
      //RaygunClient.Current.RecordBreadcrumb("About to do something risky", RaygunBreadcrumbType.Console, RaygunBreadcrumbLevel.Warning);

      throw exception;
    }
  }
}
