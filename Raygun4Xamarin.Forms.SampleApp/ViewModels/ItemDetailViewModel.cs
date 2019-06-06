using System;

using Raygun.Forms.SampleApp.Models;

namespace Raygun.Forms.SampleApp.ViewModels
{
  public class ItemDetailViewModel : BaseViewModel
  {
    public Item Item { get; set; }
    public ItemDetailViewModel(Item item = null)
    {
      Title = item?.Text;
      Item = item;
    }
  }
}
