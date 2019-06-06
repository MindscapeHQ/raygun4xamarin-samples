using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Raygun.Forms.SampleApp.Models;
using Raygun.Forms.SampleApp.ViewModels;
//using Raygun4Xamarin.Forms;

namespace Raygun.Forms.SampleApp.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ItemDetailPage : ContentPage
  {
    ItemDetailViewModel viewModel;

    public ItemDetailPage(ItemDetailViewModel viewModel)
    {
      InitializeComponent();

      BindingContext = this.viewModel = viewModel;
    }

    public ItemDetailPage()
    {
      InitializeComponent();

      var item = new Item
      {
        Text = "Item 1",
        Description = "This is an item description."
      };

      viewModel = new ItemDetailViewModel(item);
      BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      //RaygunClient.Current.RecordBreadcrumb("ItemDetailPage - OnAppearing", RaygunBreadcrumbType.Navigation);

      if (viewModel.Item.Action != null)
      {
        viewModel.Item.Action.Invoke();
      }
    }
  }
}