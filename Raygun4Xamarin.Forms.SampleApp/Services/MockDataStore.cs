using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raygun.Forms.SampleApp.Models;
using Raygun4Xamarin.Forms;

namespace Raygun.Forms.SampleApp.Services
{
  public class MockDataStore : IDataStore<Item>
  {
    List<Item> items;

    public MockDataStore()
    {
      items = new List<Item>();
      var mockItems = new List<Item>
      {
        new Item
        { 
          Id          = Guid.NewGuid().ToString(), 
          Text        = "Login user A", 
          Description = "Logging in user A", 
          Action      = () => 
          {
            RaygunClient.Current.User = new RaygunUserInfo("A") { FirstName = "A", FullName = "Mr A", Email = "a@raygun.com", IsAnonymous = false }; 
          } 
        },

        new Item 
        { 
          Id          = Guid.NewGuid().ToString(),
          Text        = "Login user B", 
          Description = "Logging in user B",
          Action      = () =>
          {
            RaygunClient.Current.User = new RaygunUserInfo("B") { FirstName = "B", FullName = "Mr B", Email = "b@raygun.com", IsAnonymous = false };
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send View Timing Event",
          Description = "Sending a custom RUM timing event",
          Action      = () =>
          {
            RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.ViewLoaded, "TestView", 123);
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Network Timing Event",
          Description = "Sending a custom RUM timing event",
          Action      = () =>
          {
            RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.NetworkCall, "TestUrl", 123);
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Crash Report",
          Description = "Sending a crash report",
          Action      = () =>
          {
            RaygunClient.Current.Send(new Exception("A Raygun Test Exception"), new List<string>{ "ManualReport" });
          }
        },
      };

      foreach (var item in mockItems)
      {
        items.Add(item);
      }
    }

    public async Task<bool> AddItemAsync(Item item)
    {
      items.Add(item);

      return await Task.FromResult(true);
    }

    public async Task<bool> UpdateItemAsync(Item item)
    {
      var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
      items.Remove(oldItem);
      items.Add(item);

      return await Task.FromResult(true);
    }

    public async Task<bool> DeleteItemAsync(string id)
    {
      var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
      items.Remove(oldItem);

      return await Task.FromResult(true);
    }

    public async Task<Item> GetItemAsync(string id)
    {
      return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
    }

    public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
    {
      return await Task.FromResult(items);
    }
  }
}