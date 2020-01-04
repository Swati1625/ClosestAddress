using System.Web.Mvc;

namespace ClosestAddress.Controllers
{
    public class ClosestAddressController : Controller
    {
        /// <summary>
        /// Index page of ClosestAddress 
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}