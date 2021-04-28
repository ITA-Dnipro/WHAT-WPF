using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class CoursesPage : Page
    {
        public CoursesPage()
        {
            InitializeComponent();

            DataContext = new CoursesViewModel();
        }
    }
}
