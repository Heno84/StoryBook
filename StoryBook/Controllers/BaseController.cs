using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;
using DAL.Interfaces;
using StoryBook.Helpers;

namespace StoryBook.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected Logger _logger;
        protected IUnitOfWork _unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            _logger = new Logger(HostingEnvironment.MapPath(ConfigurationManager.AppSettings["LogPath"]));
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
