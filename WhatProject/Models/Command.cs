using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WhatProject
{
    class Command : ICommand
    {
        private readonly Func<object, bool> canExecute;
        private readonly Action execute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public Command(Action execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => canExecute == null ? true : canExecute.Invoke(parameter);
        public void Execute(object parameter)
        {
            execute.Invoke();
        }
    }
}
