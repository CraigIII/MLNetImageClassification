using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ImageClassificationApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        string imageSource = "";

        private async void OnSelectClicked(object sender, EventArgs e)
        {
            var images = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "選取欲分析的檔案",
                FileTypes = FilePickerFileType.Images
            });
            imageSource = images.FullPath.ToString();
            imgSelected.Source = imageSource;
        }

        private async void OnAnalyzeClicked(object sender, EventArgs e)
        {
            string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android
                    ? "https://10.0.2.2:63201" : "https://localhost:63201";
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            using (HttpClient Client = new HttpClient(handler.GetPlatformMessageHandler()))
#else
            using (HttpClient Client = new HttpClient())
#endif
            { 
                string requestUri = $"{BaseAddress}/predict";
                byte[] ImageBytes = null;
                var stream = File.OpenRead(imageSource);
                using BinaryReader br = new BinaryReader(stream);
                ImageBytes = br.ReadBytes((int)stream.Length);
                ModelInput input = new ModelInput
                {
                    Label = "",
                    ImageSource = ImageBytes
                };
                string strContent = JsonSerializer.Serialize(input);
                var content = new StringContent(strContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();
                string Json = await response.Content.ReadAsStringAsync();
                ModelOutput Result = JsonSerializer.Deserialize<ModelOutput>(Json);
                await DisplayAlert("分類結果", $"內容:{Result.predictedLabel}, 信心指數:{Result.score.Max():n2}", "關閉");
            }
        }
    }
}
