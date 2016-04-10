using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using FirstTryInno.Helpers;

namespace FirstTryInno.ViewModel
{
    public class MainPageViewModel 
    {

#region Private Members 

        private GpioPin _buttonLike;
        private GpioPin _buttonDisLike;
        private GpioPin _buttonUp;
        private GpioPin _buttonDown;
        private GpioController _ioController;
        private string _customText;

#endregion

        public event EventHandler Changed;
        public string CustomText
        {
            get
            {
                return _customText;
            }
            set
            {
                _customText = value;
                OnChanged(EventArgs.Empty);
            }
        }

        public MainPageViewModel()
        {
            InitGpio();
        }
        
        private void InitGpio()
        {
            _ioController = GpioController.GetDefault();
            if (_ioController == null)
            {
                CustomText = "GPIO failed...";
                return;
            }

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

            // Connect them to thei handlers.
            _buttonUp.ValueChanged += _buttonUp_ValueChanged;
            _buttonDown.ValueChanged += _buttonDown_ValueChanged;
            _buttonLike.ValueChanged += _buttonLike_ValueChanged;
            _buttonDisLike.ValueChanged += _buttonDisLike_ValueChanged;
        }

        private static void SetButtonBehaviour(GpioPin btn)
        {
            if(btn == null)
                return;
            
            // Check if input pull-up resistors are supported
            if (btn.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                btn.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                btn.SetDriveMode(GpioPinDriveMode.Input);

            // Set a debounce timeout to filter out switch bounce noise from a button press
            btn.DebounceTimeout = TimeSpan.FromMilliseconds(50);
        }

#region Event Handlers

        private void _buttonUp_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                CustomText = "Button Up was pressed...";
            }
        }

        private void _buttonDown_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                CustomText = "Button Down was pressed...";
            }
        }

        private void _buttonLike_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                CustomText = "Button Like was pressed...";
            }
        }

        private void _buttonDisLike_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs args)
        {
            if (args.Edge == GpioPinEdge.FallingEdge)
            {
                CustomText = "Button DisLike was pressed...";
            }
        }

#endregion

        // Invoke the Changed event; called whenever list changes:
        protected virtual void OnChanged(EventArgs e)
        {
            Changed?.Invoke(this, e);
        }
    }
}
