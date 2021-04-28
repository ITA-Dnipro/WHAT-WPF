using System.Windows.Controls;

namespace WhatProject.Pages
{
    public partial class MentorsPage : Page
    {
        public MentorsPage()
        {
            InitializeComponent();

            DataContext = new MentorsViewModel();
        }
    }
}
