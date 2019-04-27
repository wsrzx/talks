using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using ToysQuest.Models;

namespace ToysQuest.Services
{
    public interface IPredictionService
    {
        Task<PredictionResult> PredictPhoto(MediaFile photo);
    }
}