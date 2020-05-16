using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using SysInventory.LogMessages.Annotations;
using SysInventory.LogMessages.DataAccess.AdoNet;
using SysInventory.LogMessages.Extensions;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.ViewModels
{
    internal class LocationsViewModel : BaseViewModel<Location>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private Location _selectedItem;
        public Location SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SaveCurrentLocationCommand?.RaiseCanExecuteChanged();
                DeleteLocationCommand?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public RelayCommand LoadLocationTreeCommand { get; }
        public RelayCommand SearchLocationsCommand { get; }
        public RelayCommand CountLocationsCommand { get; }
        public RelayCommand CreateLocationCommand { get; }
        public RelayCommand SaveCurrentLocationCommand { get; }
        public RelayCommand<LocationTreeViewitem> TreeviewSelectedItemChangedCommand { get; }
        public RelayCommand DeleteLocationCommand { get; }
        public ObservableCollection<LocationTreeViewitem> LocationTree { get; }
        public LocationsViewModel()
        {
            DataRepository = new LocationRepository();
            LoadLocationTreeCommand = new RelayCommand(LoadLocationTree);
            CreateLocationCommand = new RelayCommand(CreateNewLocation);
            LocationTree = new ObservableCollection<LocationTreeViewitem>();
            TreeviewSelectedItemChangedCommand = new RelayCommand<LocationTreeViewitem>(ShowLocationDetails);
            SaveCurrentLocationCommand = new RelayCommand(SaveCurrentLocation, IsItemInDetailMode);
            DeleteLocationCommand = new RelayCommand(DeleteSelectedLocation, IsItemInDetailMode);
            SearchLocationsCommand = new RelayCommand(SearchLocations);
            CountLocationsCommand = new RelayCommand(CountLocations);
            LoadLocationTree();
        }
        private bool IsItemInDetailMode() => SelectedItem != null;
        private void LoadLocationTree()
        {
            var locationList = DataRepository.GetAll();
            PopulateLocationTree(locationList);
        }
        private void ShowLocationDetails(LocationTreeViewitem selectedItem)
        {
            if (selectedItem?.Item == null || selectedItem.Item.Id == Guid.Empty) return;
            var locationDetails = DataRepository.GetSingle(selectedItem.Item.Id);
            SelectedItem = locationDetails;
        }
        private void SaveCurrentLocation()
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Name) || SelectedItem.PoDId == Guid.Empty)
            {
                MessageBox.Show("Pod Id and Location Name contain invalid data");
                return;
            }
            if(SelectedItem.Id == Guid.Empty) DataRepository.Add(SelectedItem);
            else DataRepository.Update(SelectedItem);
            LoadLocationTree();
            SelectedItem = null;
        }
        private void CreateNewLocation() => SelectedItem = new Location {ParentId = SelectedItem?.Id};
        private void SearchLocations()
        {
            if (string.IsNullOrEmpty(WhereCrit) || string.IsNullOrEmpty(Values)) LoadLocationTree();
            else
            {
                var foundEntries = DataRepository.GetAll(WhereCrit, ParseSearchValues());
                var messageText = string.Empty;
                foundEntries.ForEach(x => messageText += x + Environment.NewLine);
                MessageBox.Show(messageText);
            }
        }
        private void CountLocations()
        {
            if (string.IsNullOrEmpty(WhereCrit) || string.IsNullOrEmpty(Values)) MessageBox.Show($"found {DataRepository.Count()} entries");
            else
            {
                var count = DataRepository.Count(WhereCrit, ParseSearchValues());
                if (count > -1) MessageBox.Show($"found {count} entries");
            }
        }
        private void DeleteSelectedLocation()
        {
            DataRepository.Delete(SelectedItem);
            LoadLocationTree();
            SelectedItem = null;
        }
        private void PopulateLocationTree(IEnumerable<Location> locationList)
        {
            LocationTree.Clear();
            var tree = locationList.GenerateTree(_ => _.Id, _ => _.ParentId).ToList();
            LocationTree.Add(new LocationTreeViewitem
            {
                Item = new Location { Name = "Locations" },
                Children = tree
            });
        }
    }
}
