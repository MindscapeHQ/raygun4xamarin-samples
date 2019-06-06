using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Raygun.Forms.SampleApp.Models;
using Raygun.Forms.SampleApp.ViewModels;
//using Raygun4Xamarin.Forms;

namespace Raygun.Forms.SampleApp.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ItemsPage : ContentPage
  {
    ItemsViewModel viewModel;

    public ItemsPage()
    {
      InitializeComponent();

      BindingContext = viewModel = new ItemsViewModel();
    }

    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
      var item = args.SelectedItem as Item;
      if (item == null)
        return;

      //RaygunClient.Current.RecordBreadcrumb($"Clicked on item: {item.Text}", RaygunBreadcrumbType.ClickEvent);

      await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

      // Manually deselect item.
      ItemsListView.SelectedItem = null;
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      //RaygunClient.Current.RecordBreadcrumb("ItemsPage - OnAppearing", RaygunBreadcrumbType.Navigation);

      if (viewModel.Items.Count == 0)
        viewModel.LoadItemsCommand.Execute(null);
    }
  }
}