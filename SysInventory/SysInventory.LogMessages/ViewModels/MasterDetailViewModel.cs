using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SysInventory.LogMessages.Annotations;

namespace SysInventory.LogMessages.ViewModels
{
    internal abstract class MasterDetailViewModel<T, TCollection> : BaseViewModel<T>
    {
        private T _selectedItem;
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public string WhereCriteria { get; set; }
        public string ParameterValues { get; set; }
        protected Dictionary<string, object> ParseSearchValues()
        {
            var queryParams = new Dictionary<string, object>();
            foreach (var keyValuePair in ParameterValues.Split(';'))
            {
                var keyValuePairParsed = keyValuePair.Split('|');
                if (keyValuePairParsed.Length == 2) queryParams.Add(keyValuePairParsed[0], keyValuePairParsed[1]);
                else return new Dictionary<string, object>();
            }
            return queryParams;
        }
        public ObservableCollection<TCollection> ShowingItems { get; set; }
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                SaveCurrentItemCommand?.RaiseCanExecuteChanged();
                LoadDetailsCommand?.RaiseCanExecuteChanged();
                DeleteItemCommand?.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public IRelayCommand LoadFilteredItemsCommand { get; set; }
        public IRelayCommand LoadAllItemsCommand { get; set; }
        public IRelayCommand LoadDetailsCommand { get; set; }
        public IRelayCommand CountItemsCommand { get; set; }
        public IRelayCommand CreateItemCommand { get; set; }
        public IRelayCommand DeleteItemCommand { get; set; }
        public bool IsItemSelected() => SelectedItem != null;
        protected void PopulateShowingItemsList(IEnumerable<TCollection> listToAdd)
        {
            ShowingItems.Clear();
            foreach (var itemm in listToAdd) ShowingItems.Add(itemm);
        }
        protected T GetSingleEntry(Guid id) => DataRepository.GetSingle(id);
        protected void CountItems()
        {
            if (string.IsNullOrEmpty(WhereCriteria) || string.IsNullOrEmpty(ParameterValues)) MessageBox.Show($"found {DataRepository.Count()} entries");
            else
            {
                var count = DataRepository.Count(WhereCriteria, ParseSearchValues());
                if (count > -1) MessageBox.Show($"found {count} entries");
            }
        }
        protected abstract void SearchItems();
    }
}
