using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WhatProject.Views;

namespace WhatProject
{
    class AccountViewModel : INotifyPropertyChanged
    {
        IUserFilter filter = new UserFilter();
        IButtonsMenu buttonsMenu = new ButtonsMenu();

        private ObservableCollection<AccountConfiguration> gridItems;
        public ObservableCollection<AccountConfiguration> GridItems
        {
            get => gridItems;
            set
            {
                gridItems = value;
                OnPropertyChanged(nameof(GridItems));
            }
        }

        ICollectionView ItemsView { get => CollectionViewSource.GetDefaultView(GridItems); } 

        public AccountViewModel()
        {
            GridItems = new ObservableCollection<AccountConfiguration>();
            GridItems.Add(new AccountConfiguration("tesla@gmail.com", "Nikola", "Tesla", "1111", 1));
            GridItems.Add(new AccountConfiguration("musk@gmail.com", "Elon", "Musk", "1111", 2));
            GridItems.Add(new AccountConfiguration("bezos@gmail.com","Jeff", "Bezos", "1111", 3));
            GridItems.Add(new AccountConfiguration("mcgregor@gmail.com", "Conor", "McGregor", "1111", 4));
            GridItems.Add(new AccountConfiguration("trump@gmail.com", "Donald", "Trump", "1111", 4));
            GridItems.Add(new AccountConfiguration("rivia@gmail.com", "Geralt", "of Rivia", "1111", 4));

            showAllAccountsCommand = new Command(ShowAllAccounts);
            showStudentsCommand = new Command(ShowStudents);
            showAdminsCommand = new Command(ShowAdmins);
            showSecretariesCommand = new Command(ShowSecretaries);
            showMentorsCommand = new Command(ShowMentors);
            openAddUserWindowCommand = new Command(OpenAddUserWindow);
        }

        #region ---===  Commands  ===---

        private ICommand showAllAccountsCommand;
        public ICommand ShowAllAccountsCommand { get => showAllAccountsCommand; }

        private void ShowAllAccounts()
        {
            buttonsMenu.ShowAllAccounts(ItemsView);
        }

        private ICommand showStudentsCommand;
        public ICommand ShowStudentsCommand { get => showStudentsCommand; }

        private void ShowStudents()
        {
            buttonsMenu.ShowStudens(ItemsView, filter);
        }

        private ICommand showAdminsCommand;
        public ICommand ShowAdminsCommand { get => showAdminsCommand; }
        private void ShowAdmins()
        {
            buttonsMenu.ShowAdmins(ItemsView, filter);
        }

        private ICommand showSecretariesCommand;
        public ICommand ShowSecretariesCommand { get => showSecretariesCommand; }
        private void ShowSecretaries()
        {
            buttonsMenu.ShowSecretaries(ItemsView, filter);
        }

        private ICommand showMentorsCommand;
        public ICommand ShowMentorsCommand { get => showMentorsCommand; }
        private void ShowMentors()
        {
            buttonsMenu.ShowMentors(ItemsView, filter);
        }

        private ICommand openAddUserWindowCommand;
        public ICommand OpenAddUserWindowCommand { get => openAddUserWindowCommand; }

        void OpenAddUserWindow()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            addUserWindow.Show();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
