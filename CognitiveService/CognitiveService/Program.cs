using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using CognitiveService;

namespace CSHttpClientSample
{
    static class Program
    {
        static void Main()
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
        }

        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "01f71a2463d24874a70ebd6442802772");

            // Request parameters
            queryString["visualFeatures"] = "Categories";
            queryString["details"] = "Celebrities";
            queryString["language"] = "en";
            var uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?" + queryString;


            Console.WriteLine(DateTime.Now);
            await UsingImageurl(client, uri);
            Console.WriteLine(DateTime.Now);

            //await UsingByteArray(client, uri);
        }

        private static async System.Threading.Tasks.Task UsingImageurl(HttpClient client, string uri)
        {
            var url = new
            {
                url = "http://static.dnaindia.com/sites/default/files/styles/half/public/2016/02/27/430800-celebrities.jpg?itok=kg4hXqGH"
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(url), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();

            var imageDescription = JsonConvert.DeserializeObject<ImageDetails>(result);

            foreach (var item in imageDescription.categories)
            {

                if (item != null && item.detail != null)
                {
                    Console.WriteLine("Celebrities");
                    foreach (var celebrity in item.detail.celebrities)
                    {
                        Console.WriteLine(celebrity.name + " - Accuracy " + celebrity.confidence);
                    }
                }
            }
        }


        private static async System.Threading.Tasks.Task UsingByteArray(HttpClient client, string uri)
        {
            byte[] byteData = File.ReadAllBytes(@"D:\Projects\C-Sharp6 Practice\CognitiveService\CognitiveService\Images\narendra-modi-speech.jpg");

            HttpContent content = new ByteArrayContent(byteData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();

            var imageDescription = JsonConvert.DeserializeObject<ImageDetails>(result);

            foreach (var item in imageDescription.categories)
            {
                Console.WriteLine("Celebrities");
                foreach (var celebrity in item.detail.celebrities)
                {
                    Console.WriteLine(celebrity.name + " - Accuracy " + celebrity.confidence);
                }
            }
        }
    }
}

