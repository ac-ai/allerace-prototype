using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AllerAce_prototype_v2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MeasurePage : Page
    {
        int stepNumber;
        String[] steps = new[] {"Input patient data.", "Raise slider, insert well plate, and close device.", "Move slider to S+TS.", "Move slider to WB.", "Move slider to SA-HRP",
            "Move slider to WB.", "Move slider to L.", "Move slider to I."};

        public MeasurePage()
        {
            this.InitializeComponent();
            dateText.Text = DateTime.Now.ToString("MM/dd/yyyy");
            timeText.Text = DateTime.Now.ToString("h:mm tt");
            stepNumber = 1;
        }

        private void backToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllerAce_prototype_v2.MainPage));
        }

        private async void startMeasurementButton_Click(object sender, RoutedEventArgs e)
        {
            // move to the mixing reagents step
            // insertSampleBorder.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            // mixingReagentsBorder.Background = new SolidColorBrush(Windows.UI.Colors.LightBlue);

            // move to the mixing reagents step
            // insertSampleText.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            // mixingReagentsText.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);
            // mixingReagentsProgressBar.Visibility = Visibility.Visible;
            // for (int i=0; i<101; i++)
            // {
            //   await Task.Delay(TimeSpan.FromSeconds(0.1));
            //    mixingReagentsProgressBar.Value = i;
            // }
            // mixingReagentsProgressBar.Visibility = Visibility.Collapsed;

            // move to the light reaction step
            // mixingReagentsBorder.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            // measuringLightOutputBorder.Background = new SolidColorBrush(Windows.UI.Colors.LightBlue);

            // go to the next step
            if (stepNumber < 8)
            {
                stepNumber++;
                instructionPic.Source = new BitmapImage(new Uri("ms-appx:///Assets/Slide" + (stepNumber-1) + ".jpg"));
                instructionPic.Visibility = Visibility.Visible;
                instructionText.Text = steps[stepNumber - 1];
            }
            else if (stepNumber == 8)
            {
                performMeasurement();
                stepNumber++;
                startMeasurementButton.Content = "Return to main menu";
            }
            else if (stepNumber == 9)
            {
                backToMainMenu_Click(sender, e);
            }
        }

        private async void performMeasurement()
        {
            // change text and wait for 15 seconds
            for (int i = 0; i < 10; i++)
            {
                instructionText.Text = "performing measurement ..";
                await Task.Delay(TimeSpan.FromSeconds(0.1));
                instructionText.Text = "performing measurement. .";
                await Task.Delay(TimeSpan.FromSeconds(0.1));
                instructionText.Text = "performing measurement.. ";
                await Task.Delay(TimeSpan.FromSeconds(0.1));
            }

            // display the reading
            measurementText.FontSize = 48;
            Random random = new Random();
            int histamineLevel = random.Next(0, 10);
            measurementText.Text = "Histamine level: " + (histamineLevel * 50).ToString() + " nM.";
            measurementText.Visibility = Visibility.Visible;

            // create the measurement object and add it to the history
            MeasurementEntry measurementEntry = new MeasurementEntry(nameText.Text, tagsText.Text, allergenText.Text, histamineLevel * 50, DateTime.Parse(dateText.Text + " " + timeText.Text));
            // open current history
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile historyFile = await localFolder.GetFileAsync("history.json");
            String fileContent = await FileIO.ReadTextAsync(historyFile);
            List<MeasurementEntry> history = JsonConvert.DeserializeObject<List<MeasurementEntry>>(fileContent);
            if (history == null)
            {
                history = new List<MeasurementEntry>();
            }
            // add last measurement to history
            history.Add(measurementEntry);
            // save the history file again
            fileContent = JsonConvert.SerializeObject(history);
            await FileIO.WriteTextAsync(historyFile, fileContent);
        }
    }
}
