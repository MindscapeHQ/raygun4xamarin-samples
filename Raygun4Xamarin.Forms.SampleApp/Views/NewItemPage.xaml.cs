using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Raygun.Forms.SampleApp.Models;
//using Raygun4Xamarin.Forms;

namespace Raygun.Forms.SampleApp.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewItemPage : ContentPage
  {
    public Item Item { get; set; }

    public NewItemPage()
    {
      InitializeComponent();

      Item = new Item
      {
        Text = "Item name",
        Description = "This is an item description."
      };

      BindingContext = this;
    }

    async void Save_Clicked(object sender, EventArgs e)
    {
      MessagingCenter.Send(this, "AddItem", Item);
      await Navigation.PopModalAsync();
    }

    protected override void OnAppearing()
    {
      base.OnAppearing();

      //RaygunClient.Current.RecordBreadcrumb("NewItemPage - OnAppearing", RaygunBreadcrumbType.Navigation);
    }
  }
}