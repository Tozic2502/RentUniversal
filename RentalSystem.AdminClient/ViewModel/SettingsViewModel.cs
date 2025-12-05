using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    public class SettingsItem
    {
        public string Label { get; set; }
        public object Control { get; set; }
    }

    public class SettingsViewModel : BaseViewModel
    {
        public ObservableCollection<SettingsItem> WebsiteSettings { get; }
        public ObservableCollection<SettingsItem> ACPSettings { get; }

        public ICommand SaveCommand { get; }

        public SettingsViewModel()
        {
            //
            // WEBSITE SETTINGS (Server/API)
            //
            WebsiteSettings = new ObservableCollection<SettingsItem>
            {
                new SettingsItem
                {
                    Label = "System Status:",
                    Control = new ComboBox
                    {
                        Width = 200,
                        ItemsSource = new [] { "Live", "Maintenance" },
                        SelectedIndex = 0
                    }
                },

                new SettingsItem
                {
                    Label = "API Base URL:",
                    Control = new TextBox
                    {
                        Width = 300,
                        Text = "https://localhost:5000"
                    }
                },

                new SettingsItem
                {
                    Label = "Version:",
                    Control = new TextBlock
                    {
                        Text = "v1.0.0",
                        VerticalAlignment = VerticalAlignment.Center,
                        FontWeight = FontWeights.Bold
                    }
                }
            };


            //
            // ACP SETTINGz
            //
            ACPSettings = new ObservableCollection<SettingsItem>
            {
                new SettingsItem
                {
                    Label = "Theme:",
                    Control = new ComboBox
                    {
                        Width = 200,
                        ItemsSource = new [] { "Light", "Dark" },
                        SelectedIndex = 0
                    }
                }
            };


            //
            // Save Button Dummy
            //
            SaveCommand = new RelayCommand(_ => ApplyDummy());
        }

        private void ApplyDummy()
        {
            MessageBox.Show(
                "Settings saved (dummy).\n\n" +
                "Once api is implemented there will be something here",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
        }
    }
}
