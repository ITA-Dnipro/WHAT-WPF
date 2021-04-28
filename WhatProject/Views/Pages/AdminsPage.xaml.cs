using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class AdminsPage : Page
    {
        public AdminsPage()
        {
            InitializeComponent();

            DataContext = new AdminsViewModel();
        }
    }
}
