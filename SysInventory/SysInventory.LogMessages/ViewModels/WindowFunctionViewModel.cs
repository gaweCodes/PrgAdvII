using System.Collections.Generic;
using System.Collections.ObjectModel;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.DataAccess.Ef;

namespace SysInventory.LogMessages.ViewModels
{
    internal class WindowFunctionViewModel
    {
        public RelayCommand LoadWindowFunctionResultsCommand { get; }
        public ObservableCollection<WindowFunction> ShowingItems { get; set; }
        private readonly IRepositoryBase<WindowFunction> _dataRepository;
        public WindowFunctionViewModel(IRepositoryBase<WindowFunction> repo)
        {
            ShowingItems = new ObservableCollection<WindowFunction>();
            _dataRepository = repo;
            LoadWindowFunctionResultsCommand = new RelayCommand(LoadWindowFunctionResults);
        }
        private void LoadWindowFunctionResults()
        {
            var functionResults = _dataRepository.GetAll();
            PopulateShowingItemsList(functionResults);
        }
        private void PopulateShowingItemsList(IEnumerable<WindowFunction> listToAdd)
        {
            ShowingItems.Clear();
            foreach (var itemm in listToAdd) ShowingItems.Add(itemm);
        }
    }
}
