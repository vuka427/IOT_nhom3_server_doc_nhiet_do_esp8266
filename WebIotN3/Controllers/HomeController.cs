using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using WebIotN3.Models;
using WebIotN3.SignalR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebIotN3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHubContext<ControllHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, IHubContext<ControllHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Joystick()
        {
            return View();
        }

        [HttpPost]
        [Route("/UpdateDataApi")]
        public async Task<IActionResult> UpdateDataApi([FromQuery] string station_code , [FromQuery] string station_secret, [FromQuery] string temp  )
        {
            DateTime datenow = DateTime.Now;
            if(temp != null && station_secret != null && station_code != null)
            {   if (_hubContext.Clients != null)
                    await _hubContext.Clients.All.SendAsync("ReceiveDataUpFromEsp8266", new { error = 0 , temp = temp, date = datenow.ToShortDateString(), time = datenow.ToShortTimeString() }) ;
            }
            else
            {
                if (_hubContext.Clients != null)
                    await _hubContext.Clients.All.SendAsync("ReceiveDataUpFromEsp8266", new { error = 1, msg =" lỗi không có dữ liệu" });
            }

            return Ok();
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