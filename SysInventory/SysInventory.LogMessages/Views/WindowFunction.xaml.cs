using System.Windows;
using Autofac;
using SysInventory.LogMessages.ViewModels;

namespace SysInventory.LogMessages.Views
{
    /// <summary>
    /// Interaction logic for WindowFunction.xaml
    /// </summary>
    public partial class WindowFunction : Window
    {
        public WindowFunction(IContainer container)
        {
            InitializeComponent();
            DataContext = container.Resolve<WindowFunctionViewModel>();
        }
    }
}
