using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WhatProject.ViewsModels
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

        private string role;
        public string Role 
        {
            get => role; 
            set { role = value; }
        }

        public AddUserViewModel()
        {
            addNewAccountCommand = new Command(AddNewAccount);
        }

        private ICommand addNewAccountCommand;
        public ICommand AddNewAccountCommand { get => addNewAccountCommand; }

        private void AddNewAccount()
        {
            GridItems.Add(new AccountConfiguration(Email, FirstName, LastName, Password, int.Parse(Role) + 1));
        }
    }
}
