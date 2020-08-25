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
    internal class LogEntriesTests
    {
        [Test]
        public void LogEntriesViewModel_LoadUnfiltered_CallsGetAllOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryBase<ILogEntry>>().Setup(x => x.GetAll()).Returns(() =>
                {
                    var list = new List<ILogEntry>();
                    list.Add(new LogEntry{Id = Guid.NewGuid(), Message = "adsf", Hostname = "asdf", Location = "asdf", PoD = "sdf", Severity = 1, Timestamp = DateTime.Now});
                    return list.AsQueryable();
                });
                var sut = mock.Create<LogEntriesViewModel>();

                sut.LoadUnconfirmedLogEntries();

                mock.Mock<IRepositoryBase<ILogEntry>>().Verify(x => x.GetAll(), Times.Exactly(1));
            }
        }
        [Test]
        public void LogEntriesViewModel_LoadFiltered_CallsGetAllWithFilterOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IRepositoryBase<ILogEntry>>().Setup(x => x.GetAll()).Returns(() =>
                {
                    var list = new List<ILogEntry>();
                    list.Add(new LogEntry { Id = Guid.NewGuid(), Message = "adsf", Hostname = "asdf", Location = "asdf", PoD = "sdf", Severity = 1, Timestamp = DateTime.Now });
                    return list.AsQueryable();
                });
                var sut = mock.Create<LogEntriesViewModel>();

                sut.WhereCriteria = "sasdf";
                sut.SearchItems();

                mock.Mock<IRepositoryBase<ILogEntry>>().Verify(x => x.GetAll(), Times.Exactly(1));
            }
        }
        [Test]
        public void LogEntriesViewModel_LoadSingle_CallsGetSingleOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                mock.Mock<IRepositoryBase<ILogEntry>>().Setup(x => x.GetSingle(id)).Returns(() =>
                {
                    var list = new List<ILogEntry>
                    {
                        new LogEntry
                        {
                            Id = id,
                            Message = "adsf",
                            Hostname = "asdf",
                            Location = "asdf",
                            PoD = "sdf",
                            Severity = 1,
                            Timestamp = DateTime.Now
                        }
                    };
                    return list.First();
                });
                var sut = mock.Create<LogEntriesViewModel>();

                sut.GetSingleEntry(id);
                
                mock.Mock<IRepositoryBase<ILogEntry>>().Verify(x => x.GetSingle(id), Times.Exactly(1));
            }
        }
        [Test]
        public void LogEntriesViewModel_Count_CallsCountOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                mock.Mock<IRepositoryBase<ILogEntry>>().Setup(x => x.Count()).Returns(() =>
                {
                    var list = new List<ILogEntry>
                    {
                        new LogEntry
                        {
                            Id = id,
                            Message = "adsf",
                            Hostname = "asdf",
                            Location = "asdf",
                            PoD = "sdf",
                            Severity = 1,
                            Timestamp = DateTime.Now
                        }
                    };
                    return list.Count;
                });
                var sut = mock.Create<LogEntriesViewModel>();

                sut.Strategy = "asdf";
                sut.CountItems();

                mock.Mock<IRepositoryBase<ILogEntry>>().Verify(x => x.Count(), Times.Exactly(1));
            }
        }
        [Test]
        public void AddLogEntryViewModel_Add_CallsAddOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var itemToAdd = new LogEntry
                {
                    Message = "adsf",
                    Hostname = "asdf",
                    PoD = "sdf",
                    Severity = 1,
                };
                mock.Mock<IRepositoryBase<ILogEntry>>().Setup(x => x.Add(itemToAdd));
                var sut = mock.Create<AddLogEntryViewModel>();

                sut.Message = itemToAdd.Message;
                sut.PoD = itemToAdd.PoD;
                sut.Hostname = itemToAdd.Hostname;
                sut.Severity = itemToAdd.Severity;
                sut.CreateNewItem();

                mock.Mock<IRepositoryBase<ILogEntry>>().Verify(x => x.Add(itemToAdd), Times.Exactly(1));
            }
        }
        [Test]
        public void LogEntriesViewModel_Update_CallsUpdateOnce()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var id = Guid.NewGuid();
                var itemToUpdate = new LogEntry
                {
                    Id = id,
                    Message = "adsf",
                    Hostname = "asdf",
                    Location = "asdf",
                    PoD = "sdf",
                    Severity = 1,
                    Timestamp = DateTime.Now
                };
                mock.Mock<IRepositoryBase<ILogEntry>>().Setup(x => x.Update(itemToUpdate));
                var sut = mock.Create<LogEntriesViewModel>();

                sut.SelectedItem = itemToUpdate;
                sut.ConfirmLogEntry();

                mock.Mock<IRepositoryBase<ILogEntry>>().Verify(x => x.Update(itemToUpdate), Times.Exactly(1));
            }
        }
    }
}
