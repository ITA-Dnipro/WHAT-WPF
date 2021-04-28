namespace WhatProject
{
    class AdminsViewModel : InitializeViewModel
    {
        public AdminsViewModel()
        {
            filter.filter(ItemsView, 1);
        }
    }
}
