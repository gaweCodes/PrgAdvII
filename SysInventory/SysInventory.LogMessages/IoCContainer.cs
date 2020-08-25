using Autofac;
using SysInventory.LogMessages.DataAccess;
using SysInventory.LogMessages.DataAccess.Ef;
using SysInventory.LogMessages.Models;
using SysInventory.LogMessages.ViewModels;

namespace SysInventory.LogMessages
{
    public class IoCContainer
    {
        public IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LogEntriesViewModel>().AsSelf();
            builder.RegisterType<AddLogEntryViewModel>().AsSelf();
            builder.RegisterType<LocationsViewModel>().AsSelf();
            builder.RegisterType<CustomersViewModel>().AsSelf();
            builder.RegisterType<CustomerRepository>().As<IRepositoryBase<Customer>>();
            builder.RegisterType<WindowRepository>().As<IRepositoryBase<WindowFunction>>();
            builder.RegisterType<AddressTypeRepository>().As<IRepositoryBase<AddressType>>();
            builder.RegisterType<AddressRepository>().As<IRepositoryBase<Address>>();
            builder.RegisterType<WindowFunctionViewModel>().AsSelf();
            builder.RegisterType<DataAccess.AdoNet.LogRepository>().As<IRepositoryBase<ILogEntry>>();
            builder.RegisterType<DataAccess.AdoNet.LogRepository>().Named<IRepositoryBase<ILogEntry>>("LogAdoNet");
            builder.RegisterType<DataAccess.LINQ.LogRepository>().Named<IRepositoryBase<ILogEntry>>("LogLINQ");
            builder.RegisterType<LogRepository>().Named<IRepositoryBase<ILogEntry>>("LogEf");
            builder.RegisterType<DataAccess.AdoNet.LocationRepository>().As<IRepositoryBase<ILocation>>();
            builder.RegisterType<DataAccess.AdoNet.LocationRepository>().Named<IRepositoryBase<ILocation>>("LocationAdoNet");
            builder.RegisterType<DataAccess.LINQ.LocationRepository>().Named<IRepositoryBase<ILocation>>("LocationLINQ");
            builder.RegisterType<LocationRepository>().Named<IRepositoryBase<ILocation>>("LocationEf");
            return builder.Build();
        }
    }
}
