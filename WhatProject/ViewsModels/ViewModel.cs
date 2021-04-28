using System;
using System.ComponentModel;

namespace WhatProject
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event EventHandler ClosingRequest;

        protected void OnClosingRequest()
        {
            if (ClosingRequest != null)
            {
                ClosingRequest(this, EventArgs.Empty);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
