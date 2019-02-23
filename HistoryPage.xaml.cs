using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AllerAce_prototype_v2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            this.InitializeComponent();

            // read the history into the viewmodel
            loadHistory();
        }

        private async void loadHistory()
        {
            // open current history
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile historyFile = await localFolder.GetFileAsync("history.json");
            String fileContent = await FileIO.ReadTextAsync(historyFile);
            List<MeasurementEntry> history = JsonConvert.DeserializeObject<List<MeasurementEntry>>(fileContent);
            if (history == null)
            {
                history = new List<MeasurementEntry>();
            }

            // turn the history List into a ViewModel
            this.ViewModel = new MeasurementEntryViewModel(history);
            historyGrid.ItemsSource = this.ViewModel.MeasurementEntries;
        }

        public MeasurementEntryViewModel ViewModel { get; private set; }

        private void backToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllerAce_prototype_v2.MainPage));
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void exportDataButton_Clicked()
        {

        }

        private async void deleteButton_Clicked()
        {
            // open up the history
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile historyFile = await localFolder.GetFileAsync("history.json");
            String fileContent = await FileIO.ReadTextAsync(historyFile);
            List<MeasurementEntry> history = JsonConvert.DeserializeObject<List<MeasurementEntry>>(fileContent);
            if (history == null)
            {
                history = new List<MeasurementEntry>();
            }

            // remove the selected item from history
            history.RemoveAt(historyGrid.SelectedIndex);

            // repackage history and save it
            fileContent = JsonConvert.SerializeObject(history);
            await FileIO.WriteTextAsync(historyFile, fileContent);

            // reload history
            historyGrid.ItemsSource = null;
            loadHistory();
        }

        private void hamburgerMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hamburgerMenu.SelectedIndex == -1) return;

            // if export data is selected
            if (hamburgerMenu.SelectedIndex == 0) exportDataButton_Clicked();

            // if delete entry button is selected
            if (hamburgerMenu.SelectedIndex == 1) deleteButton_Clicked();

            hamburgerMenu.SelectedIndex = -1;
        }
    }
}
