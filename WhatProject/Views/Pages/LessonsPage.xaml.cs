using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class LessonsPage : Page
    {
        public LessonsPage()
        {
            InitializeComponent();

            DataContext = new LessonsViewModel();
        }
    }
}
