using System.ComponentModel;

namespace WhatProject
{
    interface IUserFilter
    {
        void filter(ICollectionView ItemsView, int role);
    }
}
