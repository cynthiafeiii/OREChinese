using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OREChinese.Data;
using OREChinese.Models;
using System.Diagnostics;
using System.Security.Policy;

namespace OREChinese.Controllers
{
    [Route("manage")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _appDbContext;
        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Admin> admins = _appDbContext.Admins.ToList();

            return View(admins);
        }

        [HttpGet("Unit")]
        public IActionResult Unit()
        {
            List<Unit> units = _appDbContext.Units.ToList();

            return View(units);
        }

        [HttpGet("Video/{id}")]
        public IActionResult Video(int id)
        {
            List<Video> videos = _appDbContext.Videos.Where(x => x.UnitId == id).ToList();
            UnitVideo uv = new UnitVideo();
            uv.Videos = videos;
            uv.UnitId = id;
            return View(uv);
        }

        [HttpGet("VideoAdd/{id}")]
        public IActionResult VideoAdd(int id)
        {
            return View(id);
        }

        /// <summary>
        /// 新增视频
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost("SaveUnit")]
        public IActionResult SaveUnit(int unitId, string name, string url, IFormFile image)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            string ex = System.IO.Path.GetExtension(image.FileName);
            fileName = fileName + ex;
            
            if (image != null && image.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }

            Video video = new Video();
            video.AdminId = 1;
            video.UnitId = unitId;
            video.Name = name;
            video.URL = url;
            video.ImageName = fileName;
            _appDbContext.Videos.Add(video);
            _appDbContext.SaveChanges();

            return Redirect("video/" + unitId);
        }

        [HttpGet("UnitAdd")]
        public IActionResult UnitAdd()
        {
            return View();
        }

        /// <summary>
        /// 新增单元
        /// </summary>
        /// <param name="name"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost("UnitAdd")]
        public IActionResult UnitAdd(string name, IFormFile image)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            string ex = System.IO.Path.GetExtension(image.FileName);
            fileName = fileName + ex;

            if (image != null && image.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }

            Unit unit = new Unit();
            unit.Name = name;
            unit.ImageName = fileName;
            _appDbContext.Units.Add(unit);
            _appDbContext.SaveChanges();

            return Redirect("unit");
        }

        /// <summary>
        /// 打开Unit新增或修改页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("UnitEdit/{id?}")]
        public IActionResult UnitEdit(int? id)
        {
            Unit unit;
            if (id.HasValue)
            {
                unit = _appDbContext.Units.First(x => x.Id == id);
            }
            else
            {
                unit = new();
            }

            return View(unit);
        }

        /// <summary>
        /// 保存新增或修改的Unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [HttpPost("UnitEdit")]
        public IActionResult UnitEdit(string name, IFormFile image, int id)
        {
            string fileName = "";
            if (image != null && image.Length > 0)
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                string ex = System.IO.Path.GetExtension(image.FileName);
                fileName = fileName + ex;


                var filePath = Path.Combine("wwwroot/uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
            }

            Unit unit = _appDbContext.Units.First(x => x.Id == id);
            unit.Name = name;
            if(fileName != "")
            {
                unit.ImageName = fileName;
            }

            _appDbContext.Units.Update(unit);
            _appDbContext.SaveChanges();

            return Redirect("Unit");
        }

        [HttpGet("UnitDelete/{id}")]
        public IActionResult UnitDelete(int id)
        {
            Unit unit = _appDbContext.Units.First(x => x.Id == id);
            _appDbContext.Units.Remove(unit);
            _appDbContext.SaveChanges();

            return RedirectToAction("Unit");
        }

        [HttpGet("VideoEdit/{id}")]
        public IActionResult VideoEdit(int id) //新写的
        {
            Video video;
            if (id == 0)
            {
                video = new();
            }
            else
            {
                video = _appDbContext.Videos.First(x => x.Id == id);
            }

            return View(video);
        }

        /// <summary>
        /// 保存修改的视频
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [HttpPost("VideoEdit")]
        public IActionResult VideoEdit(Video video)
        {

            _appDbContext.Videos.Update(video);
            _appDbContext.SaveChanges();

            return Redirect("video/" + video.UnitId);
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
