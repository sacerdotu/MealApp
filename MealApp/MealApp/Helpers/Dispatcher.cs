using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace MealApp.Helpers
{
    public class Dispatcher
    {
        public static async void InvokeOnUI(DispatchedHandler runAsync)
        {
           await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, runAsync);
        }
    }
}
