using System.Windows;
using System.Windows.Controls;

namespace WhatProject.Views.Pages
{
    public partial class SecretariesPage : Page
    {
        public SecretariesPage()
        {
            InitializeComponent();
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            Window addUsers = new AddUserWindow();
            addUsers.Owner = MainWindow.GetWindow(this);
            addUsers.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            addUsers.DataContext = DataContext;
            addUsers.Show();
        }
    }
}
