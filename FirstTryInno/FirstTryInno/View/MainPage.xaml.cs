using FirstTryInno.ViewModel;
using System;
using Windows.Devices.Gpio;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FirstTryInno
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel _viewModel; 

        public MainPage()
        {
            InitializeComponent();
            InitializeCustomComponents();
         }
               
        private void InitializeCustomComponents()
        {
            _viewModel = new MainPageViewModel();
            _viewModel.Changed += _viewModel_Changed;
            
        }

        private void _viewModel_Changed(object sender, EventArgs e)
        {
            // need to invoke UI updates on the UI thread because this event
            // handler gets invoked on a separate thread.
            var task = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => 
            {
                labelName.Text = _viewModel.CustomText;
            });
            
        }
    }
}
