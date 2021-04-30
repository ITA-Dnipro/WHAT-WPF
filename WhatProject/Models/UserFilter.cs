using System.ComponentModel;
using WhatProject.Interfaces;

namespace WhatProject
{
    class UserFilter : IUserFilter
    {
        public void filter(ICollectionView ItemsView, int role)
        {
            ItemsView.Filter = w => ((AccountConfiguration)w).Role.Equals(role);
        }
    }
}
