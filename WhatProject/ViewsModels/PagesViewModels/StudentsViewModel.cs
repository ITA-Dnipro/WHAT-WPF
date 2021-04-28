using System.Windows.Input;
using WhatProject.Views;

namespace WhatProject
{
    class StudentsViewModel : InitializeViewModel
    {
        public StudentsViewModel()
        {
            filter.filter(ItemsView, 4);

            addNewStudentCommand = new Command(AddNewStudent);
        }

        private ICommand addNewStudentCommand;
        public ICommand AddNewStudentCommand { get => addNewStudentCommand; }
        private void AddNewStudent()
        {
            AddUserWindow window = new AddUserWindow();
            window.Show();
        }
    }
}
