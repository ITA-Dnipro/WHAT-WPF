using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class StudentsPage : Page
    {
        public StudentsPage()
        {
            InitializeComponent();

            DataContext = new StudentsViewModel();
        }
    }
}
