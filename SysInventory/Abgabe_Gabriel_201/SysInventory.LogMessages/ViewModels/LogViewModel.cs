using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using DuplicateCheckerLib;
using SysInventory.LogMessages.Models;
using SysInventory.LogMessages.Properties;

namespace SysInventory.LogMessages.ViewModels
{
    internal class LogViewModel
    {
        public RelayCommand LoadUnconfirmedLogMessagesCommand { get; }
        public RelayCommand FindDuplicateLogMessagesCommand { get; }
        public RelayCommand ConfirmLogMessageCommand { get; }
        public RelayCommand ShowInfoMessageCommand { get; }
        public RelayCommand AddCommand { get; }
        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                LoadUnconfirmedLogMessagesCommand?.RaiseCanExecuteChanged();
                AddCommand?.RaiseCanExecuteChanged();
                FindDuplicateLogMessagesCommand?.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<LogMessage> UnconfirmedLogMessages { get; }
        private LogMessage _selectedLogMessage;
        public LogMessage SelectedLogMessage
        {
            get => _selectedLogMessage;
            set
            {
                _selectedLogMessage = value;
                ConfirmLogMessageCommand?.RaiseCanExecuteChanged();
            }
        }
        public LogViewModel()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.ConnectionString))
            {
                Settings.Default.ConnectionString =
                    "Data Source=EPICPCGAEBSTER;Initial Catalog=SysInventory;User ID=user;Password=pw";
                Settings.Default.Save();
            }
            ConnectionString = Settings.Default.ConnectionString;
            UnconfirmedLogMessages = new ObservableCollection<LogMessage>();
            LoadUnconfirmedLogMessagesCommand = new RelayCommand(LoadUnconfirmedLogMessages, CanConnectToDatabase);
            ConfirmLogMessageCommand = new RelayCommand(ConfirmLogMessage, CanConfirmLogMessage);
            AddCommand = new RelayCommand(OpenAddDialog, CanConnectToDatabase);
            FindDuplicateLogMessagesCommand = new RelayCommand(LoadDuplicateLogMessages, CanConnectToDatabase);
            ShowInfoMessageCommand = new RelayCommand(ShowInfoMessage);
        }
        private bool CanConnectToDatabase() => !string.IsNullOrWhiteSpace(ConnectionString);
        private void LoadUnconfirmedLogMessages()
        {
            try
            {
                UpdateSettings();
                RequestDatabaseByRequestType(DatabaseRequestType.Query);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten: " + ex.Message);
            }
        }
        private bool CanConfirmLogMessage() => SelectedLogMessage != null;
        private void ConfirmLogMessage()
        {
            try
            {
                RequestDatabaseByRequestType(DatabaseRequestType.StoredProcedure);
                LoadUnconfirmedLogMessages();
            }
            catch (Exception e)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten: " + e.Message);
            }
        }
        private void OpenAddDialog()
        {
            UpdateSettings();
            new AddDialog().ShowDialog();
            LoadUnconfirmedLogMessages();
        }
        private void LoadDuplicateLogMessages()
        {
            if(UnconfirmedLogMessages.Count == 0)
                LoadUnconfirmedLogMessages();
            var duplicateChecker = new DuplicateChecker();
            var duplicates = duplicateChecker.FindDuplicates(UnconfirmedLogMessages);
            var castedDuplicates = duplicates.Where(entity => entity is LogMessage).Cast<LogMessage>();
            PopulateUnconfirmedLogMessagesCollection(castedDuplicates);
        }
        private void UpdateSettings()
        {
            Settings.Default.ConnectionString = ConnectionString;
            Settings.Default.Save();
        }
        private void RequestDatabaseByRequestType(DatabaseRequestType requestType)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                if (requestType == DatabaseRequestType.Query)
                    SendQueryRequest(connection);
                else
                    ExecuteStoredProcedureOnConnection(connection);
            }
        }
        private void SendQueryRequest(SqlConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText =
                    "select id, pod, location, hostname, severity, timestamp, message from v_logentries order by timestamp";
                using (var reader = cmd.ExecuteReader())
                    PopulateUnconfirmedLogMessagesCollection(reader);
            }
        }
        private void PopulateUnconfirmedLogMessagesCollection(IDataReader reader)
        {
            UnconfirmedLogMessages.Clear();
            while (reader.Read())
            {
                UnconfirmedLogMessages.Add(new LogMessage
                {
                    Id = Guid.Parse(reader.GetValue(0).ToString()),
                    PoD = reader.GetValue(1).ToString(),
                    Location = reader.GetValue(2).ToString(),
                    Hostname = reader.GetValue(3).ToString(),
                    Severity = int.Parse(reader.GetValue(4).ToString()),
                    Timestamp = DateTime.Parse(reader.GetValue(5).ToString()),
                    Message = reader.GetValue(6).ToString()
                });
            }
        }
        private void PopulateUnconfirmedLogMessagesCollection(IEnumerable<LogMessage> logMessgesToAdd)
        {
            UnconfirmedLogMessages.Clear();
            foreach (var duplicate in logMessgesToAdd)
                UnconfirmedLogMessages.Add(duplicate);
        }
        private void ExecuteStoredProcedureOnConnection(SqlConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "LogClear";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SelectedLogMessage.Id);
                cmd.ExecuteNonQuery();
            }
        }
        public void ShowInfoMessage() => MessageBox.Show("SysInventory by Gabriel Weibel");
    }
}
