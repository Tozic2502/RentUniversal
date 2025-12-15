using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    /// <summary>
    /// ViewModel responsible for file upload functionality in the Admin Panel.
    /// </summary>
    /// <remarks>
    /// Currently simulates upload progress. Should later integrate with
    /// backend endpoints and support error handling and validation.
    /// </remarks>
    public class UploadViewModel : BaseViewModel
    {
        private string _selectedFile;

        /// <summary>
        /// Full path of the selected file.
        /// </summary>
        public string SelectedFile
        {
            get => _selectedFile;
            set { _selectedFile = value; OnPropertyChanged(); }
        }

        private string _statusMessage = "Waiting for file...";

        /// <summary>
        /// Status message shown to the user during upload.
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        private int _progress;

        /// <summary>
        /// Upload progress percentage (0–100).
        /// </summary>
        public int Progress
        {
            get => _progress;
            set { _progress = value; OnPropertyChanged(); }
        }

        public ICommand ChooseFileCommand { get; }
        public ICommand UploadCommand { get; }

        public UploadViewModel()
        {
            ChooseFileCommand = new RelayCommand(_ => ChooseFile());
            UploadCommand = new RelayCommand(async _ => await UploadAsync(), _ => !string.IsNullOrEmpty(SelectedFile));
        }

        private void ChooseFile()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                SelectedFile = dialog.FileName;
                StatusMessage = "File selected. Ready to upload.";
                Progress = 0;
            }
        }

        private async Task UploadAsync()
        {
            if (!File.Exists(SelectedFile))
            {
                StatusMessage = "File does not exist!";
                return;
            }

            StatusMessage = "Uploading...";
            Progress = 0;

            // Simulated upload progress
            for (int i = 0; i <= 100; i += 8)
            {
                Progress = i;
                await Task.Delay(120);
            }

            StatusMessage = "Upload completed!";
        }
    }
}