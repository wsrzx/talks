using System;

using Xamarin.Forms;

namespace CodePooper.Pages
{
    public class Poop : ContentPage
    {
        public Poop()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

