using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class AllAccountsPage : Page
    {
        public AllAccountsPage()
        {
            InitializeComponent();
            DataContext = new AllAccountsViewModel();
        }
    }
}
