using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    /// <summary>
    /// Represents a single settings entry consisting of a label and UI control.
    /// </summary>
    /// <remarks>
    /// Allows flexible definition of settings without hardcoding layout in XAML.
    /// </remarks>
    public class SettingsItem
    {
        /// <summary>
        /// Display label for the setting.
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// UI control used to edit or display the setting.
        /// </summary>
        public object Control { get; set; }
    }

    /// <summary>
    /// ViewModel responsible for system and admin configuration.
    /// </summary>
    /// <remarks>
    /// Divides settings into Website (API/System) and ACP (Admin Control Panel)
    /// sections. Uses dummy logic for now.
    /// </remarks>
    public class SettingsViewModel : BaseViewModel
    {
        public ObservableCollection<SettingsItem> WebsiteSettings { get; }
        public ObservableCollection<SettingsItem> ACPSettings { get; }

        /// <summary>
        /// Command to save settings.
        /// </summary>
        public ICommand SaveCommand { get; }

        public SettingsViewModel()
        {
            // Website / API related settings
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
                    Control = new TextBlock()
                    {
                        Width = 300,
                        Text = "https://localhost:8080"
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

            // Admin Control Panel settings
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

            
            SaveCommand = new RelayCommand(_ => ApplyDummy());
        }
        /// <summary>
        /// Dummy save handler for settings.
        /// </summary>
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
