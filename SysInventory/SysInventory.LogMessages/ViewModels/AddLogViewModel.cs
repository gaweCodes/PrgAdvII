using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using SysInventory.LogMessages.Properties;

namespace SysInventory.LogMessages.ViewModels
{
    internal class AddLogViewModel
    {
        private string _pod;
        public string PoD
        {
            get => _pod;
            set
            {
                _pod = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        private string _hostname;
        public string Hostname
        {
            get => _hostname;
            set
            {
                _hostname = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        private int _severity;
        public int Severity
        {
            get => _severity;
            set
            {
                _severity = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        private string _message;
        public string Message { get => _message;
            set
            {
                _message = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public RelayCommand<Window> SaveCommand { get; }
        public RelayCommand<Window> CancelCommand { get; }

        public AddLogViewModel()
        {
            SaveCommand = new RelayCommand<Window>(Save, CanSave);
            CancelCommand = new RelayCommand<Window>(windoToClose =>
            {
                windoToClose?.Close();
            });
        }
        private void Save(Window windoToClose)
        {
            try
            {
                using (var connection = new SqlConnection(Settings.Default.ConnectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "LogMessageAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@podName", PoD);
                        cmd.Parameters.AddWithValue("@hostname", Hostname);
                        cmd.Parameters.AddWithValue("@lvl", Severity);
                        cmd.Parameters.AddWithValue("@msg", Message);
                        var result = cmd.ExecuteNonQuery();
                        if (result == -1)
                        {
                            MessageBox.Show("The device or pod was not found.");
                            return;
                        }
                    }
                }
                windoToClose?.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten: " + e.Message);
            }
        }

        private bool CanSave(Window windowToClose) =>
            windowToClose != null && !string.IsNullOrWhiteSpace(PoD) && !string.IsNullOrWhiteSpace(Hostname) &&
            Severity > 0 &&
            !string.IsNullOrWhiteSpace(Message);
    }
}
