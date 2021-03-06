﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.DataAccess.Ef;

namespace SysInventory.LogMessages.ViewModels
{
    public class CustomersViewModel : MasterDetailViewModel<Customer, Customer>, INotifyPropertyChanged
    {
        private readonly MyRegExValidations _regExValidation = new MyRegExValidations();
        private IRepositoryBase<AddressType> _addressTypeRepo;
        private IRepositoryBase<Address> _addressRepo;
        public string CustomerNoSearchParam { get; set; }
        public string NameSearchParam { get; set; }
        public string MailSearchParam { get; set; }
        public RelayCommand SearchCommand { get; }
        public ObservableCollection<Address> Addresses { get; }
        public ObservableCollection<AddressType> AddressTypes { get; }
        public CustomersViewModel(IRepositoryBase<Customer> repo, IRepositoryBase<AddressType> addressTypeRepo, IRepositoryBase<Address> addressRepo)
        {
            ShowingItems = new ObservableCollection<Customer>();
            Addresses = new ObservableCollection<Address>();
            AddressTypes = new ObservableCollection<AddressType>();
            SearchCommand = new RelayCommand(SearchCustomers);
            LoadDetailsCommand = new RelayCommand<Customer>(LoadDetails);
            DataRepository = repo;
            _addressTypeRepo = addressTypeRepo;
            _addressRepo = addressRepo;
            SaveCurrentItemCommand = new RelayCommand(SaveCurrentCustomer, IsItemSelected);
            CreateItemCommand = new RelayCommand(CreateEmptyCustomer);
            DeleteItemCommand = new RelayCommand(DeleteCustomer, IsItemSelected);
            LoadAllCustomers();
        }
        public void SearchCustomers()
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
        public void LoadAllCustomers()
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
            Addresses.Clear();
            foreach (var address in _addressRepo.GetAll()) Addresses.Add(address);
            SelectedItem.Address = _addressRepo.GetSingle(SelectedItem.AddressFk);
        }
        private void LoadAddressTypes()
        {
            if(SelectedItem == null) return;
            AddressTypes.Clear();
            foreach (var addresstype in _addressTypeRepo.GetAll()) AddressTypes.Add(addresstype);
            SelectedItem.AddressType = _addressTypeRepo.GetSingle(SelectedItem.AddressTypeFk);
        }
        public void SaveCurrentCustomer()
        {
            if(!ValidateCustomer()) return;
            SelectedItem.Password = ComputeSHA256Hash(SelectedItem.Password);
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
        public void DeleteCustomer()
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
            if (!_regExValidation.ValidateUrl(SelectedItem.Website))
            {
                MessageBox.Show(@"Invalid url.");
                return false;
            }
            if (!_regExValidation.ValidatePassword(SelectedItem.Password))
            {
                MessageBox.Show(@"Invalid invalid password.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(SelectedItem.Name))
            {
                MessageBox.Show(@"Name is required.");
                return false;
            }
            if (SelectedItem.Address == null)
            {
                MessageBox.Show(@"Select an address.");
                return false;
            }
            if (SelectedItem.AddressType == null)
            {
                MessageBox.Show(@"Select an address type.");
                return false;
            }
            return true;
        }
        private string ComputeSHA256Hash(string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(text))).Replace("-", "");
            }
        }
    }
}
