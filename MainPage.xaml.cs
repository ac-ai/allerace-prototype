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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AllerAce_prototype_v2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // check if history file already exists; if not, create it
            string rootPath = ApplicationData.Current.LocalFolder.Path;
            string filePath = Path.Combine(rootPath, "history.json");
            if (! System.IO.File.Exists(filePath))
            {
                createFile("history.json");
            }
        }

        private async Task createFile(string fileName)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync(fileName);
        }

        private void measureButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllerAce_prototype_v2.MeasurePage));
        }

        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllerAce_prototype_v2.HistoryPage));
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllerAce_prototype_v2.SettingsPage));
        }
    }
}
