using System.Diagnostics;
using Plugin.Maui.Audio;

namespace TipCalculatorPart2
{
    public class TipCalculatorDataModel
    {
        public string billDisplay = "$";
        public double tipAmount = 0.00;
        public bool isDecimal = false;
        public int decimalPlaces = 0;
        public double sliderPercentage;
        public bool totalCalculated = false;

        public double TipAmount(double bill, double tipPercentage)
        {
            double tip = 0.00;
            if (bill >= 0)
            {
                tip = bill * (tipPercentage / 100);
            }

            return tip;
        }
        public double TotalAmount(double bill, double tip)
        {
            double totalAmount = bill + tip;
            return totalAmount;
        }

        public double CostPerDiner(double total, int diners)
        {
            double costPerDiner = total / diners;
            return costPerDiner;
        }
    }
    public partial class MainPage : ContentPage
    {
        IAudioPlayer tapSound;
        public TipCalculatorDataModel data = new TipCalculatorDataModel();
        public bool hasStarted = false;

        public MainPage()
        {
            InitializeComponent();
            InitialiseSound();

            TipCalculatorDataModel data = new TipCalculatorDataModel();
        }

        public async void InitialiseSound()
        {
            Stream soundFile = await FileSystem.OpenAppPackageFileAsync("tap.wav");
            tapSound = AudioManager.Current.CreatePlayer(soundFile);
        }

        public void ButtonClicked(object sender, EventArgs e)
        {
            tapSound.Play();

            var buttonPressed = (Button)sender;
            string buttonText = buttonPressed.Text;


            if (data.isDecimal == false)
            {
                billAmount.Text = data.billDisplay + buttonText;
                data.billDisplay = billAmount.Text;
            }
            else if (data.isDecimal == true)
            {
                while (data.decimalPlaces < 2)
                {
                    billAmount.Text = data.billDisplay + buttonText;
                    data.billDisplay = billAmount.Text;
                    data.decimalPlaces++;
                    break;
                }
            }


            hasStarted = true;

            Debug.WriteLine(buttonText);
        }

        public void ClearButtonClicked(object sender, EventArgs e)
        {
            tapSound.Play();

            if (hasStarted == true)
            {
                data.billDisplay = "$";
                billAmount.Text = "$0.00";
                tipAmount.Text = "$0.00";
                totalAmount.Text = "$0.00";
                data.decimalPlaces = 0;
                dinersAmount.Text = "1";
                perDinerAmount.Text = "$0.00";

                hasStarted = false;
                data.isDecimal = false;
                data.totalCalculated = false;

                Debug.WriteLine("Bill cleared");
            }
        }

        public void SliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            data.sliderPercentage = slider.Value;
            percentageLabel.Text = data.sliderPercentage.ToString($"F2") + "%";

            double bill = double.Parse(billAmount.Text.Trim("$"));
            double tip = data.TipAmount(bill, data.sliderPercentage);

            tipAmount.Text = "$" + tip.ToString($"F2");

            if (hasStarted == true)
            {
                totalAmount.Text = "$" + data.TotalAmount(bill, tip).ToString($"F2");
                data.totalCalculated = true;

                double total = double.Parse(totalAmount.Text.Trim("$"));

                perDinerAmount.Text = "$" + data.CostPerDiner(total,
                    int.Parse(dinersAmount.Text)).ToString($"F2");
            }
        }
        public void Point_Clicked(object sender, EventArgs e)
        {
            tapSound.Play();

            var buttonPressed = (Button)sender;
            string buttonText = buttonPressed.Text;

            if (hasStarted == true && data.isDecimal == false)
            {
                billAmount.Text = data.billDisplay + buttonText;
                data.billDisplay = billAmount.Text;

                data.isDecimal = true;
            }
        }

        public void DinerStepperAmount_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            tapSound.Play();

            var stepperPressed = (Stepper)sender;
            int stepperValue = (int)stepperPressed.Value;

            dinersAmount.Text = stepperValue.ToString();

            if (data.totalCalculated == true)
            {
                double total = double.Parse(totalAmount.Text.Trim("$"));

                perDinerAmount.Text = "$" +
                    data.CostPerDiner(total, stepperValue).ToString($"F2");
            }
        }
    }
}
