using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using WhatProject.Models;
using System.Linq;

namespace WhatProject
{
    class InitializeViewModel : ViewModel
    {
        private Page currentPage;
        private GroupContext _group;

        public Page CurrentPage
        {
            get => currentPage;
            set { currentPage = value; OnPropertyChanged(nameof(CurrentPage)); }
        }

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

        public ICollectionView GroupsView => CollectionViewSource.GetDefaultView(_group.Groups.ToList());

        public ICollectionView ItemsView {get => CollectionViewSource.GetDefaultView(GridItems);}

        public InitializeViewModel()
        {
            _group = new GroupContext();

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
