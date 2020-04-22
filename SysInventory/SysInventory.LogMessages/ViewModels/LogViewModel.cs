﻿using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using SysInventory.LogMessages.Models;
using SysInventory.LogMessages.Properties;

namespace SysInventory.LogMessages.ViewModels
{
    internal class LogViewModel
    {
        public RelayCommand LoadDataCommand { get; }
        public RelayCommand ConfirmCommand { get; }
        public RelayCommand AddCommand { get; }
        private string _connectionString;
        public string ConnectionString 
        { 
            get => _connectionString;
            set 
            { 
                _connectionString = value;
                LoadDataCommand?.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<LogMessage> LogMessages { get; }
        private LogMessage _selectedMessage;
        public LogMessage SelectedMessage
        {
            get => _selectedMessage;
            set 
            { 
                _selectedMessage = value; 
                ConfirmCommand?.RaiseCanExecuteChanged();
            }
        }
        public LogViewModel()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.ConnectionString))
            {
                Settings.Default.ConnectionString =
                    "Data Source=EPICPCGAEBSTER;Initial Catalog=SysInventory;User ID=sa;Password=FCBasel1893.";
                Settings.Default.Save();
            }
            ConnectionString = Settings.Default.ConnectionString;
            LogMessages = new ObservableCollection<LogMessage>();
            LoadDataCommand = new RelayCommand(LoadData, CanLoadData);
            ConfirmCommand = new RelayCommand(ConfirmMessage, CanConfirmMessage);
            AddCommand = new RelayCommand(OpenAddDialog);
        }
        private void LoadData()
        {
            try
            {
                LogMessages.Clear();
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText =
                            "select id, pod, location, hostname, severity, timestamp, message from v_logentries order by timestamp";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LogMessages.Add(new LogMessage
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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten: " + ex.Message);
            }
        }
        private bool CanLoadData() => !string.IsNullOrWhiteSpace(ConnectionString);
        private bool CanConfirmMessage() => SelectedMessage != null;
        private void ConfirmMessage()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "LogClear";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", SelectedMessage.Id);
                        cmd.ExecuteNonQuery();
                        LoadData();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Es ist ein Fehler aufgetreten: " + e.Message);
            }
        }
        private void OpenAddDialog()
        {
            new AddDialog().ShowDialog();
            LoadData();
        }
    }
}