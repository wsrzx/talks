using System;
using Xamarin.Forms;

namespace ToysQuest.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }
    }
}
