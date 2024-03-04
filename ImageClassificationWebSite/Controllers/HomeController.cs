using ClusteringWebSite.Models;
using ImageClassificationWebSite;
using ImageClassificationWebSite.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Emit;

namespace ClusteringWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ClassifyImage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ClassifyImage(IFormFile ImageFile)
        {
            byte[] ImageBytes = null;
            using (BinaryReader br = new BinaryReader(ImageFile.OpenReadStream()))
            {
                ImageBytes = br.ReadBytes((int)ImageFile.Length);
                MLModel.ModelInput sampleData = new MLModel.ModelInput()
                {
                    ImageSource = ImageBytes,
                };
                var sortedScoresWithLabel = MLModel.PredictAllLabels(sampleData);
                return Json(new {
                    Label= sortedScoresWithLabel.First().Key,
                    Score= sortedScoresWithLabel.First().Value,
                });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
