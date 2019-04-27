using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using ToysQuest.Models;
using ToysQuest.Services;
using Xamarin.Forms;

namespace ToysQuest.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        readonly IPredictionService _predictionService = null;

        double _precision;
        public double Precision
        {
            get { return _precision; }
            set { _precision = value; OnPropertyChanged(); }
        }

        string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }


        string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; OnPropertyChanged(); }
        }

        public ICommand TakePhotoCommand { get; }
        public ICommand PickPhotoCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        public MainViewModel()
        {
            Title = "Toy Quest";

            TakePhotoCommand = new Command(async () => await TakePhoto());
            PickPhotoCommand = new Command(async () => await PickPhoto());
            _predictionService = new PredictionService();
        }

        Task TakePhoto() => GetPhoto(() => CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Medium }));

        Task PickPhoto() => GetPhoto(() => CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Medium }));

        async Task GetPhoto(Func<Task<MediaFile>> getPhotoFunc)
        {
            IsBusy = true;

            Name = ImageSource = string.Empty;

            try
            {
                var photo = await getPhotoFunc();
                if (photo == null)
                    return;

                ImageSource = photo.Path;

                var result = await _predictionService.PredictPhoto(photo);

                FilterPrediction(result);

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occured: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        void FilterPrediction(PredictionResult predictionResult)
        {
            var prediction = predictionResult.Predictions
                                     .FirstOrDefault(p => p.Probability > Precision);

            Name = prediction != null ? $"👀 {prediction.TagName}" : "💩";
        }
    }
}
