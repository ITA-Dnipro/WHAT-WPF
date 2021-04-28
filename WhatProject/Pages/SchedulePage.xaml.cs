using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class SchedulePage : Page
    {
        public SchedulePage()
        {
            InitializeComponent();

            DataContext = new ScheduleViewModel();
        }
    }
}
