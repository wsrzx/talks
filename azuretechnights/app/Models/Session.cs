using System;
using MvvmHelpers;

namespace AzureTechNights.Models
{
    public class Session : ObservableObject
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Day { get; set; }
        public string StartAt { get; set; }
        public string SpeakerName { get; set; }
        public string SpeakerTitle { get; set; }
        public string SpeakerAvatar { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }
    }
}
