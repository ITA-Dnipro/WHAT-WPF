using System.Windows.Data;
using System.Windows.Input;

namespace WhatProject
{
    class AddUserViewModel : InitializeViewModel
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
            GridItems.Add(new AccountConfiguration(Email, FirstName, LastName, Password, 4));
            OnClosingRequest();
        }
    }
}
