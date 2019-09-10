using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dragodachi.Models;

namespace Dragodachi.Controllers
{
    public class HomeController : Controller
    {
        public static int full = 20;
        public static int happ = 20;
        public static int ml = 3;
        public static int ener = 50;
        public static Random r = new Random();
        public static string message = "...";
        public static string BaseImg = "~/css/DEBase.png";
        [HttpGet("")]
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("fullness", full);
            HttpContext.Session.SetInt32("happiness", happ);
            HttpContext.Session.SetInt32("meals", ml);
            HttpContext.Session.SetInt32("energy", ener);
            ViewBag.fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.meals = HttpContext.Session.GetInt32("meals");
            ViewBag.energy = HttpContext.Session.GetInt32("energy");
            if(full < 1 || happ < 1){
                BaseImg = "~/css/DEGO.png";
                message = "Your dragon ate you!! Try again in the next life!";
            }
            if(full > 99 && happ > 99 && ener > 99){
                BaseImg = "~/css/DEBase.png";
                message = "You and your dragon lived happily ever after!";
            }
            ViewBag.image = BaseImg;
            ViewBag.message = message;
            return View();
        }

        [HttpGet("feed")]
        public IActionResult Feed()
        {
            int like = r.Next(0,100);
            if(ml > 0){
                ml = ml - 1;
                if (like > 25)
                {
                    BaseImg = "~/css/DEFoodPos.png";
                    int ran = r.Next(5,11);
                    full = full + ran;
                    message = "Yum! Your Dragon ate happily!";
                } else {
                    BaseImg = "~/css/DEFoodNeg.png";
                    message = "Your dragon didn't like that...";
                }
                
            }
            return Redirect("/");
        }

        [HttpGet("play")]
        public IActionResult Play()
        {
            int like = r.Next(0,100);
            if (ener > 0)
            {
                ener = ener - 5;
                if(like > 25){
                    BaseImg = "~/css/DEPlayPos.png";
                    int ran = r.Next(5,11);
                    happ = happ + ran;
                    message = "Your Dragon played happily!";
                } else {
                    BaseImg = "~/css/DEPlayNeg.png";
                    message = "Your Dragon just wants to be left alone right now...";
                }    
            }
            return Redirect("/");
        }

        [HttpGet("work")]
        public IActionResult Work()
        {
            if (ener > 0){
                BaseImg = "~/css/DEBase.png";
                int gain = r.Next(1,4);
                ener = ener - 5;
                ml = ml + gain;
                message = "You and your dragon got to work and earned some meals!";
            }
            return Redirect("/");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            if (full > 0 && happ > 0){
                BaseImg = "~/css/DESleep.png";
                ener = ener + 15;
                full = full - 5;
                happ = happ - 5;
                message = "Your Dragon enjoyed a good snooze.";
            }
            return Redirect("/");
        }

        [HttpGet("reset")]
        public IActionResult Reset(){
            BaseImg = "~/css/DEBase.png";
            full = 20;
            happ = 20;
            ml = 3;
            ener = 50;
            message = "...";
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
