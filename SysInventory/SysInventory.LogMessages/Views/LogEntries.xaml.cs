using System.Windows.Controls;
using Autofac;
using SysInventory.LogMessages.ViewModels;

namespace SysInventory.LogMessages.Views
{
        public partial class LogEntries : UserControl
        {
            public LogEntries()
            {
                var container = new IoCContainer().BuildContainer();
                InitializeComponent();

                var vm = container.Resolve<LogEntriesViewModel>();
                vm.Container = container;
                DataContext = vm;
            }
        }
}
