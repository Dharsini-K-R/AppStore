using AppStore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Controllers
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly AppModelDBContext _context;
        private readonly IConfiguration _configuration;
        public AppController(AppModelDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("[controller]/addApp"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddApp([FromBody]AppModel appModel)
        {
            _context.AppList.Add(appModel);
            await _context.SaveChangesAsync();
            return Ok("App Added Sucessfully");
        }
        [HttpPost]
        [Route("[controller]/DeleteApp"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteApp(int id)
        {
            if (_context.AppList == null)
            {
                return Problem("Nothing to delete");
            }
            var appModel = await _context.AppList.FindAsync(id);
            if (appModel != null)
            {
                _context.AppList.Remove(appModel);
                await _context.SaveChangesAsync();
                return Ok("User deleted successfully");
            }
            else
            {
                return Ok("User Not Found");
            }
        }
        [HttpPut]
        [Route("[controller]/updateApp"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<AppModel>> UpdateApp(int id, [FromBody] AppModel appmodel)
        {
            var update = await _context.AppList.FirstOrDefaultAsync(x => x.Id == appmodel.Id);
            if (update != null)
            {
                update.AppName = appmodel.AppName;
                update.Description = appmodel.Description;
                await _context.SaveChangesAsync();
                return Ok(update);
            }
            else
            {
                return Ok("Not found");
            }
        }
        [HttpGet]
        [Route("[controller]/getApp"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetApp(int id)
        {
            if (id == null || _context.AppList == null)
            {
                return NotFound();
            }
            var applist = await _context.AppList.FindAsync(id);
            return Ok(applist);
        }
    }
}
