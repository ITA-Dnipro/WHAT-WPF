namespace WhatProject
{
    class SecretariesViewModel : InitializeViewModel
    {
        public SecretariesViewModel()
        {
            filter.filter(ItemsView, 2);
        }
    }
}
