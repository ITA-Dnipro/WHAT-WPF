using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WhatProject
{
    public class AddUserViewModel : ViewModel, INotifyPropertyChanged
    {
        private string firstName;
        public string FirstName { get => firstName; set { firstName = value; } }

        private string lastName;
        public string LastName { get => lastName; set { lastName = value; } }

        private string email;
        public string Email { get => email; set { email = value; } }

        private string password;
        public string Password { get => password; set { password = value; } }


        public AddUserViewModel()
        {
            cancelButtonCommand = new Command(CancelButton);
            addNewAccountCommand = new Command(AddNewAccount);
        }

        private ICommand cancelButtonCommand;
        public ICommand CancelButtonCommand { get => cancelButtonCommand; }
        private void CancelButton()
        {
            OnClosingRequest();
        }

        private ICommand addNewAccountCommand;
        public ICommand AddNewAccountCommand { get => addNewAccountCommand; }

        private void AddNewAccount()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
