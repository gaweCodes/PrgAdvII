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
    }
}
