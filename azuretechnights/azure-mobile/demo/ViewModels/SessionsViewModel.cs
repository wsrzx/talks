using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AzureTechNights.Extensions;
using AzureTechNights.Models;
using AzureTechNights.Services;
using MvvmHelpers;
using Xamarin.Forms;

namespace AzureTechNights.ViewModels
{
    public class SessionsViewModel : BaseViewModel
    {
        public Command RefreshSessionCommand { get; set; }
        public ObservableCollection<Session> Sessions { get; } = new ObservableCollection<Session>();

        public SessionsViewModel()
        {
            Title = "Azure Tech Nights";
            RefreshSessionCommand = new Command(async () => await ExecuteRefreshSessionCommand(), () => !IsBusy);
        }

        private async Task ExecuteRefreshSessionCommand()
        {
            if (IsBusy)
                return;

            Exception error = null;

            try
            {
                IsBusy = true;

                var service = DependencyService.Get<AzureService>();
                var itens = await service.GetSessions();

                Sessions.Clear();
                Sessions.AddRange(itens);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }

            if (error != null) // credo
                await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
        }
    }
}
