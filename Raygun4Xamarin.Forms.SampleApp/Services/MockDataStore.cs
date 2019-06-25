using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raygun.Forms.SampleApp.Models;
using Raygun4Xamarin.Forms;
using System.Net.Http;
using System.Threading;

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

        //
        // CRASH REPORTING
        //

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Crash Report (Basic)",
          Description = "Report a generic exception with a stack trace",
          Action      = () =>
          {
            try
            {
              new StackGenerator().DoSomething(new Exception("A Raygun Test Exception (Basic)"));
            }
            catch (Exception ex)
            {
              RaygunClient.Current.Send(ex, new List<string> { "CustomTag" }, new Dictionary<string, object> { { "CustomString", "Value" }, { "CustomNumber", 123 } });
            }
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Crash Report (Aggregate)",
          Description = "Report an aggregated exception",
          Action      = () =>
          {
            try
            {
              var exceptions = new List<Exception>()
              {
                new Exception("A Raygun Test Exception (Basic 1 of 2)"),
                new Exception("A Raygun Test Exception (Basic 2 of 2)")
              };

              new StackGenerator().DoSomething(new AggregateException("A Raygun Test Exception (Aggregate)", exceptions));
            }
            catch (Exception ex)
            {
              RaygunClient.Current.Send(ex, new List<string> { "CustomTag" }, new Dictionary<string, object> { { "CustomString", "Value" }, { "CustomNumber", 123 } });
            }
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Crash Report (InnerException)",
          Description = "Report an exception that contains inner exceptions",
          Action      = () =>
          {
            try
            {
              new StackGenerator().DoSomething(new Exception("A Raygun Test Exception (BasicWithInner)", new Exception("A Raygun Test Exception (Inner)")));
            }
            catch (Exception ex)
            {
              RaygunClient.Current.Send(ex, new List<string> { "CustomTag" }, new Dictionary<string, object> { { "CustomString", "Value" }, { "CustomNumber", 123 } });
            }
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Crash Report (UnobservedTask)",
          Description = "Report an unobserved task exception",
          Action      = () =>
          {
            RaygunClient.Current.RecordBreadcrumb("About to do some task", RaygunBreadcrumbType.Console, RaygunBreadcrumbLevel.Warning);

            // Testing unobserved task exception handling.
            Task.Factory.StartNew(() =>
            {
              Thread.Sleep(1000);
              throw new Exception("A Raygun Test Exception (Unobserved)");
            });

            Thread.Sleep(2000);

            GC.Collect();
            GC.WaitForPendingFinalizers();
          }
        },

        //
        // RUM
        // 

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Custom View Timing Event",
          Description = "Sending a custom RUM timing event",
          Action      = () =>
          {
            RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.ViewLoaded, "TestView", 123);
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Custom View Timing Event (Ignored)",
          Description = "Attempts to send a timing event for a view that is ignored",
          Action      = () =>
          {
            RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.ViewLoaded, "DebugView", 123);
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Send Custom Network Timing Event",
          Description = "Sending a custom RUM timing event",
          Action      = () =>
          {
            RaygunClient.Current.SendTimingEvent(RaygunRUMEventTimingType.NetworkCall, "TestUrl", 123);
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Perform Web Request",
          Description = "Performs a web request that is timed and reported to Raygun",
          Action      = () =>
          {
            using(var client = new HttpClient())
            {
              var response = client.GetAsync("https://www.raygun.com").Result;
              Console.WriteLine("Made web request with response: " + response.StatusCode);
            }
          }
        },

        new Item
        {
          Id          = Guid.NewGuid().ToString(),
          Text        = "Perform Web Request (Ignored)",
          Description = "Performs a web request that is not reported to Raygun",
          Action      = () =>
          {
            using(var client = new HttpClient())
            {
              var response = client.GetAsync("http://www.debug.com").Result;
              Console.WriteLine("Made web request with response: " + response.StatusCode);
            }
          }
        }
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