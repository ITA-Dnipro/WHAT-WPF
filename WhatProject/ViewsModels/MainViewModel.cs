using System.Windows.Controls;
using System.Windows.Input;

namespace WhatProject
{
    class MainViewModel : InitializeViewModel
    {
        private Page allAccountsPage = new Pages.AllAccountsPage();
        private Page studentsPage = new Pages.StudentsPage();
        private Page secretariesPage = new Pages.SecretariesPage();
        private Page mentorsPage = new Pages.MentorsPage();
        private Page adminsPage = new Pages.AdminsPage();
        private Page groupsPage = new Pages.GroupsPage();
        private Page coursesPage = new Pages.CoursesPage();
        private Page schedulePage = new Pages.SchedulePage();
        private Page lessonsPage = new Pages.LessonsPage();
        private Page settingsPage = new Pages.SettingsPage();

        private Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set { currentPage = value; OnPropertyChanged(nameof(CurrentPage)); }
        }

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
