using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class GroupsPage : Page
    {
        public GroupsPage()
        {
            InitializeComponent();

            DataContext = new GroupsViewModel();
        }
    }
}
