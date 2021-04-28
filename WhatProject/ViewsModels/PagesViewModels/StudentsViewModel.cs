namespace WhatProject
{
    class StudentsViewModel : InitializeViewModel
    {
        public StudentsViewModel()
        {
            filter.filter(ItemsView, 4);
        }
    }
}
