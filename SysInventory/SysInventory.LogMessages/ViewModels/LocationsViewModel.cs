using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.Extensions;
using SysInventory.LogMessages.Models;
using IContainer = Autofac.IContainer;

namespace SysInventory.LogMessages.ViewModels
{
    public class LocationsViewModel : MasterDetailViewModel<ILocation, LocationTreeViewitem>, INotifyPropertyChanged
    {
        private string _connectionStrategy;
        public string ConnectionStrategy
        {
            get => _connectionStrategy;
            set
            {
                _connectionStrategy = value;
                Strategy = value;
                DataRepository = Factory.GetLocationRepository(value);
                LoadLocationsTree();
            }
        }
        private IContainer _container;
        public IContainer Container
        {
            private get { return _container; }
            set
            {
                _container = value;
                Factory = new RepositoryFactory(Container);
                ConnectionStrategy = "AdoNet";
            }
        }
        public LocationsViewModel(IRepositoryBase<ILocation> repo)
        {
            ShowingItems = new ObservableCollection<LocationTreeViewitem>();
            DataRepository = repo;
            LoadFilteredItemsCommand = new RelayCommand(SearchItems);
            LoadAllItemsCommand = new RelayCommand(LoadLocationsTree);
            LoadDetailsCommand = new RelayCommand<LocationTreeViewitem>(ShowLocationDetails);
            CountItemsCommand = new RelayCommand(CountItems);
            CreateItemCommand = new RelayCommand(CreateNewLocation);
            SaveCurrentItemCommand = new RelayCommand(SaveCurrentLocation, IsItemSelected);
            DeleteItemCommand = new RelayCommand(DeleteSelectedLocation, IsItemSelected);
        }
        public void LoadLocationsTree()
        {
            try
            {
                var locationList = DataRepository.GetAll();
                var tree = locationList.AsEnumerable().GenerateTree(_ => _.Id, _ => _.ParentId).ToList();
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
        public void SaveCurrentLocation()
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
        public void SearchItems()
        {
            if (string.IsNullOrEmpty(WhereCriteria) || string.IsNullOrEmpty(ParameterValues)) LoadLocationsTree();
            else
            {
                IQueryable<ILocation> foundEntries;
                if(ConnectionStrategy == "AdoNet")
                    foundEntries = DataRepository.GetAll(WhereCriteria, ParseSearchValues());
                else
                {
                    Expression<Func<ILocation, bool>> exp = loc => !string.IsNullOrWhiteSpace(loc.Name);
                    exp = null;
                    foundEntries = DataRepository.GetAll(exp);
                }
                var messageText = string.Empty;
                foundEntries.ToList().ForEach(x => messageText += x + Environment.NewLine);
                MessageBox.Show(messageText);
            }
        }
        public void DeleteSelectedLocation()
        {
            DataRepository.Delete(SelectedItem);
            LoadLocationsTree();
            SelectedItem = null;
        }
    }
}
