using System;
using System.ComponentModel;

namespace WhatProject
{
    public class AccountConfiguration : INotifyPropertyChanged
    {
        private string toTable = "account";
        private string email;
        private int id;
        private string firstName;
        private string lastName;
        private bool isActive;
        private string password;
        private int role;
        private string salt;
        private string forgotPasswordToken;
        private DateTime forgotTokenGenDate;
        private string avatarID;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public int Id { get => id; }
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public int Role
        {
            get => role;
            set
            {
                role = value;
                OnPropertyChanged(nameof(Role));
            }
        }
        public string Salt
        {
            get => salt;
            set
            {
                salt = value; OnPropertyChanged(nameof(Salt));
            }
        }
        public string ForgotPasswordToken
        {
            get => forgotPasswordToken;
            set
            {
                forgotPasswordToken = value;
                OnPropertyChanged(nameof(ForgotPasswordToken));
            }
        }
        public DateTime ForgotTokenGenDate
        {
            get => forgotTokenGenDate;
            set
            {
                forgotTokenGenDate = value;
                OnPropertyChanged(nameof(ForgotTokenGenDate));
            }
        }
        public string AvatarID
        {
            get => avatarID;
            set
            {
                avatarID = value;
                OnPropertyChanged(nameof(AvatarID));
            }
        }

        public AccountConfiguration(string email, string firstName, string lastName, string password, int role)
        {
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
            this.role = role;
            isActive = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
