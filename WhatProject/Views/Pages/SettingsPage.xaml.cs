using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            DataContext = new SettingsViewModel();
        }
    }
}
