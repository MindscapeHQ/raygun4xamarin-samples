using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Raygun4Xamarin.Forms;

namespace Raygun.Forms.SampleApp.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class MainPage : TabbedPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      //RaygunClient.Current.RecordBreadcrumb("MainPage - OnAppearing", RaygunBreadcrumbType.Navigation);
    }
  }
}