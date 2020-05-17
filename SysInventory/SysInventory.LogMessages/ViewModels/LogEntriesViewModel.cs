﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using DuplicateCheckerLib;
using SysInventory.LogMessages.DataAccess.AdoNet;
using SysInventory.LogMessages.Models;
using SysInventory.LogMessages.Properties;

namespace SysInventory.LogMessages.ViewModels
{
    internal class LogEntriesViewModel : MasterDetailViewModel<LogEntry, LogEntry>
    {
        public IRelayCommand FindDuplicateLogEntriesCommand { get; }
        public RelayCommand ShowInfoMessageCommand { get; }
        public RelayCommand OpenLocationWindowCommand { get; }
        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                LoadFilteredItemsCommand?.RaiseCanExecuteChanged();
                CreateItemCommand?.RaiseCanExecuteChanged();
                FindDuplicateLogEntriesCommand?.RaiseCanExecuteChanged();
                CountItemsCommand?.RaiseCanExecuteChanged();
                LoadFilteredItemsCommand?.RaiseCanExecuteChanged();
                OpenLocationWindowCommand?.RaiseCanExecuteChanged();
            }
        }
        public LogEntriesViewModel()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.ConnectionString))
            {
                Settings.Default.ConnectionString =
                    "Data Source=EPICPCGAEBSTER;Initial Catalog=SysInventory;User ID=user;Password=pw";
                Settings.Default.Save();
            }
            ConnectionString = Settings.Default.ConnectionString;
            DataRepository = new LogRepository();
            ShowingItems = new ObservableCollection<LogEntry>();
            LoadFilteredItemsCommand = new RelayCommand(LoadUnconfirmedLogEntries, CanConnectToDatabase);
            SaveCurrentItemCommand = new RelayCommand(ConfirmLogEntry, IsItemSelected);
            LoadDetailsCommand = new RelayCommand(ShowLogEntryDetails, IsItemSelected);
            DeleteItemCommand = new RelayCommand(DeleteLogEntry, IsItemSelected);
            CreateItemCommand = new RelayCommand(OpenAddLogEntryDialog, CanConnectToDatabase);
            FindDuplicateLogEntriesCommand = new RelayCommand(LoadDuplicateLogEntries, CanConnectToDatabase);
            LoadAllItemsCommand = new RelayCommand(LoadAllLogEntries, CanConnectToDatabase);
            ShowInfoMessageCommand = new RelayCommand(ShowInfoMessage);
            CountItemsCommand = new RelayCommand(CountItems, CanConnectToDatabase);
            LoadFilteredItemsCommand = new RelayCommand(SearchItems, CanConnectToDatabase);
            OpenLocationWindowCommand = new RelayCommand(OpenLocationWindow, CanConnectToDatabase);
        }
        private bool CanConnectToDatabase() => !string.IsNullOrWhiteSpace(ConnectionString);
        private void LoadUnconfirmedLogEntries()
        {
            try
            {
                UpdateSettings();
                var foundLogEntries = DataRepository.GetAll(string.Empty, new Dictionary<string, object>());
                PopulateShowingItemsList(foundLogEntries);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while loading the log entries: " + ex.Message);
            }
        }
        private void ShowLogEntryDetails() => MessageBox.Show(GetSingleEntry(SelectedItem.Id).ToString());
        private void ConfirmLogEntry()
        {
            try
            {
                DataRepository.Update(SelectedItem);
                LoadUnconfirmedLogEntries();
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured while confirming the log entry: " + e.Message);
            }
        }
        private void LoadAllLogEntries()
        {
            try
            {
                UpdateSettings();
                var foundLogEntries = DataRepository.GetAll();
                PopulateShowingItemsList(foundLogEntries);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while loading the log entries: " + ex.Message);
            }
        }
        private void OpenAddLogEntryDialog()
        {
            UpdateSettings();
            new AddLogEntryDialog().ShowDialog();
            LoadUnconfirmedLogEntries();
        }
        private void LoadDuplicateLogEntries()
        {
            if(ShowingItems.Count == 0)
                LoadUnconfirmedLogEntries();
            var duplicateChecker = new DuplicateChecker();
            var duplicates = duplicateChecker.FindDuplicates(ShowingItems);
            var castedDuplicates = duplicates.Where(entity => entity is LogEntry).Cast<LogEntry>();
            PopulateShowingItemsList(castedDuplicates);
        }
        private void UpdateSettings()
        {
            Settings.Default.ConnectionString = ConnectionString;
            Settings.Default.Save();
        }
        public void ShowInfoMessage() => MessageBox.Show(
            $"Product: SysInventory {Environment.NewLine}Version: {Assembly.GetExecutingAssembly().GetName().Version} {Environment.NewLine}Author: Gabriel Weibel{Environment.NewLine}Support: admin@gaebster.ch");
        private void DeleteLogEntry()
        {
            DataRepository.Delete(SelectedItem);
            LoadUnconfirmedLogEntries();
        }
        private void OpenLocationWindow()
        {
            UpdateSettings();
            new Locations().ShowDialog();
        }
        protected override void SearchItems()
        {
            if (string.IsNullOrEmpty(WhereCriteria) || string.IsNullOrEmpty(ParameterValues)) LoadUnconfirmedLogEntries();
            else PopulateShowingItemsList(DataRepository.GetAll(WhereCriteria, ParseSearchValues()));
        }
    }
}