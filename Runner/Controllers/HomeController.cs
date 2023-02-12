using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Runner.Helpers;
using Runner.Models;
using Runner.Models.ViewModels;
using Runner.Repository.Interface;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace Runner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _club;
        public HomeController(ILogger<HomeController> logger,IClubRepository club)
        {
            _club=club;
            _logger = logger;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var ipinfo = new IpInfo();

        //    var homeViwModel = new HomeViewModel();
        //    try
        //    {
        //        string url = "https://ipinfo.io?token=";
        //        var info= new WebClient().DownloadString(url);
        //       // info = JsonConvert.DeserializeObject<IpInfo>(info);
        //        RegionInfo myRI1 = new RegionInfo(ipinfo.Country);
        //        ipinfo.Country = myRI1.EnglishName;
        //        homeViwModel.City = ipinfo.City;
        //        homeViwModel.State = ipinfo.Region;
        //        if(homeViwModel.City!=null)
        //        {
        //            homeViwModel.clubs = await _club.GetClubByCity(homeViwModel.City);
        //        }
        //        else
        //        {
        //            homeViwModel.clubs = null;
        //        }
        //        return View(homeViwModel);
        //    }
        //    catch (Exception e)
        //    {
        //        homeViwModel.clubs = null;
        //    }
        //    return View(homeViwModel.clubs);
        //}

        public async Task<IActionResult> Index()
        {
            var result= await _club.GetAll();
            return View(result);
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