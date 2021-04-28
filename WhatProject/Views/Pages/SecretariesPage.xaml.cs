using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class SecretariesPage : Page
    {
        public SecretariesPage()
        {
            InitializeComponent();

            DataContext = new SecretariesViewModel();
        }
    }
}
