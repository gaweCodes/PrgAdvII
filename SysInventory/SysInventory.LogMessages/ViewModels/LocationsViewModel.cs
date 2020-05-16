using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using SysInventory.LogMessages.DataAccess.AdoNet;
using SysInventory.LogMessages.Extensions;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.ViewModels
{
    internal class LocationsViewModel : MasterDetailViewModel<Location, LocationTreeViewitem>, INotifyPropertyChanged
    {
        public LocationsViewModel()
        {
            DataRepository = new LocationRepository();
            LoadFilteredItemsCommand = new RelayCommand(SearchLocations);
            LoadAllItemsCommand = new RelayCommand(LoadLocationTree);
            LoadDetailsCommand = new RelayCommand<LocationTreeViewitem>(ShowLocationDetails);
            CountItemsCommand = new RelayCommand(CountLocations);
            CreateItemCommand = new RelayCommand(CreateNewLocation);
            ShowingItems = new ObservableCollection<LocationTreeViewitem>();
            SaveCurrentItemCommand = new RelayCommand(SaveCurrentLocation, IsItemSelected);
            DeleteItemCommand = new RelayCommand(DeleteSelectedLocation, IsItemSelected);
            LoadLocationTree();
        }
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
            if (string.IsNullOrEmpty(WhereCriteria) || string.IsNullOrEmpty(ParameterValues)) LoadLocationTree();
            else
            {
                var foundEntries = DataRepository.GetAll(WhereCriteria, ParseSearchValues());
                var messageText = string.Empty;
                foundEntries.ForEach(x => messageText += x + Environment.NewLine);
                MessageBox.Show(messageText);
            }
        }
        private void CountLocations()
        {
            if (string.IsNullOrEmpty(WhereCriteria) || string.IsNullOrEmpty(ParameterValues)) MessageBox.Show($"found {DataRepository.Count()} entries");
            else
            {
                var count = DataRepository.Count(WhereCriteria, ParseSearchValues());
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
            ShowingItems.Clear();
            var tree = locationList.GenerateTree(_ => _.Id, _ => _.ParentId).ToList();
            ShowingItems.Add(new LocationTreeViewitem
            {
                Item = new Location { Name = "Locations" },
                Children = tree
            });
        }
    }
}
