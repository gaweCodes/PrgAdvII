using System.Windows;
using Autofac;
using SysInventory.LogMessages.ViewModels;

namespace SysInventory.LogMessages.Views
{
    public partial class AddLogEntryDialog : Window
    {
        public AddLogEntryDialog(IContainer container)
        {
            InitializeComponent();
            var vm = container.Resolve<AddLogEntryViewModel>();
            vm.Container = container;
            DataContext = vm;
        }
    }
}
