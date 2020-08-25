using System.Windows;
using Autofac;
using SysInventory.LogMessages.ViewModels;

namespace SysInventory.LogMessages.Views
{
    public partial class Locations : Window
    {
        public Locations(IContainer container)
        {
            InitializeComponent();
            var vm = container.Resolve<LocationsViewModel>();
            vm.Container = container;
            DataContext = vm;
        }
    }
}
