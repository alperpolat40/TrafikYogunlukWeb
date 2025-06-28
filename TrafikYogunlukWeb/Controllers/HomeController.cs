using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using TrafikYogunlukWeb.Models;
using TrafikYogunlukWeb.Services;

namespace TrafikYogunlukWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TomTomService _tomTomService;

        public HomeController(ILogger<HomeController> logger, TomTomService tomTomService)
        {
            _logger = logger;
            _tomTomService = tomTomService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Grafik()
        {
            return View();
        }

        public IActionResult Harita(string yil = "2020")
        {
            ViewBag.HaritaDosyasi = $"/haritalar/trafik_yol_{yil}.html";
            return View();
        }

        public IActionResult Isi()
        {
            return View();
        }
        public IActionResult Anasayfa()
        {
            return View();
        }

        public IActionResult Performans()
        {
            var modeller = new List<Model>
            {
                new Model { Ad = "Decision Tree", R2 = 0.91, MAE = 4.79, MSE = 53.42, Aciklama = "Kurallara dayal� yap�s� sayesinde en y�ksek do�rulu�u g�stermi�tir.", ImgDosyaAdi = "decision.png" },
                new Model { Ad = "LightGBM", R2 = 0.75, MAE = 9.72, MSE = 156.24, Aciklama = "B�y�k veriyle h�zl� �al���r ama karma��k yap�larda hatalar artabilir.", ImgDosyaAdi = "lightGBM.png" },
                new Model { Ad = "XGBoost", R2 = 0.83, MAE = 7.75, MSE = 106.24, Aciklama = "Y�ksek performansl� ve dengeli sonu�lar sunar.", ImgDosyaAdi = "XGBoost.png" },
                new Model { Ad = "K-Nearest Neighbors", R2 = 0.33, MAE = 16.25, MSE = 420.12, Aciklama = "B�y�k veri setlerinde d���k ba�ar� g�stermi�tir.", ImgDosyaAdi = "KNN.png" },
                new Model { Ad = "Polynomial Regression", R2 = 0.007, MAE = 21.76, MSE = 618.51, Aciklama = "Veri karma��kl���na kar�� zay�f kalm��t�r.", ImgDosyaAdi = "PolynomialRegression.png" }
            };

            return View(modeller);
        }

        public async Task<IActionResult> Trafik()
        {
            var trafficPoints = await _tomTomService.GetMultipleTrafficFlowsAsync(new List<(double, double)>
            {
                (41.015137, 28.979530),
                (41.078383, 29.044400),
                (41.057083, 28.977960),
                (41.066400, 29.006800),
                (41.100000, 29.050000),


                // Bo�azi�i (15 Temmuz) g�zergah�
                (41.0237, 29.0514), // Altunizade
                (41.0308, 29.0427),
                (41.0383, 29.0365),
                (41.0458, 29.0310),
                (41.0915, 29.0395),
                (41.0938, 29.0237),
                (41.0849, 29.0121),
                (41.0748, 29.0155), // Balmumcu

                // Avrasya T�neli
                (40.9928, 28.9011), // Kazl��e�me
                (40.9923, 28.9132),
                (40.9916, 28.9250),
                (40.9904, 28.9372),
                (40.9890, 28.9501),
                (40.9876, 28.9643),
                (40.9865, 28.9765),
                (40.9854, 28.9902), // G�ztepe

                (40.9908, 29.0543), // Ac�badem
                (40.9927, 29.0645),
                (40.9943, 29.0786),
                (40.9957, 29.0920),
                (40.9968, 29.1071), // Kozyata��


                (41.1037, 28.9831), // Seyrantepe
                (41.1079, 28.9973), // TEM ba�lant�s�
                (41.1108, 29.0136), // FSM Avrupa yakla��m�
                (41.1110, 29.0225), // FSM Avrupa giri�i
                (41.1102, 29.0322), // FSM ortas�
                (41.1078, 29.0420), // FSM Anadolu ��k���
                (41.1056, 29.0508),
                (41.1033, 29.0580), // Kavac�k merkez
                (41.1012, 29.0645), // Kavac�k ��k��� / Kav�a��

                (41.099761, 28.968587),
                (41.101810, 28.994481),
                (41.099518, 29.007077),
                (41.090953, 29.055244),
                (41.091599, 29.086223),
                (41.055045, 29.119402),
                (41.006290, 29.066820),
                (
41.006107, 29.075390),
                (
41.009573, 29.087430),
                (
41.004768, 29.101045),

                (41.2114, 28.9701), // Uskumruk�y
                (41.2103, 28.9958),
                (41.2046, 29.0235),
                (41.1980, 29.0561),
                (41.1892, 29.0862),
                (41.1779, 29.1155), // Riva

                
                //Mahmutbey Gi�eler

                (41.062835, 28.782688),
                (41.061219, 28.807847),
                (41.062157, 28.817525),
                (41.063892, 28.827226),
                (41.063814, 28.835093),




            });

            ViewBag.TrafficPoints = trafficPoints;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
