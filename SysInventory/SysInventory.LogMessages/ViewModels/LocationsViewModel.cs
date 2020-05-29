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
            LoadFilteredItemsCommand = new RelayCommand(SearchItems);
            LoadAllItemsCommand = new RelayCommand(LoadLocationsTree);
            LoadDetailsCommand = new RelayCommand<LocationTreeViewitem>(ShowLocationDetails);
            CountItemsCommand = new RelayCommand(CountItems);
            CreateItemCommand = new RelayCommand(CreateNewLocation);
            ShowingItems = new ObservableCollection<LocationTreeViewitem>();
            SaveCurrentItemCommand = new RelayCommand(SaveCurrentLocation, IsItemSelected);
            DeleteItemCommand = new RelayCommand(DeleteSelectedLocation, IsItemSelected);
            LoadLocationsTree();
        }
        private void LoadLocationsTree()
        {
            try
            {
                var locationList = DataRepository.GetAll();
                var tree = locationList.GenerateTree(_ => _.Id, _ => _.ParentId).ToList();
                var listToAdd = new List<LocationTreeViewitem>
                {
                    new LocationTreeViewitem
                    {
                        Item = new Location {Name = "Locations"},
                        Children = tree
                    }
                };
                PopulateShowingItemsList(listToAdd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while loading the locations: " + ex.Message);
            }
        }
        private void ShowLocationDetails(LocationTreeViewitem selectedItem)
        {
            if (selectedItem?.Item == null || selectedItem.Item.Id == Guid.Empty) return;
            SelectedItem = GetSingleEntry(selectedItem.Item.Id);
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
            LoadLocationsTree();
            SelectedItem = null;
        }
        private void CreateNewLocation() => SelectedItem = new Location {ParentId = SelectedItem?.Id};
        protected override void SearchItems()
        {
            if (string.IsNullOrEmpty(WhereCriteria) || string.IsNullOrEmpty(ParameterValues)) LoadLocationsTree();
            else
            {
                var foundEntries = DataRepository.GetAll(WhereCriteria, ParseSearchValues());
                var messageText = string.Empty;
                foundEntries.ToList().ForEach(x => messageText += x + Environment.NewLine);
                MessageBox.Show(messageText);
            }
        }
        private void DeleteSelectedLocation()
        {
            DataRepository.Delete(SelectedItem);
            LoadLocationsTree();
            SelectedItem = null;
        }
    }
}
