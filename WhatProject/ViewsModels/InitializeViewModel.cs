using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using WhatProject.Interfaces;

namespace WhatProject
{
    class InitializeViewModel : ViewModel
    {
        protected IUserFilter filter = new UserFilter();

        private ObservableCollection<AccountConfiguration> gridItems;
        public ObservableCollection<AccountConfiguration> GridItems
        {
            get => gridItems;
            set
            {
                gridItems = value;
                OnPropertyChanged(nameof(ItemsView));
            }
        }

        public ICollectionView ItemsView {get => CollectionViewSource.GetDefaultView(GridItems);}

        public InitializeViewModel()
        {
            GridItems = new ObservableCollection<AccountConfiguration>();
            GridItems.Add(new AccountConfiguration("tesla@gmail.com", "Nikola", "Tesla", "1111", 1));
            GridItems.Add(new AccountConfiguration("musk@gmail.com", "Elon", "Musk", "1111", 2));
            GridItems.Add(new AccountConfiguration("bezos@gmail.com", "Jeff", "Bezos", "1111", 3));
            GridItems.Add(new AccountConfiguration("mcgregor@gmail.com", "Conor", "McGregor", "1111", 4));
            GridItems.Add(new AccountConfiguration("trump@gmail.com", "Donald", "Trump", "1111", 4));
            GridItems.Add(new AccountConfiguration("rivia@gmail.com", "Geralt", "of Rivia", "1111", 4));
        }
    }
}
