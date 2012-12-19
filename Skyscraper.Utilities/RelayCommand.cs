using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Skyscraper.Utilities
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            this.OnCanExecuteChanged();
        }

        protected void OnCanExecuteChanged()
        {
            EventHandler eventHandler = this.CanExecuteChanged;

            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }
        }
    }
}