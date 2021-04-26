using System.ComponentModel;

namespace WhatProject
{
    interface IButtonsMenu
    {
        void ShowAllAccounts(ICollectionView itemsView);
        void ShowStudens(ICollectionView itemsView, IUserFilter filter);
        void ShowMentors(ICollectionView itemsView, IUserFilter filter);
        void ShowSecretaries(ICollectionView itemsView, IUserFilter filter);
        void ShowAdmins(ICollectionView itemsView, IUserFilter filter);

    }
}
