using System.Windows.Controls;
using System.Windows.Input;

namespace WhatProject
{
    class MainViewModel : InitializeViewModel
    {

        protected Page allAccountsPage = new Pages.AllAccountsPage();
        protected Page studentsPage = new Pages.StudentsPage();
        protected Page secretariesPage = new Pages.SecretariesPage();
        protected Page mentorsPage = new Pages.MentorsPage();
        protected Page adminsPage = new Pages.AdminsPage();
        protected Page groupsPage = new Pages.GroupsPage();
        protected Page coursesPage = new Pages.CoursesPage();
        protected Page schedulePage = new Pages.SchedulePage();
        protected Page lessonsPage = new Pages.LessonsPage();
        protected Page settingsPage = new Pages.SettingsPage();

        public MainViewModel()
        {
            CurrentPage = allAccountsPage;

            showAllAccountsCommand = new Command(ShowAllAccounts);
            showStudentsCommand = new Command(ShowStudents);
            showSecretariesCommand = new Command(ShowSecretaries);
            showMentorsCommand = new Command(ShowMentors);
            showAdminsCommand = new Command(ShowAdmins);
            showGroupsCommand = new Command(ShowGroups);
            showCoursesCommand = new Command(ShowCourses);
            showScheduleCommand = new Command(ShowSchedule);
            showLessonsCommand = new Command(ShowLessons);
            showSettingsCommand = new Command(ShowSettings);
        }

        #region ---=== Commands ===---
        private ICommand showAllAccountsCommand;
        public ICommand ShowAllAccountsCommand { get => showAllAccountsCommand; }

        private void ShowAllAccounts()
        {
            CurrentPage = allAccountsPage;
        }

        private ICommand showStudentsCommand;
        public ICommand ShowStudentsCommand { get => showStudentsCommand; }

        private void ShowStudents()
        {
            
            CurrentPage = studentsPage;
        }

        private ICommand showSecretariesCommand;
        public ICommand ShowSecretariesCommand { get => showSecretariesCommand; }

        private void ShowSecretaries()
        {

            CurrentPage = secretariesPage;
        }

        private ICommand showMentorsCommand;
        public ICommand ShowMentorsCommand { get => showMentorsCommand; }

        private void ShowMentors()
        {

            CurrentPage = mentorsPage;
        }

        private ICommand showAdminsCommand;
        public ICommand ShowAdminsCommand { get => showAdminsCommand; }

        private void ShowAdmins()
        {

            CurrentPage = adminsPage;
        }

        private ICommand showGroupsCommand;
        public ICommand ShowGroupsCommand { get => showGroupsCommand; }

        private void ShowGroups()
        {

            CurrentPage = groupsPage;
        }

        private ICommand showCoursesCommand;
        public ICommand ShowCoursesCommand { get => showCoursesCommand; }

        private void ShowCourses()
        {

            CurrentPage = coursesPage;
        }

        private ICommand showScheduleCommand;
        public ICommand ShowScheduleCommand { get => showScheduleCommand; }

        private void ShowSchedule()
        {

            CurrentPage = schedulePage;
        }

        private ICommand showLessonsCommand;
        public ICommand ShowLessonsCommand { get => showLessonsCommand; }

        private void ShowLessons()
        {

            CurrentPage = lessonsPage;
        }

        private ICommand showSettingsCommand;
        public ICommand ShowSettingsCommand { get => showSettingsCommand; }

        private void ShowSettings()
        {

            CurrentPage = settingsPage;
        }
        #endregion
    }
}
