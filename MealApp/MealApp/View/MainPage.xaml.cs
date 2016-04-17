using System;
using Windows.Devices.Gpio;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using DevExpress.UI.Xaml.Charts;
using MealApp.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MealApp.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel PageViewModel;

        public class MyPalette : Palette
        {
            private ColorCollection color = new ColorCollection()
                    {
                        Windows.UI.Colors.Green,
                        Windows.UI.Colors.Red
                    };

            public ColorCollection Colors
            {
                get { return color; }
            }

            protected override ColorCollection ActualColors { get { return color; } }
        }

        public MainPage()
        {
            InitializeComponent();
            PageViewModel = new MainPageViewModel();
            pieChart.Palette = new MyPalette();
        }
    }
}
