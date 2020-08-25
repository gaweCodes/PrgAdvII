using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Moq;
using Moq;
using NUnit.Framework;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.Models;
using SysInventory.LogMessages.ViewModels;

namespace SysInventory.Test
{
    [TestFixture]
    internal class LocationTests
    {
        [Test]
        public void LocationsViewModel_LoadAll_CallsGetAllOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryBase<ILocation>>().Setup(x => x.GetAll()).Returns(() =>
                {
                    var list = new List<ILocation> {new Location {Id = Guid.NewGuid()}};
                    return list.AsQueryable();
                });
                var sut = mock.Create<LocationsViewModel>();

                sut.LoadLocationsTree();

                mock.Mock<IRepositoryBase<ILocation>>().Verify(x => x.GetAll(), Times.Exactly(1));
            }
        }
        [Test]
        public void LocationsViewModel_LoadFiltered_CallsGetAllWithFilterOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryBase<ILocation>>().Setup(x => x.GetAll()).Returns(() =>
                {
                    var list = new List<ILocation> {new Location {Id = Guid.NewGuid()}};
                    return list.AsQueryable();
                });
                var sut = mock.Create<LocationsViewModel>();

                sut.SearchItems();

                mock.Mock<IRepositoryBase<ILocation>>().Verify(x => x.GetAll(), Times.Exactly(1));
            }
        }
        [Test]
        public void LocationsViewModel_LoadSingle_CallsGetSingleOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                mock.Mock<IRepositoryBase<ILocation>>().Setup(x => x.GetSingle(id)).Returns(() =>
                {
                    var list = new List<ILocation> {new Location {Id = id}};
                    return list.First();
                });
                var sut = mock.Create<LocationsViewModel>();

                sut.GetSingleEntry(id);
                
                mock.Mock<IRepositoryBase<ILocation>>().Verify(x => x.GetSingle(id), Times.Exactly(1));
            }
        }
        [Test]
        public void LocationsViewModel_Count_CallsCountOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                mock.Mock<IRepositoryBase<ILocation>>().Setup(x => x.Count()).Returns(() =>
                {
                    var list = new List<ILocation> {new Location {Id = id}};
                    return list.Count;
                });
                var sut = mock.Create<LocationsViewModel>();

                sut.Strategy = "asdf";
                sut.CountItems();

                mock.Mock<IRepositoryBase<ILocation>>().Verify(x => x.Count(), Times.Exactly(1));
            }
        }
        [Test]
        public void LocationsViewModel_Add_CallsAddOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var itemToAdd = new Location
                {
                    Id = Guid.Empty,
                    PoDId = Guid.NewGuid(),
                    Name = "test"
                };
                mock.Mock<IRepositoryBase<ILocation>>().Setup(x => x.Add(itemToAdd));
                var sut = mock.Create<LocationsViewModel>();

                sut.SelectedItem = itemToAdd;
                sut.SaveCurrentLocation();

                mock.Mock<IRepositoryBase<ILocation>>().Verify(x => x.Add(itemToAdd), Times.Exactly(1));
            }
        }
        [Test]
        public void LocationsViewModel_Update_CallsUpdateOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                var itemToUpdate = new Location
                {
                    Id = id,
                    PoDId = Guid.NewGuid(),
                    Name = "asdf"
                };
                mock.Mock<IRepositoryBase<ILocation>>().Setup(x => x.Update(itemToUpdate));
                var sut = mock.Create<LocationsViewModel>();

                sut.SelectedItem = itemToUpdate;
                sut.SaveCurrentLocation();

                mock.Mock<IRepositoryBase<ILocation>>().Verify(x => x.Update(itemToUpdate), Times.Exactly(1));
            }
        }
    }
}
