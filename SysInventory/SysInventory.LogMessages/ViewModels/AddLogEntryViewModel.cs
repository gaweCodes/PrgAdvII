using System;
using System.Windows;
using SysInventory.LogMessages.DataAccess.AdoNet;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.ViewModels
{
    internal class AddLogEntryViewModel : BaseViewModel<LogEntry>
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
        public string Message 
        { 
            get => _message;
            set
            {
                _message = value;
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        public RelayCommand<Window> SaveCommand { get; }
        public RelayCommand<Window> CancelCommand { get; }
        public AddLogEntryViewModel()
        {
            SaveCommand = new RelayCommand<Window>(Save, CanSave);
            CancelCommand = new RelayCommand<Window>(Cancel);
            DataRepository = new LogRepository();
        }
        private void Save(Window windowToClose)
        {

            try
            {
                DataRepository.Add(new LogEntry
                    {PoD = PoD, Message = Message, Severity = Severity, Hostname = Hostname});
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured while adding the new log entry: " + e.Message);
            }
            Cancel(windowToClose);
        }
        private bool CanSave(Window windowToClose) =>
            windowToClose != null && !string.IsNullOrWhiteSpace(PoD) && !string.IsNullOrWhiteSpace(Hostname) &&
            Severity > 0 &&
            !string.IsNullOrWhiteSpace(Message);
        private static void Cancel(Window windowToClose) => windowToClose?.Close();
    }
}
