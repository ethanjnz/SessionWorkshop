using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sessionWorkshop.Models;

namespace sessionWorkshop.Controllers;

public class HomeController : Controller
{
    public static User NewUser = new();

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }




    // ===========================================================================================
    [HttpGet("")]
    public IActionResult Index()
    {
        // HttpContext.Session.SetString("Username", "Nichole");
        return View();
    }
    // ===========================================================================================
    // ============================================================================================
    [HttpPost("process")]
    public IActionResult Process(User newUser)
    {

        // if (!ModelState.IsValid)
        // {
        //     var message = string.Join(" | ", ModelState.Values
        //         .SelectMany(v => v.Errors)
        //         .Select(e => e.ErrorMessage));
        //     Console.WriteLine(message);
        // }

        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }

        HttpContext.Session.SetString("Name", newUser.Name);
        return RedirectToAction("Dashboard");
    }
    // =============================================================================================
    // ===============================================================================================
    [HttpGet("success")]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetString("Name") == null) return RedirectToAction("Index");

        HttpContext.Session.SetInt32("Num", 22);

        return View();
    }
    // ===============================================================================================
    //===============================================================================================
    [HttpPost("math")]
    public IActionResult Math(string math)
    {
        // pull number from session and cast it into a int
        int? Number = HttpContext.Session.GetInt32("Num");
        int NewNumber = (int)Number;

        switch (math)
        {
            case "add":
                NewNumber += 1;
                break;
            case "subtract":
                NewNumber -= 1;
                break;
            case "multiply":
                NewNumber *= 2;
                break;
            case "random":
                Random rand = new();
                int RandNum = rand.Next(1, 11);
                NewNumber += RandNum;
                break;
            default:
                break;
        }
        HttpContext.Session.SetInt32("Num", NewNumber);
        return View("Dashboard");
    }
    //===============================================================================================
    //===============================================================================================
    [HttpPost("clear")]
    public RedirectToActionResult Clear()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
