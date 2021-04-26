using System.ComponentModel;

namespace WhatProject
{
    class ButtonsMenu : IButtonsMenu
    {
        public void ShowAdmins(ICollectionView itemsView, IUserFilter filter)
        {
            filter.filter(itemsView, 1);
        }

        public void ShowAllAccounts(ICollectionView itemsView)
        {
            itemsView.Filter = null;
        }

        public void ShowMentors(ICollectionView itemsView, IUserFilter filter)
        {
            filter.filter(itemsView, 3);
        }

        public void ShowSecretaries(ICollectionView itemsView, IUserFilter filter)
        {
            filter.filter(itemsView, 2);
        }

        public void ShowStudens(ICollectionView itemsView, IUserFilter filter)
        {
            filter.filter(itemsView, 4);
        }
    }
}
