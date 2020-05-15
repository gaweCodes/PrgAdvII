using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using DuplicateCheckerLib;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.DataAccess.AdoNet;
using SysInventory.LogMessages.Models;
using SysInventory.LogMessages.Properties;

namespace SysInventory.LogMessages.ViewModels
{
    internal class LogEntriesViewModel
    {
        private readonly IRepositoryBase<LogEntry> _logRepository;
        public RelayCommand LoadUnconfirmedLogEntriesCommand { get; }
        public RelayCommand FindDuplicateLogEntriesCommand { get; }
        public RelayCommand LoadAllLogEntriesCommand { get; }
        public RelayCommand ShowDetailsCommand { get; }
        public RelayCommand CountLogEntriesCommand { get; }
        public RelayCommand ConfirmLogEntryCommand { get; }
        public RelayCommand DeleteLogEntryCommand { get; }
        public RelayCommand ShowInfoMessageCommand { get; }
        public RelayCommand AddLogEntryCommand { get; }
        public RelayCommand SearchLogEntriesCommand { get; }
        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                LoadUnconfirmedLogEntriesCommand?.RaiseCanExecuteChanged();
                AddLogEntryCommand?.RaiseCanExecuteChanged();
                FindDuplicateLogEntriesCommand?.RaiseCanExecuteChanged();
                CountLogEntriesCommand?.RaiseCanExecuteChanged();
                SearchLogEntriesCommand?.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<LogEntry> UnconfirmedLogEntries { get; }
        private LogEntry _selectedLogEntry;
        public LogEntry SelectedLogEntry
        {
            get => _selectedLogEntry;
            set
            {
                _selectedLogEntry = value;
                ConfirmLogEntryCommand?.RaiseCanExecuteChanged();
                ShowDetailsCommand?.RaiseCanExecuteChanged();
                DeleteLogEntryCommand?.RaiseCanExecuteChanged();
            }
        }
        public string WhereCrit { get; set; }
        public string Values { get; set; }
        public LogEntriesViewModel()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.ConnectionString))
            {
                Settings.Default.ConnectionString =
                    "Data Source=EPICPCGAEBSTER;Initial Catalog=SysInventory;User ID=user;Password=pw";
                Settings.Default.Save();
            }
            ConnectionString = Settings.Default.ConnectionString;
            _logRepository = new LogRepository();
            UnconfirmedLogEntries = new ObservableCollection<LogEntry>();
            LoadUnconfirmedLogEntriesCommand = new RelayCommand(LoadUnconfirmedLogEntries, CanConnectToDatabase);
            ConfirmLogEntryCommand = new RelayCommand(ConfirmLogEntry, IsEntrySelected);
            ShowDetailsCommand = new RelayCommand(ShowLogEntryDetails, IsEntrySelected);
            DeleteLogEntryCommand = new RelayCommand(DeleteLogEntry, IsEntrySelected);
            AddLogEntryCommand = new RelayCommand(OpenAddDialog, CanConnectToDatabase);
            FindDuplicateLogEntriesCommand = new RelayCommand(LoadDuplicateLogEntries, CanConnectToDatabase);
            LoadAllLogEntriesCommand = new RelayCommand(LoadAllLogEntries, CanConnectToDatabase);
            ShowInfoMessageCommand = new RelayCommand(ShowInfoMessage);
            CountLogEntriesCommand = new RelayCommand(CountLogEntries, CanConnectToDatabase);
            SearchLogEntriesCommand = new RelayCommand(SearchLogEntries, CanConnectToDatabase);
        }
        private bool CanConnectToDatabase() => !string.IsNullOrWhiteSpace(ConnectionString);
        private void LoadUnconfirmedLogEntries()
        {
            try
            {
                UpdateSettings();
                var foundLogEntries = _logRepository.GetAll(string.Empty, new Dictionary<string, object>());
                PopulateUnconfirmedLogEntriesCollection(foundLogEntries);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while loading the log entries: " + ex.Message);
            }
        }
        private void LoadAllLogEntries()
        {
            try
            {
                UpdateSettings();
                var foundLogEntries = _logRepository.GetAll();
                PopulateUnconfirmedLogEntriesCollection(foundLogEntries);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while loading the log entries: " + ex.Message);
            }
        }
        private bool IsEntrySelected() => SelectedLogEntry != null;
        private void ConfirmLogEntry()
        {
            try
            {
                _logRepository.Update(SelectedLogEntry);
                LoadUnconfirmedLogEntries();
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured while confirming the log entry: " + e.Message);
            }
        }
        private void OpenAddDialog()
        {
            UpdateSettings();
            new AddEntryDialog().ShowDialog();
            LoadUnconfirmedLogEntries();
        }
        private void LoadDuplicateLogEntries()
        {
            if(UnconfirmedLogEntries.Count == 0)
                LoadUnconfirmedLogEntries();
            var duplicateChecker = new DuplicateChecker();
            var duplicates = duplicateChecker.FindDuplicates(UnconfirmedLogEntries);
            var castedDuplicates = duplicates.Where(entity => entity is LogEntry).Cast<LogEntry>();
            PopulateUnconfirmedLogEntriesCollection(castedDuplicates);
        }
        private void UpdateSettings()
        {
            Settings.Default.ConnectionString = ConnectionString;
            Settings.Default.Save();
        }
        private void PopulateUnconfirmedLogEntriesCollection(IEnumerable<LogEntry> entriesToAdd)
        {
            UnconfirmedLogEntries.Clear();
            foreach (var duplicate in entriesToAdd)
                UnconfirmedLogEntries.Add(duplicate);
        }
        public void ShowInfoMessage() => MessageBox.Show($"Product: SysInventory {Environment.NewLine}Version: {Assembly.GetExecutingAssembly().GetName().Version} {Environment.NewLine}Author: Gabriel Weibel{Environment.NewLine}Support: admin@gaebster.ch");
        private void ShowLogEntryDetails()
        {
            var foundEntry = _logRepository.GetSingle(SelectedLogEntry.Id);
            MessageBox.Show(foundEntry?.ToString());
        }
        private void DeleteLogEntry()
        {
            _logRepository.Delete(SelectedLogEntry);
            LoadUnconfirmedLogEntries();
        }
        private void CountLogEntries()
        {
            if (string.IsNullOrEmpty(WhereCrit) || string.IsNullOrEmpty(Values)) MessageBox.Show($"found {_logRepository.Count()} entries");
            else
            {
                var count = _logRepository.Count(WhereCrit, ParseSearchValues());
                if(count > -1) MessageBox.Show($"found {count} entries");
            }
        }
        private void SearchLogEntries()
        {
            if (string.IsNullOrEmpty(WhereCrit) || string.IsNullOrEmpty(Values)) LoadUnconfirmedLogEntries();
            else
            {
                var foundEntries = _logRepository.GetAll(WhereCrit, ParseSearchValues());
                PopulateUnconfirmedLogEntriesCollection(foundEntries);
            }

        }
        private Dictionary<string, object> ParseSearchValues()
        {
            var queryParams = new Dictionary<string, object>();
            foreach (var keyValuePair in Values.Split(';'))
            {
                var keyValuePairParsed = keyValuePair.Split('|');
                if (keyValuePairParsed.Length == 2) queryParams.Add(keyValuePairParsed[0], keyValuePairParsed[1]);
                else return new Dictionary<string, object>();
            }
            return queryParams;
        }
    }
}
