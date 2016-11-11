using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeopleJornal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDetailPage : ContentPage
    {
        DetailViewModel vm;
        DetailViewModel ViewModel => vm ?? (vm = BindingContext as DetailViewModel);

        protected string PersonId;

        public AddDetailPage(string personId)
        {
            PersonId = personId;
            InitializeComponent();

            BindingContext = vm = new DetailViewModel();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
                ViewModel.Init(PersonId);
        }
    }
}
