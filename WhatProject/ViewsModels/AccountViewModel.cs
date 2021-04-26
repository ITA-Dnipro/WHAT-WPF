using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

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

            showAllAccountsCommand = new Command(showAllAccounts);
            showStudentsCommand = new Command(showStudents);
            showAdminsCommand = new Command(showAdmins);
            showSecretariesCommand = new Command(showSecretaries);
            showMentorsCommand = new Command(showMentors);
        }

        #region ---===  Commands  ===---

        private ICommand showAllAccountsCommand;
        public ICommand ShowAllAccountsCommand { get => showAllAccountsCommand; }

        private void showAllAccounts()
        {
            buttonsMenu.ShowAllAccounts(ItemsView);
        }

        private ICommand showStudentsCommand;
        public ICommand ShowStudentsCommand { get => showStudentsCommand; }

        private void showStudents()
        {
            buttonsMenu.ShowStudens(ItemsView, filter);
        }

        private ICommand showAdminsCommand;
        public ICommand ShowAdminsCommand { get => showAdminsCommand; }
        private void showAdmins()
        {
            buttonsMenu.ShowAdmins(ItemsView, filter);
        }

        private ICommand showSecretariesCommand;
        public ICommand ShowSecretariesCommand { get => showSecretariesCommand; }
        private void showSecretaries()
        {
            buttonsMenu.ShowSecretaries(ItemsView, filter);
        }

        private ICommand showMentorsCommand;
        public ICommand ShowMentorsCommand { get => showMentorsCommand; }
        private void showMentors()
        {
            buttonsMenu.ShowMentors(ItemsView, filter);
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
