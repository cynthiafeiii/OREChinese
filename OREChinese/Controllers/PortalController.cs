using Microsoft.AspNetCore.Mvc;
using OREChinese.Data;

namespace OREChinese.Controllers
{
    public class PortalController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PortalController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Route("/")]
        public IActionResult Index()
        {
            List<Video> videos = _appDbContext.Videos.ToList();
            return View(videos);
        }

        [Route("/support")]
        public IActionResult Support()
        {
            return View();
        }

        [Route("/category")]
        public IActionResult Category()
        {
            List<Unit> units = _appDbContext.Units.ToList();
            return View(units);
        }

    }
}
