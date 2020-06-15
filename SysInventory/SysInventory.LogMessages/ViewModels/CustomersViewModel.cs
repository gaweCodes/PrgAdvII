using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using SysInventory.LogMessages.DataAccess.Ef;

namespace SysInventory.LogMessages.ViewModels
{
    internal class CustomersViewModel : MasterDetailViewModel<Customer, Customer>, INotifyPropertyChanged
    {
        private readonly MyRegExValidations _regExValidation = new MyRegExValidations();
        public string CustomerNoSearchParam { get; set; }
        public string NameSearchParam { get; set; }
        public string MailSearchParam { get; set; }
        public RelayCommand SearchCommand { get; }
        public ObservableCollection<Address> Addresses { get; }
        public ObservableCollection<AddressType> AddressTypes { get; }
        public CustomersViewModel()
        {
            ShowingItems = new ObservableCollection<Customer>();
            Addresses = new ObservableCollection<Address>();
            AddressTypes = new ObservableCollection<AddressType>();
            SearchCommand = new RelayCommand(SearchCustomers);
            LoadDetailsCommand = new RelayCommand<Customer>(LoadDetails);
            DataRepository = new CustomerRepository();
            SaveCurrentItemCommand = new RelayCommand(SaveCurrentCustomer, IsItemSelected);
            CreateItemCommand = new RelayCommand(CreateEmptyCustomer);
            DeleteItemCommand = new RelayCommand(DeleteCustomer, IsItemSelected);
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
        private void LoadDetails(Customer selectedItem)
        {
            SelectedItem = selectedItem;
            LoadAddressTypes();
            LoadAddresses();
        }
        private void LoadAddresses()
        {
            if (SelectedItem == null) return;
            var addressRepo = new AddressRepository();
            Addresses.Clear();
            foreach (var address in addressRepo.GetAll()) Addresses.Add(address);
            SelectedItem.Address = addressRepo.GetSingle(SelectedItem.AddressFk);
        }
        private void LoadAddressTypes()
        {
            if(SelectedItem == null) return;
            var addressTypeRepo = new AddressTypeRepository();
            AddressTypes.Clear();
            foreach (var addresstype in addressTypeRepo.GetAll()) AddressTypes.Add(addresstype);
            SelectedItem.AddressType = addressTypeRepo.GetSingle(SelectedItem.AddressTypeFk);
        }
        private void SaveCurrentCustomer()
        {
            if(!ValidateCustomer()) return;
            if (SelectedItem.Id == Guid.Empty) DataRepository.Add(SelectedItem);
            else DataRepository.Update(SelectedItem);
            LoadAllCustomers();
            SelectedItem = null;
        }
        private void CreateEmptyCustomer()
        {
            SelectedItem = new Customer();
            LoadAddressTypes();
            LoadAddresses();
        }
        private void DeleteCustomer()
        {
            DataRepository.Delete(SelectedItem);
            LoadAllCustomers();
            SelectedItem = null;
        }
        private bool ValidateCustomer()
        {
            if (!_regExValidation.ValidateCustomerNo(SelectedItem.CustomerNumber))
            {
                MessageBox.Show("Invalid customer number. It must start with CU following 5 numbers");
                return false;
            }
            if (!_regExValidation.ValidateEMail(SelectedItem.Mail))
            {
                MessageBox.Show(@"Invalid mail address.");
                return false;
            }
            return true;
        }
    }
}
