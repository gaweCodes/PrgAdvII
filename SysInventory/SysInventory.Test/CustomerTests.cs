using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.DataAccess.Ef;
using SysInventory.LogMessages.ViewModels;

namespace SysInventory.Test
{
    [TestFixture]
    internal class CustomerTests
    {
        [Test]
        public void CustomersViewModel_LoadAll_CallsGetAllOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryBase<Customer>>().Setup(x => x.GetAll()).Returns(() =>
                {
                    var list = new List<Customer> {new Customer {Id = Guid.NewGuid()}};
                    return list.AsQueryable();
                });
                var sut = mock.Create<CustomersViewModel>();

                sut.LoadAllCustomers();

                mock.Mock<IRepositoryBase<Customer>>().Verify(x => x.GetAll(), Times.Exactly(2));
            }
        }
        [Test]
        public void CustomersViewModel_LoadFiltered_CallsGetAllWithFilterTwice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryBase<Customer>>().Setup(x => x.GetAll()).Returns(() =>
                {
                    var list = new List<Customer> {new Customer {Id = Guid.NewGuid()}};
                    return list.AsQueryable();
                });
                var sut = mock.Create<CustomersViewModel>();

                sut.SearchCustomers();

                mock.Mock<IRepositoryBase<Customer>>().Verify(x => x.GetAll(), Times.Exactly(2));
            }
        }
        [Test]
        public void CustomersViewModel_LoadSingle_CallsGetSingleTwice()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                mock.Mock<IRepositoryBase<Customer>>().Setup(x => x.GetSingle(id)).Returns(() =>
                {
                    var list = new List<Customer> {new Customer {Id = id}};
                    return list.First();
                });
                var sut = mock.Create<CustomersViewModel>();

                sut.GetSingleEntry(id);
                
                mock.Mock<IRepositoryBase<Customer>>().Verify(x => x.GetSingle(id), Times.Exactly(1));
            }
        }
        [Test]
        public void CustomersViewModel_Count_CallsCountOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                mock.Mock<IRepositoryBase<Customer>>().Setup(x => x.Count()).Returns(() =>
                {
                    var list = new List<Customer> {new Customer {Id = id}};
                    return list.Count;
                });
                var sut = mock.Create<CustomersViewModel>();

                sut.Strategy = "asdf";
                sut.CountItems();

                mock.Mock<IRepositoryBase<Customer>>().Verify(x => x.Count(), Times.Exactly(1));
            }
        }
        [Test]
        public void CustomersViewModel_Add_CallsAddOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var itemToAdd = new Customer
                {
                    Id = Guid.Empty,
                    CustomerNumber = "CU12345",
                    Mail = "gabriel.weibel@hotmail.de",
                    Password = "asdfAsdf5",
                    Website = "www.gaebster.ch",
                    Name = "test",
                    Address = new Address{ Id = Guid.NewGuid()},
                    AddressType = new AddressType { Id = Guid.NewGuid() }
                };
                mock.Mock<IRepositoryBase<Customer>>().Setup(x => x.Add(itemToAdd));
                var sut = mock.Create<CustomersViewModel>();

                sut.SelectedItem = itemToAdd;
                sut.SaveCurrentCustomer();

                mock.Mock<IRepositoryBase<Customer>>().Verify(x => x.Add(itemToAdd), Times.Exactly(1));
            }
        }
        [Test]
        public void CustomersViewModel_Update_CallsUpdateOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                var itemToUpdate = new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerNumber = "CU12345",
                    Mail = "gabriel.weibel@hotmail.de",
                    Password = "asdfAsdf5",
                    Website = "www.gaebster.ch",
                    Name = "test",
                    Address = new Address{ Id = Guid.NewGuid()},
                    AddressType = new AddressType { Id = Guid.NewGuid() }
                };
                mock.Mock<IRepositoryBase<Customer>>().Setup(x => x.Update(itemToUpdate));
                var sut = mock.Create<CustomersViewModel>();

                sut.SelectedItem = itemToUpdate;
                sut.SaveCurrentCustomer();

                mock.Mock<IRepositoryBase<Customer>>().Verify(x => x.Update(itemToUpdate), Times.Exactly(1));
            }
        }
        [Test]
        public void CustomersViewModel_Delete_CallsDeleteOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                var itemToDelete = new Customer
                {
                    Id = Guid.NewGuid(),
                    CustomerNumber = "CU12345",
                    Mail = "gabriel.weibel@hotmail.de",
                    Password = "asdfAsdf5",
                    Website = "www.gaebster.ch",
                    Name = "test",
                    Address = new Address { Id = Guid.NewGuid() },
                    AddressType = new AddressType { Id = Guid.NewGuid() }
                };
                mock.Mock<IRepositoryBase<Customer>>().Setup(x => x.Delete(itemToDelete));
                var sut = mock.Create<CustomersViewModel>();

                sut.SelectedItem = itemToDelete;
                sut.DeleteCustomer();

                mock.Mock<IRepositoryBase<Customer>>().Verify(x => x.Delete(itemToDelete), Times.Exactly(1));
            }
        }
    }
}
