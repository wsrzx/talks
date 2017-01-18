using System;
using System.Collections.Generic;
using AzureTechNights.Models;
using Xamarin.Forms;

namespace AzureTechNights.Views
{
    public partial class SessionBrowserView : ContentPage
    {
        public SessionBrowserView(Session session)
        {
            InitializeComponent();

            Title = session.Title;
            webView.Source = session.Url;
        }
    }
}
