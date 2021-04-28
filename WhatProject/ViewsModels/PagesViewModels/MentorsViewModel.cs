namespace WhatProject
{
    class MentorsViewModel : InitializeViewModel
    {
        public MentorsViewModel()
        {
            filter.filter(ItemsView, 3);
        }
    }
}
