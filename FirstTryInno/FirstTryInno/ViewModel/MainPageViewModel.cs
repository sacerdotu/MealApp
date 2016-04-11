using System;
using Windows.Devices.Gpio;
using Helpers;
using System.Collections.ObjectModel;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainPageViewModel : NotificationBase
    {
        #region Members 

        private GpioPin _buttonLike;
        private GpioPin _buttonDisLike;
        private GpioPin _buttonUp;
        private GpioPin _buttonDown;
        private GpioController _ioController;
        private string _customText;
        private ObservableCollection<MenuItemViewModel> _menuItemsViewModel;
        private int _selectedIndex;

        public string CustomText
        {
            get
            {
                return _customText;
            }
            set
            {
                SetProperty(ref _customText, value);
            }
        }

        public ObservableCollection<MenuItemViewModel> MenuItemsViewModel
        {
            get { return _menuItemsViewModel; }
            set { SetProperty(ref _menuItemsViewModel, value); }
        }
        
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (SetProperty(ref _selectedIndex, value))
                {
                    RaisePropertyChanged(nameof(SelectedMenuItem));
                }
            }
        }

        public MenuItemViewModel SelectedMenuItem
        {
            get { return (_selectedIndex >= 0 && _selectedIndex < _menuItemsViewModel.Count) ? _menuItemsViewModel[_selectedIndex] : null; }
        }

        #endregion

        public MainPageViewModel()
        {
            InitGpio();
            _menuItemsViewModel = new ObservableCollection<MenuItemViewModel>();
            // new List<ProviderMenuItem>();

            Task<List<ProviderMenuItem>> task = Task.Run(() => WebApiHelper.GetAllProducts());
            task.Wait();

            var list = task.Result;
            foreach(var item in list)
            {
                MenuItemsViewModel.Add(new MenuItemViewModel(item));
            }

            SelectedIndex = -1;
        }

        #region I/O 

        private void InitGpio()
        {
            _ioController = GpioController.GetDefault();
            if (_ioController == null)
            {
                CustomText = "GPIO failed...";
                return;
            }
            CustomText = "GPIO was successfully started...";
            InitButtons();
        }

        private void InitButtons()
        {
            _buttonUp = _ioController.OpenPin(GPIOConstants.BUTTON_UP_PIN);
            _buttonDown = _ioController.OpenPin(GPIOConstants.BUTTON_DOWN_PIN);
            _buttonLike = _ioController.OpenPin(GPIOConstants.BUTTON_LIKE_PIN);
            _buttonDisLike = _ioController.OpenPin(GPIOConstants.BUTTON_DISLIKE_PIN);

            SetButtonBehaviour(_buttonUp);
            SetButtonBehaviour(_buttonDown);
            SetButtonBehaviour(_buttonLike);
            SetButtonBehaviour(_buttonDisLike);

            // Connect them to their handlers.
            _buttonUp.ValueChanged += _buttonUp_ValueChanged;
            _buttonDown.ValueChanged += _buttonDown_ValueChanged;
            _buttonLike.ValueChanged += _buttonLike_ValueChanged;
            _buttonDisLike.ValueChanged += _buttonDisLike_ValueChanged;
        }

        private static void SetButtonBehaviour(GpioPin btn)
        {
            if (btn == null)
                return;

            // Check if input pull-up resistors are supported
            if (btn.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                btn.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                btn.SetDriveMode(GpioPinDriveMode.Input);

            // Set a debounce timeout to filter out switch bounce noise from a button press
            btn.DebounceTimeout = TimeSpan.FromMilliseconds(50);
        }

        #endregion

        #region Event Handlers

        private void _buttonUp_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                Dispatcher.InvokeOnUI(() =>
                {
                    if (SelectedIndex - 1 >= 0)
                        SelectedIndex--;                    
                });
            }
        }

        private void _buttonDown_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                Dispatcher.InvokeOnUI(() =>
                {
                    if (_menuItemsViewModel.Count > SelectedIndex + 1)
                        SelectedIndex++;
                });
            }
        }

        private void _buttonLike_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                Dispatcher.InvokeOnUI(() =>
                {
                    CustomText = "Button Like was pressed...";
                });
            }
        }

        private void _buttonDisLike_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                Dispatcher.InvokeOnUI(() =>
                {
                    CustomText = "Button DisLike was pressed...";
                });
            }
        }

#endregion
    }
}
