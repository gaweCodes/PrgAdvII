using System.Windows;
using Autofac;
using SysInventory.LogMessages.ViewModels;

namespace SysInventory.LogMessages.Views
{
    public partial class CustomersView : Window
    {
        public CustomersView(IContainer container)
        {
            InitializeComponent();
            DataContext = container.Resolve<CustomersViewModel>();
        }
    }
}
