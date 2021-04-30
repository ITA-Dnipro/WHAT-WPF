using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

using WhatProject.Enums;

namespace WhatProject
{

    public partial class MainWindow : Window
    {
        MainViewModel mainViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = mainViewModel;

            MainFrame.DataContext = DataContext;
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\AllUsersPage.xaml", UriKind.Relative));

            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonAllAccounts_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\AllUsersPage.xaml", UriKind.Relative));
            mainViewModel.ItemsView.Filter = null;
        }

        private void ButtonStudents_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\StudentsPage.xaml", UriKind.Relative));
            mainViewModel.ItemsView.Filter = w => ((AccountConfiguration)w).Role.Equals((int)Roles.Student);
        }

        private void ButtonMentors_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\MentorsPage.xaml", UriKind.Relative));
            mainViewModel.ItemsView.Filter = w => ((AccountConfiguration)w).Role.Equals((int)Roles.Mentor);
        }

        private void ButtonSecreteries_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\SecretariesPage.xaml", UriKind.Relative));
            mainViewModel.ItemsView.Filter = w => ((AccountConfiguration)w).Role.Equals((int)Roles.Secretary);
        }

        private void ButtonAdmins_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\AdminsPage.xaml", UriKind.Relative));
            mainViewModel.ItemsView.Filter = w => ((AccountConfiguration)w).Role.Equals((int)Roles.Admin);
        }

        private void ButtonGroups_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\GroupsPage.xaml", UriKind.Relative));
        }

        private void ButtonHomeWork_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\CoursesPage.xaml", UriKind.Relative));
        }

        private void ButtonSchedule_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\SchedulePage.xaml", UriKind.Relative));
        }

        private void ButtonLessons_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\LessonsPage.xaml", UriKind.Relative));
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Views\\Pages\\SettingsPage.xaml", UriKind.Relative));
        }

        private void ButtonOpenMenu_Click(Object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void ButtonCloseMenu_Click(Object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void WindowHeaderGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ((FrameworkElement)e.Content).DataContext = this.DataContext;
        }
    }
}
