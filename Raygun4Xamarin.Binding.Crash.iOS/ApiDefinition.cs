using Foundation;
using System;

namespace Raygun4Xamarin.Binding.Crash.iOS
{
  [BaseType (typeof (NSObject))]
  public interface Crash {

    [Static, Export("throwGenericException")]
    void ThrowGenericException();
  }
}