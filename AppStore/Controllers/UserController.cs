using AppStore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly AppModelDBContext _context;
        private readonly IConfiguration _configuration;
        public UserController(AppModelDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("[controller]/getApp"), Authorize(Roles = "User")]
        public async Task<IActionResult> GetApp(int id)
        {
            if (id == null || _context.AppList == null)
            {
                return NotFound();
            }
            var applist = await _context.AppList.FindAsync(id);
            return Ok(applist);
        }

        [HttpPost]
        [Route("[controller]/downloadApp"), Authorize(Roles = "User")]
        public async Task<ActionResult<AppModel>> NoofDownload([FromBody]AppModel appModel)
        {
            var TokenVariables = HttpContext.User;
            var UserName = "";//new AppModel();
            if (TokenVariables?.Claims != null)
            {
                foreach (var claim in TokenVariables.Claims)
                {
                    UserName = claim.Value;
                    break;
                }
            }
            var newDownload = _context.AppList.Where(x => x.Id == appModel.Id).SingleOrDefault();
            if(newDownload != null)
            {
                var No_of_Downloads = newDownload.No_of_Downloads;
                newDownload.No_of_Downloads = No_of_Downloads+1;
                
                _context.SaveChanges();
                return Ok("You are downloaded sucessfully");
            }
            else
            {
                return BadRequest("Unable to download the app");
            }
        }
        [HttpPost]
        [Route("[controller]/Ratings"), Authorize(Roles = "User")]
        public async Task<ActionResult<AppModel>> Ratings([FromBody]AppModel appModel)
        {
            var checkRatings = _context.AppList.Where(x => x.AppName == appModel.AppName).SingleOrDefault();
                var TokenVariables = HttpContext.User;
            /* var Name = "";
             if (TokenVariables?.Claims != null)
             {
                 foreach (var claim in TokenVariables.Claims)
                 {
                     Name = claim.Value;
                     break;
                 }
             }*/
            /*var userDetails = _context.AppList.Where(x => x.AppName == Name).SingleOrDefault();*/
            var ratings = checkRatings.Ratings;
                checkRatings.Ratings=(ratings+appModel.Ratings)/checkRatings.No_of_Downloads;
                _context.SaveChanges();
                return Ok("Updated the Ratings");

        }
    }
}

