using System;
using System.Windows.Input;

namespace SysInventory.LogMessages
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };
        private readonly Action _targetExecuteMethod;
        private readonly Func<bool> _targetCanExecuteMethod;
        public RelayCommand(Action executeMethod) => _targetExecuteMethod = executeMethod;
        public RelayCommand(Action executeMethod, Func<bool> canexecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canexecuteMethod;
        }
        public void RaiseCanExecuteChanged() => CanExecuteChanged(this, EventArgs.Empty);
        bool ICommand.CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod != null)
                return _targetCanExecuteMethod();
            return _targetExecuteMethod != null;
        }
        void ICommand.Execute(object parameter) => _targetExecuteMethod?.Invoke();
    }
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _targetExecuteMethod;
        private readonly Func<T, bool> _targetCanExecuteMethod;
        public RelayCommand(Action<T> executeMethod) => _targetExecuteMethod = executeMethod;
        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _targetExecuteMethod = executeMethod;
            _targetCanExecuteMethod = canExecuteMethod;
        }
        public void RaiseCanExecuteChanged() => CanExecuteChanged(this, EventArgs.Empty);
        bool ICommand.CanExecute(object parameter)
        {
            if (_targetCanExecuteMethod == null) return _targetExecuteMethod != null;
            var tparm = (T)parameter;
            return _targetCanExecuteMethod(tparm);
        }
        public event EventHandler CanExecuteChanged = delegate { };
        void ICommand.Execute(object parameter) => _targetExecuteMethod?.Invoke((T)parameter);
    }
}
