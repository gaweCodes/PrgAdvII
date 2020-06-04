using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using SysInventory.LogMessages.DataAccess.Ef;

namespace SysInventory.LogMessages.ViewModels
{
    internal class CustomersViewModel : MasterDetailViewModel<Customer, Customer>
    {
        public string CustomerNoSearchParam { get; set; }
        public string NameSearchParam { get; set; }
        public string MailSearchParam { get; set; }
        public RelayCommand SearchCommand { get; }
        public CustomersViewModel()
        {
            ShowingItems = new ObservableCollection<Customer>();
            SearchCommand = new RelayCommand(SearchCustomers);
            DataRepository = new CustomerRepository();
            LoadAllCustomers();
        }
        private void SearchCustomers()
        {
            Expression<Func<Customer, bool>> whereExpression = null;
            if (!string.IsNullOrWhiteSpace(CustomerNoSearchParam))
                whereExpression = c => c.Id.ToString().Contains(CustomerNoSearchParam);
            if (!string.IsNullOrWhiteSpace(NameSearchParam))
                whereExpression = c => c.Name.Contains(NameSearchParam);
            if (!string.IsNullOrWhiteSpace(MailSearchParam))
                whereExpression = c => c.Mail.Contains(MailSearchParam);
            if (whereExpression == null)
            {
                LoadAllCustomers();
                return;
            }
            var filteredCustomers = DataRepository.GetAll(whereExpression);
            PopulateShowingItemsList(filteredCustomers);
        }
        private void LoadAllCustomers()
        {
            var customers = DataRepository.GetAll();
            PopulateShowingItemsList(customers);
        }
    }
}
