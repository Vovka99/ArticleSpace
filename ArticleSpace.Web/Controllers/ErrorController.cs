using ArticleSpace.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ArticleSpace.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("[action]")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
