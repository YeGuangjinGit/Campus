using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "哎呀。看来你拐错弯了。";
                    break;
                default:
                    break;
            }
            return View("NotFound");
        }
    }
}
