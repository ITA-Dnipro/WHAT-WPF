using System.Windows;
using System.Windows.Input;

namespace WhatProject.Views
{
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();

            AddUserViewModel viewModel = new AddUserViewModel();
            DataContext = viewModel;

            viewModel.ClosingRequest += (sender, e) => Close();
        }

        private void AddUserBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
