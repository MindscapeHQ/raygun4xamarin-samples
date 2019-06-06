using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Raygun4Xamarin.Forms;

namespace Raygun.Forms.SampleApp.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class AboutPage : ContentPage
  {
    public AboutPage()
    {
      InitializeComponent();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      //RaygunClient.Current.RecordBreadcrumb("AboutPage - OnAppearing", RaygunBreadcrumbType.Navigation);
    }
  }
}