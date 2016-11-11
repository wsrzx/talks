using System;
using PropertyChanged;
using Xamarin.Forms;

namespace PeopleJornal
{
    [ImplementPropertyChanged]
    public class ViewModelBase
    {
        public string Title { get; set; }
        public string Busy { get; set; }

        public static void Init()
        {
            DependencyService.Register<IPersonService, PersonService>();
        }
    }
}