using System;

namespace Raygun.Forms.SampleApp.Models
{
  public class Item
  {
    public string Id { get; set; }
    public string Text { get; set; }
    public string Description { get; set; }
    public Action Action { get; set; }
  }
}