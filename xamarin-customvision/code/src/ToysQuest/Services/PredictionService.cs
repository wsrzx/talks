using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ToysQuest.Models;

namespace ToysQuest.Services
{
    public class PredictionService : IPredictionService
    {
        readonly string predictionKey = "edfcb0835eda4709aad8f8fb1780a75e";
        readonly string predictionEndPoint = $"https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/3f2f0c17-1232-4771-9ac0-a29f7e698351/detect/iterations/Demo/image";

        static byte[] GetImageAsByteArray(MediaFile photo)
        {
            FileStream fileStream = new FileStream(photo.Path, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        public async Task<PredictionResult> PredictPhoto(MediaFile photo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);

                    HttpResponseMessage response;

                    byte[] byteData = GetImageAsByteArray(photo);

                    var content = new ByteArrayContent(byteData);

                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(predictionEndPoint, content);

                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PredictionResult>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw ex;
            }
        }

    }
}
