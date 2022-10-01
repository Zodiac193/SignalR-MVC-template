using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR_Sample1.Data;
using SignalR_Sample1.Hubs;
using SignalR_Sample1.Models;
using System.Diagnostics;

namespace SignalR_Sample1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IHubContext<DeathlyHallowsHub> _dethlyHub;
        public readonly IHubContext<OrderHub> _order;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger,
            IHubContext<DeathlyHallowsHub> dethlyHub,
            IHubContext<OrderHub> order,
            ApplicationDbContext context)
        {
            _logger = logger;
            _dethlyHub = dethlyHub;
            _order = order;
            _context = context; 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BasicChat()
        {
            return View();
        }

        public async Task<IActionResult> DeathlyHallows(string type)
        {
            if (SD.DeathlyHallowRace.ContainsKey(type))
            {
                SD.DeathlyHallowRace[type]++;
            }
            await _dethlyHub.Clients.All.SendAsync("updateDeathlyHallowCount",
                SD.DeathlyHallowRace[SD.Cloak],
                SD.DeathlyHallowRace[SD.Stone],
                SD.DeathlyHallowRace[SD.Wand]);


            return Accepted();
        }

        public IActionResult Notification()
        {
            return View();
        }

        public IActionResult DeathlyHallowRace()
        {
            return View();
        }

        public IActionResult HarryPotterHouse()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ActionName("Order")]
        public async Task<IActionResult> Order()
        {
            string[] name = { "Bhrugen", "Ben", "Jess", "Laura", "Ron" };
            string[] itemName = { "Food1", "Food2", "Food3", "Food4", "Food5" };

            Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(name.Length);

            Order order = new Order()
            {
                Name = name[index],
                ItemName = itemName[index],
                Count = index
            };

            return View(order);
        }

        [ActionName("Order")]
        [HttpPost]
        public async Task<IActionResult> OrderPost(Order order)
        {

            _context.Orders.Add(order);
            _context.SaveChanges();
            await _order.Clients.All.SendAsync("newOrder");
            return RedirectToAction(nameof(Order));
        }
        [ActionName("OrderList")]
        public async Task<IActionResult> OrderList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var productList = _context.Orders.ToList();
            return Json(new { data = productList });
        }
    }
}