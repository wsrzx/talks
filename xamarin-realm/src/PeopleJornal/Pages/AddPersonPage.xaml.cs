using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeopleJornal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPersonPage : ContentPage
    {
        PersonViewModel vm;
        PersonViewModel ViewModel => vm ?? (vm = BindingContext as PersonViewModel);

        protected string PersonId;

        public AddPersonPage(string personId)
        {
            PersonId = personId;
            InitializeComponent();
            BindingContext = vm = new PersonViewModel();
            InitButtons();
        }

        public AddPersonPage()
        {
            InitializeComponent();
            BindingContext = vm = new PersonViewModel();
            InitButtons();
        }

        void InitButtons()
        {
            if (!string.IsNullOrWhiteSpace(PersonId))
                ToolbarItems.Add(new ToolbarItem("Delete", null, () => ViewModel.DeleteCommand.Execute(null)));

            AddDetailButton.Clicked += (sender, e) => this.Navigation.PushAsync(new AddDetailPage(PersonId));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext != null)
                ViewModel.Init(PersonId);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != null)
                ViewModel.Refresh();
        }
    }
}
